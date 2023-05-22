using PDFScrape.Data;
using PDFScrape.Exceptions;
using PDFScrape.Macros.Events;
using PDFScrape.PDF;
using Silence.Hooking;
using Silence.Hooking.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Macros {

    public class MacroRecorder {
        //================================================================================
        // Hook manager doesn't capture input events from windows which belong to a
        // different user or permission level. E.g. If you open notepad as administrator
        // input events when it's the focus don't get captured.
        //================================================================================
        private HookManager                     mHookManager = new HookManager();

        private MacroNode                       mNode = null;
        private MacroNode                       mStartNode = null;

        private bool                            mEnabled = true;

        private bool                            mRecording = false;
        private bool                            mPaused = false;
        private long                            mLastEventTime;

        private long                            mDisablePauseEventTime;

        private List<int>                       mKeysToIgnore = new List<int>();

        //--------------------------------------------------------------------------------
        public event EventDelegate              StartedRecording;
        public event EventDelegate              StoppedRecording;
        public event EventDelegate              PausedRecording;
        public event EventDelegate              UnpausedRecording;
        public event EventDelegate              BeganRepetition;
        public event EventDelegate              EndedRepetition;
        public event EventDelegate              BeganBranching;
        public event EventDelegate              EndedBranching;
        public event EventDelegate              BeganBranch;
        public event EventDelegate              EndedBranch;
        public event EventDelegate              AddedDataInput;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroRecorder() {
            // Hooking
            mHookManager.KeyUp += OnHookKeyUp;
            mHookManager.KeyDown += OnHookKeyDown;
            mHookManager.MouseMove += OnHookMouseMove;
            mHookManager.MouseUp += OnHookMouseUp;
            mHookManager.MouseDown += OnHookMouseDown;
            mHookManager.MouseWheel += OnHookMouseWheel;
        }
        
        //--------------------------------------------------------------------------------
        public void Dispose() {
            mHookManager.Dispose();
        }


        // ENABLED ================================================================================
        //--------------------------------------------------------------------------------
        public void Enable() {
            // Time
            if (!mEnabled && mRecording && !mPaused) {
                long preDisablePauseEventTime = mDisablePauseEventTime - mLastEventTime;
                mLastEventTime = DateTime.Now.Ticks;
                mLastEventTime -= Math.Max(preDisablePauseEventTime, 0); // Add delay time that was left-over at disable / pause
            }

            // Enable
            mEnabled = true;
        }

        //--------------------------------------------------------------------------------
        public void Disable() {
            // Time
            if (mEnabled && mRecording && !mPaused)
                mDisablePauseEventTime = DateTime.Now.Ticks;

            // Disable
            mEnabled = false;
        }

        //--------------------------------------------------------------------------------
        public void SetEnabled(bool enabled) {
            if (enabled)
                Enable();
            else
                Disable();
        }

        //--------------------------------------------------------------------------------
        public bool Enabled { get { return mEnabled; } }


        // RECORDING ================================================================================
        //--------------------------------------------------------------------------------
        // Don't indulge the idea of allowing recording to start from a position in the
        // middle of a node's elements - it's not compatible with the fact keys can be in
        // an up or down state, as you could start recording while a key is already down
        // according to the previous events.
        //--------------------------------------------------------------------------------
        public void Start(MacroNode node, bool clear = false) {
            // Checks
            if (node.Macro == null)
                throw new ArgumentException();

            // Node
            mNode = node;
            mStartNode = node;

            // Clear
            if (clear)
                node.RemoveAllElements();

            // Start
            mLastEventTime = DateTime.Now.Ticks;
            mRecording = true;
            mPaused = false;

            // Disable / pause event time
            if (!mEnabled)
                mDisablePauseEventTime = DateTime.Now.Ticks;

            // Notify
            if (StartedRecording != null)
                StartedRecording(this, mNode);
        }

        //--------------------------------------------------------------------------------
        public void Stop(bool addKeyUpsForUnreleasedKeys = true) {
            // Stop
            bool wasRecording = mRecording;
            mRecording = false;
            mPaused = false;

            // Closure
            MacroNode node = mNode;
            while (true) {
                if (node == null)
                    break;
                node.AddKeyUpsForUnreleasedKeys();
                if ((node == mStartNode) || !node.HasParent)
                    break;
                node = node.Parent;
            }

            // Node
            node = mNode;
            mNode = null;

            // Notify
            if (wasRecording && (StoppedRecording != null))
                StoppedRecording(this, node);
        }

        //--------------------------------------------------------------------------------
        public void Pause() {
            if (mRecording && !mPaused) {
                // Disable / pause event time
                if (mEnabled)
                    mDisablePauseEventTime = DateTime.Now.Ticks;

                // Pause
                mPaused = true;
                
                // Notify
                if (PausedRecording != null)
                    PausedRecording(this, mNode);
            }
        }

        //--------------------------------------------------------------------------------
        public void Unpause() {
            if (mRecording && mPaused) {
                // Time
                if (mEnabled) {
                    long preDisablePauseEventTime = mDisablePauseEventTime - mLastEventTime;
                    mLastEventTime = DateTime.Now.Ticks;
                    mLastEventTime -= Math.Max(preDisablePauseEventTime, 0); // Add delay time that was left-over at disable / pause
                }

                // Unpause
                mPaused = false;
                
                // Notify
                if (UnpausedRecording != null)
                    UnpausedRecording(this, mNode);
            }
        }
        
        //--------------------------------------------------------------------------------
        public bool Recording { get { return mRecording; } }
        public bool Paused { get { return mPaused; } }


        // REPETITIONS ================================================================================
        //--------------------------------------------------------------------------------
        public void BeginRepetition(MacroRepetitionNode.RepetitionMode mode, string extractorName) {
            // Checks
            if (!mRecording)
                throw new InvalidCallException();

            // Begin repetition
            AddDelayEvent();
            mNode = (MacroNode)mNode.AddElement(new MacroRepetitionNode(mode, extractorName));

            // Notify
            if (BeganRepetition != null)
                BeganRepetition(this, mNode);
        }

        //--------------------------------------------------------------------------------
        public void EndRepetition(bool addKeyUpsForUnreleasedKeys = true) {
            // Checks
            if (!mRecording || !InRepetition)
                throw new InvalidCallException();

            // Closure
            if (addKeyUpsForUnreleasedKeys)
                mNode.AddKeyUpsForUnreleasedKeys();

            // End repetition
            MacroNode node = mNode;
            mNode = mNode.Parent;

            // Notify
            if (EndedRepetition != null)
                EndedRepetition(this, node);
        }

        //--------------------------------------------------------------------------------
        public bool InRepetition { get { return mNode is MacroRepetitionNode; } }

        //--------------------------------------------------------------------------------
        public MacroRepetitionNode Repetition(int index) {
            // Checks
            if (mNode == null)
                return null;

            // Repetition
            int depth = RepetitionDepth - 1;
            MacroNode node = mNode;
            while (node != null) {
                if (node is MacroRepetitionNode) {
                    if (depth == index)
                        return (MacroRepetitionNode)node;
                    --depth;
                }
                node = node.Parent;
            }

            // Not found
            return null;
        }

        //--------------------------------------------------------------------------------
        public int RepetitionDepth {
            get {
                int depth = 0;
                MacroNode node = mNode;
                while (node != null) {
                    if (node is MacroRepetitionNode)
                        ++depth;
                    node = node.Parent;
                }

                return depth;
            }
        }


        // BRANCHING ================================================================================
        //--------------------------------------------------------------------------------
        public void BeginBranching(Macro.SourceType source, string extractorName, int repetitionIndex, PDFExtractor.ColumnSpecifier column) {
            // Checks
            if (!mRecording)
                throw new InvalidCallException();

            // Begin branching
            AddDelayEvent();
            mNode = (MacroNode)mNode.AddElement(new MacroBranchingNode(source, extractorName, repetitionIndex, column));

            // Notify
            if (BeganBranching != null)
                BeganBranching(this, mNode);
        }

        //--------------------------------------------------------------------------------
        public void BeginBranching(Macro.SourceType source, string extractorName, PDFExtractor.ColumnSpecifier column) { BeginBranching(source, extractorName, 0, column); }
        public void BeginBranching(Macro.SourceType source, int repetitionIndex, PDFExtractor.ColumnSpecifier column) { BeginBranching(source, "", repetitionIndex, column); }
        
        //--------------------------------------------------------------------------------
        public void EndBranching(bool addKeyUpsForUnreleasedKeys = true) {
            // Checks
            if (!mRecording || !InBranching)
                throw new InvalidCallException();

            // Closure
            if (addKeyUpsForUnreleasedKeys)
                mNode.AddKeyUpsForUnreleasedKeys();

            // End branching
            MacroNode node = mNode;
            mNode = mNode.Parent;

            // Notify
            if (EndedBranching != null)
                EndedBranching(this, node);
        }

        //--------------------------------------------------------------------------------
        public bool InBranching { get { return mNode is MacroBranchingNode; } }

        //--------------------------------------------------------------------------------
        public int BranchingDepth {
            get {
                int depth = 0;
                MacroNode node = mNode;
                while (node != null) {
                    if (node is MacroBranchingNode)
                        ++depth;
                    node = node.Parent;
                }

                return depth;
            }
        }


        // BRANCHES ================================================================================
        //--------------------------------------------------------------------------------
        public void BeginBranch(MacroBranchNode.ConditionType condition, string conditionArgument, bool final) {
            // Checks
            if (!mRecording || !InBranching)
                throw new InvalidCallException();

            // Begin branch
            mNode = ((MacroBranchingNode)mNode).AddBranch(condition, conditionArgument, final);

            // Notify
            if (BeganBranch != null)
                BeganBranch(this, mNode);
        }

        //--------------------------------------------------------------------------------
        public void EndBranch() {
            // Checks
            if (!mRecording || !InBranch)
                throw new InvalidCallException();

            // End branch
            AddDelayEvent();
            MacroNode node = mNode;
            mNode = mNode.Parent;

            // Notify
            if (EndedBranch != null)
                EndedBranch(this, node);
        }
        
        //--------------------------------------------------------------------------------
        public bool InBranch { get { return mNode is MacroBranchNode; } }


        // DATA ================================================================================
        //--------------------------------------------------------------------------------
        public void AddDataInput(Macro.SourceType source, string extractorName, int repetitionIndex, PDFExtractor.ColumnSpecifier column) {
            // Checks
            if (!mRecording)
                throw new InvalidCallException();

            // Data input
            AddDelayEvent();
            mNode.AddElement(new MacroDataInputEvent(source, extractorName, repetitionIndex, column));

            // Notify
            if (AddedDataInput != null)
                AddedDataInput(this, mNode);
        }


        // MACRO ================================================================================
        //--------------------------------------------------------------------------------
        public Macro Macro { get { return (mNode != null ? mNode.Macro : null); } }
        public MacroNode Node { get { return mNode; } }

        //--------------------------------------------------------------------------------
        public int NodeDepth {
            get {
                int depth = 0;
                MacroNode node = mNode;
                while (node != null) {
                    ++depth;
                    node = node.Parent;
                }

                return depth;
            }
        }
        
            
        // MACRO EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        private void AddDelayEvent(bool addZeroDurationDelay = false) {
            // Duration
            long duration;
            if (!mEnabled || mPaused) {
                // Disabled / paused
                duration = Math.Max(mDisablePauseEventTime - mLastEventTime, 0);
                mLastEventTime = mDisablePauseEventTime;
            }
            else {
                // Enabled / not paused
                long time = DateTime.Now.Ticks;
                duration = time - mLastEventTime;
                mLastEventTime = time;
            }
            
            // Event
            if ((duration > 0) || addZeroDurationDelay)
                mNode.AddElement(new MacroDelayEvent(duration));
        }


        // HOOK EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        private void OnHookKeyUp(object sender, GlobalKeyEventHandlerArgs e) {
            if (mEnabled && mRecording && !mPaused && !mKeysToIgnore.Contains(e.VirtualKeyCode)) {
                AddDelayEvent();
                mNode.AddElement(new MacroKeyEvent(MacroKeyEvent.ActionType.UP, e.VirtualKeyCode));
            }
        }

        //--------------------------------------------------------------------------------
        private void OnHookKeyDown(object sender, GlobalKeyEventHandlerArgs e) {
            if (mEnabled && mRecording && !mPaused && !mKeysToIgnore.Contains(e.VirtualKeyCode)) {
                AddDelayEvent();
                mNode.AddElement(new MacroKeyEvent(MacroKeyEvent.ActionType.DOWN, e.VirtualKeyCode));
            }
        }

        //--------------------------------------------------------------------------------
        private void OnHookMouseMove(object sender, GlobalMouseEventHandlerArgs e) {
            if (mEnabled && mRecording && !mPaused) {
                AddDelayEvent();
                mNode.AddElement(new MacroMouseEvent(MacroMouseEvent.ActionType.MOVE, e.Point));
            }
        }

        //--------------------------------------------------------------------------------
        private void OnHookMouseUp(object sender, GlobalMouseEventHandlerArgs e) {
            if (mEnabled && mRecording && !mPaused) {
                AddDelayEvent();
                mNode.AddElement(new MacroMouseEvent(MacroMouseEvent.ActionType.UP, e.Point, e.Button));
            }
        }

        //--------------------------------------------------------------------------------
        private void OnHookMouseDown(object sender, GlobalMouseEventHandlerArgs e) {
            if (mEnabled && mRecording && !mPaused) {
                AddDelayEvent();
                mNode.AddElement(new MacroMouseEvent(MacroMouseEvent.ActionType.DOWN, e.Point, e.Button));
            }
        }

        //--------------------------------------------------------------------------------
        private void OnHookMouseWheel(object sender, GlobalMouseEventHandlerArgs e) {
            if (mEnabled && mRecording && !mPaused) {
                AddDelayEvent();
                mNode.AddElement(new MacroMouseEvent(MacroMouseEvent.ActionType.WHEEL, e.Point, System.Windows.Input.MouseButton.Left, e.Delta));
            }
        }
        
            
        // KEYBOARD ================================================================================
        //--------------------------------------------------------------------------------
        public List<int> KeysToIgnore { get { return mKeysToIgnore; } }


        //================================================================================
        //********************************************************************************
        public delegate void EventDelegate(MacroRecorder recorder, MacroNode node);
    }

}

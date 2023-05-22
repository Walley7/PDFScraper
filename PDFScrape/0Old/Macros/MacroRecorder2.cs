using PDFScrape.Macros.Events;
using Silence.Hooking;
using Silence.Hooking.Windows;
using Silence.Simulation.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Macros {

    public class MacroRecorder2 {
        //================================================================================
        // Hook manager doesn't capture input events from windows which belong to a
        // different user or permission level. E.g. If you open notepad as administrator
        // input events when it's the focus don't get captured.
        //================================================================================
        private HookManager                     mHookManager = new HookManager();

        private Macro2                          mMacro = null;

        private bool                            mRecording = false;
        private bool                            mPaused = false;
        private long                            mLastEventTime;
        private long                            mPauseEventTime;

        private List<int>                       mKeysToIgnore = new List<int>();

            
        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroRecorder2(Macro2 macro = null) {
            // Hooking
            mHookManager.KeyUp += OnHookKeyUp;
            mHookManager.KeyDown += OnHookKeyDown;
            mHookManager.MouseMove += OnHookMouseMove;
            mHookManager.MouseUp += OnHookMouseUp;
            mHookManager.MouseDown += OnHookMouseDown;
            mHookManager.MouseWheel += OnHookMouseWheel;

            // Macro
            mMacro = macro;
        }

        //--------------------------------------------------------------------------------
        public void Dispose() {
            mHookManager.Dispose();
        }
        
            
        // EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        private void OnHookKeyUp(object sender, GlobalKeyEventHandlerArgs e) {
            if (mRecording && !mPaused && !mKeysToIgnore.Contains(e.VirtualKeyCode)) {
                AddDelayEvent();
                mMacro.AddEvent(new MacroKeyEvent2(MacroKeyEvent2.EventType.UP, e.VirtualKeyCode));
            }
        }

        //--------------------------------------------------------------------------------
        private void OnHookKeyDown(object sender, GlobalKeyEventHandlerArgs e) {
            if (mRecording && !mPaused && !mKeysToIgnore.Contains(e.VirtualKeyCode)) {
                AddDelayEvent();
                mMacro.AddEvent(new MacroKeyEvent2(MacroKeyEvent2.EventType.DOWN, e.VirtualKeyCode));
            }
        }

        //--------------------------------------------------------------------------------
        private void OnHookMouseMove(object sender, GlobalMouseEventHandlerArgs e) {
            if (mRecording && !mPaused) {
                AddDelayEvent();
                mMacro.AddEvent(new MacroMouseEvent2(MacroMouseEvent2.EventType.MOVE, e.Point));
            }
        }

        //--------------------------------------------------------------------------------
        private void OnHookMouseUp(object sender, GlobalMouseEventHandlerArgs e) {
            if (mRecording && !mPaused) {
                AddDelayEvent();
                mMacro.AddEvent(new MacroMouseEvent2(MacroMouseEvent2.EventType.UP, e.Point, e.Button));
            }
        }

        //--------------------------------------------------------------------------------
        private void OnHookMouseDown(object sender, GlobalMouseEventHandlerArgs e) {
            if (mRecording && !mPaused) {
                AddDelayEvent();
                mMacro.AddEvent(new MacroMouseEvent2(MacroMouseEvent2.EventType.DOWN, e.Point, e.Button));
            }
        }

        //--------------------------------------------------------------------------------
        private void OnHookMouseWheel(object sender, GlobalMouseEventHandlerArgs e) {
            if (mRecording && !mPaused) {
                AddDelayEvent();
                mMacro.AddEvent(new MacroMouseEvent2(MacroMouseEvent2.EventType.WHEEL, e.Point, System.Windows.Input.MouseButton.Left, e.Delta));
            }
        }
        
            
        // MACRO ================================================================================
        //--------------------------------------------------------------------------------
        public Macro2 Macro { set { mMacro = value; } get { return mMacro; } }

        //--------------------------------------------------------------------------------
        public void ClearMacro() {
            if (mMacro != null)
                mMacro.Clear();
        }

        //--------------------------------------------------------------------------------
        private void AddDelayEvent() {
            long time = DateTime.Now.Ticks;
            mMacro.AddEvent(new MacroDelayEvent2(time - mLastEventTime));
            mLastEventTime = time;
        }


        // DATA INPUT EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        public void AddDataInputEvent(string field) {
            AddDelayEvent();
            mMacro.AddEvent(new MacroDataInputEvent2(field));
        }
        

        // INPUT EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        /*public void AddInputEvents(string input, long delay = 10000) {
            foreach (char c in input) {
                AddInputEvent(c, delay);
            }
        }

        //--------------------------------------------------------------------------------
        public void AddInputEvent(char c, long delay = 10000) {
            // Virtual key
            VirtualKeyStruct virtualKey = new VirtualKeyStruct { value = VkKeyScan(c) };

            // Modifiers
            bool shift = ((virtualKey.high & 1) != 0);

            // Input
            mMacro.AddEvent(new MacroDelayEvent(delay));
            if (shift)
                mMacro.AddEvent(new MacroKeyEvent(MacroKeyEvent.EventType.DOWN, (int)VirtualKeyCode.SHIFT));
            mMacro.AddEvent(new MacroKeyEvent(MacroKeyEvent.EventType.DOWN, virtualKey.low));
            mMacro.AddEvent(new MacroKeyEvent(MacroKeyEvent.EventType.UP, virtualKey.low));
            if (shift)
                mMacro.AddEvent(new MacroKeyEvent(MacroKeyEvent.EventType.UP, (int)VirtualKeyCode.SHIFT));
        }*/
        
            
        // RECORDING ================================================================================
        //--------------------------------------------------------------------------------
        public void Start() {
            ClearMacro();
            mLastEventTime = DateTime.Now.Ticks;
            mRecording = true;
            mPaused = false;
        }

        //--------------------------------------------------------------------------------
        public void Stop() {
            mRecording = false;
            mPaused = false;
        }
        
        //--------------------------------------------------------------------------------
        public void Pause() {
            if (mRecording && !mPaused) {
                mPauseEventTime = DateTime.Now.Ticks;
                mPaused = true;
            }
        }
        
        //--------------------------------------------------------------------------------
        public void Unpause() {
            if (mRecording && mPaused) {
                long prePauseEventTime = mPauseEventTime - mLastEventTime;
                mLastEventTime = DateTime.Now.Ticks;
                mLastEventTime -= Math.Max(prePauseEventTime, 0);
                mPaused = false;
            }
        }
        
        //--------------------------------------------------------------------------------
        public bool Recording { get { return mRecording; } }
        public bool Paused { get { return mPaused; } }
        
            
        // KEYBOARD ================================================================================
        //--------------------------------------------------------------------------------
        public List<int> KeysToIgnore { get { return mKeysToIgnore; } }
    }

}

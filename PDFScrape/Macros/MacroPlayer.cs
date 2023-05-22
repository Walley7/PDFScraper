using PDFScrape.Data;
using PDFScrape.PDF;
using Silence.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace PDFScrape.Macros {

    public class MacroPlayer {
        //================================================================================
        private MouseSimulator                      mMouseSimulator = new MouseSimulator(new InputSimulator());
        private KeyboardSimulator                   mKeyboardSimulator = new KeyboardSimulator(new InputSimulator());
        
        private volatile MacroNode                  mNode = null;
        
        private volatile bool                       mEnabled = true;

        private volatile Thread                     mPlayThread = null;
        private object                              mLock = new object();
        private volatile bool                       mStop = false;
        private volatile bool                       mPaused = false;
        private volatile float                      mSpeed = 1.0f;
        private volatile float                      mDataInputSpeed = 1.0f;

        private volatile ScrapeSet                  mScrapeSet = null;

        private volatile List<MacroRepetitionNode>  mRepetitionStack = new List<MacroRepetitionNode>();

        //--------------------------------------------------------------------------------
        public event EventDelegate                  StartedPlaying;
        public event EventDelegate                  StoppedPlaying = delegate { }; // To avoid null check and make thread safe
        public event EventDelegate                  PausedPlaying;
        public event EventDelegate                  UnpausedPlaying;
        public event EventDelegate                  PlayingElement = delegate { }; // To avoid null check and make thread safe


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroPlayer() { }
        public void Dispose() { }


        // ENABLED ================================================================================
        //--------------------------------------------------------------------------------
        public void Enable() { mEnabled = true; }
        public void Disable() { mEnabled = false; }

        //--------------------------------------------------------------------------------
        public void SetEnabled(bool enabled) {
            if (enabled)
                Enable();
            else
                Disable();
        }

        //--------------------------------------------------------------------------------
        public bool Enabled { get { return mEnabled; } }


        // PLAYING ================================================================================
        //--------------------------------------------------------------------------------
        private void Play() {
            // Play
            if (mNode != null)
                mNode.Play(this);

            // Stop
            lock (mLock) {
                mPlayThread = null;
                mStop = false;
                MacroNode node = mNode;
                mNode = null;

                // Notify
                StoppedPlaying?.Invoke(this, node);
            }
        }

        //--------------------------------------------------------------------------------
        public void Start(MacroNode node, ScrapeSet scrapeSet, float speed = 1.0f, float dataInputSpeed = 1.0f) {
            // Stop
            Stop();

            // Node / speed
            mNode = node;
            mSpeed = speed;
            mDataInputSpeed = dataInputSpeed;

            // Scrape set
            mScrapeSet = scrapeSet;

            // Repetition
            mRepetitionStack.Clear();

            // Start
            mStop = false;
            mPaused = false;
            mPlayThread = new Thread(Play);

            // Notify
            if (StartedPlaying != null)
                StartedPlaying(this, mNode);

            // Play
            mPlayThread.Start();
        }

        //--------------------------------------------------------------------------------
        public void Start(MacroNode node, float speed = 1.0f, float dataInputSpeed = 1.0f) { Start(node, null, speed, dataInputSpeed); }

        //--------------------------------------------------------------------------------
        public void Stop() {
            // Thread
            Thread playThread = null;

            // Stop
            lock (mLock) {
                playThread = mPlayThread;
                mStop = true;
                mPaused = false;
            }
            
            // Join
            if (playThread != null)
                playThread.Join();
        }

        //--------------------------------------------------------------------------------
        public void Pause() {
            lock (mLock) {
                if (Playing && !mPaused) {
                    // Pause
                    mPaused = true;

                    // Notify
                    if (PausedPlaying != null)
                        PausedPlaying(this, mNode);
                }
            }
        }

        //--------------------------------------------------------------------------------
        public void Unpause() {
            lock (mLock) {
                if (Playing && mPaused) {
                    // Unpause
                    mPaused = false;

                    // Notify
                    if (UnpausedPlaying != null)
                        UnpausedPlaying(this, mNode);
                }
            }
        }
        
        //--------------------------------------------------------------------------------
        public bool Playing { get { return (mPlayThread != null); } }
        public bool Stopping { get { return mStop; } }
        public bool Paused { get { return mPaused; } }
        public float Speed { get { return mSpeed; } }
        public float DataInputSpeed { get { return mDataInputSpeed; } }


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


        // SCRAPE SET ================================================================================
        //--------------------------------------------------------------------------------
        public ScrapeSet ScrapeSet { get { return mScrapeSet; } }
        public bool HasScrapeSet { get { return (mScrapeSet != null); } }

        
        // REPETITION ================================================================================
        //--------------------------------------------------------------------------------
        public int PushRepetition(MacroRepetitionNode node) {
            mRepetitionStack.Add(node);
            return (mRepetitionStack.Count - 1);
        }

        //--------------------------------------------------------------------------------
        public void PopRepetition(MacroRepetitionNode node) {
            if (mRepetitionStack.Last() != node)
                throw new InvalidOperationException();
            mRepetitionStack.RemoveAt(mRepetitionStack.Count - 1);
        }

        //--------------------------------------------------------------------------------
        public MacroRepetitionNode Repetition(int index) { return mRepetitionStack[index]; }
        public int RepetitionCount { get { return mRepetitionStack.Count; } }


        // DATA RETRIEVAL ================================================================================
        //--------------------------------------------------------------------------------
        public string RetrieveScrapeData(Macro.SourceType source, string extractorName, int repetitionIndex, PDFExtractor.ColumnSpecifier column) {
            // Checks
            if (!HasScrapeSet)
                return null;

            // Variables
            ScrapeTable table;
            ScrapeRow row;
            string value;

            // Source value
            switch (source) {
                case Macro.SourceType.EXTRACTOR:
                    // Table
                    if (!mScrapeSet.HasTable(extractorName)) {
                        Console.WriteLine("ERROR: Missing extractor '" + extractorName + "'");
                        return null;
                    }
                    table = mScrapeSet.Table(extractorName);

                    // Row
                    if (table.RowCount == 0) {
                        Console.WriteLine("ERROR: Extractor '" + extractorName + "' has no rows");
                        return null;
                    }
                    row = table.Row(0);
                    break;

                case Macro.SourceType.REPETITION_ROW:
                    // Repetition
                    if (repetitionIndex >= RepetitionCount) {
                        Console.WriteLine("ERROR: Repetition index exceeds repetition count");
                        return null;
                    }
                    
                    // Row
                    if (Repetition(repetitionIndex).Row == null) {
                        Console.WriteLine("ERROR: Repetition " + (repetitionIndex + 1) + " has no row");
                        return null;
                    }
                    row = Repetition(repetitionIndex).Row;
                    break;

                case Macro.SourceType.EXTRACTOR_ROW_AT_REPETITION_POSITION:
                    // Repetition
                    if (repetitionIndex >= RepetitionCount) {
                        Console.WriteLine("ERROR: Repetition index exceeds repetition count");
                        return null;
                    }
                    MacroRepetitionNode repetitionNode = Repetition(repetitionIndex);

                    // Table
                    if (!mScrapeSet.HasTable(extractorName)) {
                        Console.WriteLine("ERROR: Missing extractor '" + extractorName + "'");
                        return null;
                    }
                    table = mScrapeSet.Table(extractorName);
                    
                    // Row
                    if (repetitionNode.Row == null) {
                        Console.WriteLine("ERROR: Repetition " + (repetitionIndex + 1) + " has no row");
                        return null;
                    }
                    if (repetitionNode.RowIndex >= table.RowCount) {
                        Console.WriteLine("ERROR: Extractor '" + extractorName + "' has no row at index " + repetitionNode.RowIndex);
                        return null;
                    }
                    row = table.Row(repetitionNode.RowIndex);
                    break;

                default:
                    return null;
            }

            // Value
            value = row.Get(column);
            if (value == null)
                Console.WriteLine("ERROR: Column not found for extractor '" + extractorName + "'");
            return value;
        }

            
        // SIMULATORS ================================================================================
        //--------------------------------------------------------------------------------
        public MouseSimulator MouseSimulator { get { return mMouseSimulator; } }
        public KeyboardSimulator KeyboardSimulator { get { return mKeyboardSimulator; }}


        // NOTIFICATIONS ================================================================================
        //--------------------------------------------------------------------------------
        public void NotifyPlayingElement(MacroElement element) {
            PlayingElement?.Invoke(this, element);
        }


        //================================================================================
        //********************************************************************************
        public delegate void EventDelegate(MacroPlayer player, MacroElement element);
    }

}

using PDFScrape.Data;
using PDFScrape.Macros.Events;
using Silence.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace PDFScrape.Macros {

    public class MacroPlayer2 {
        //================================================================================
        private MouseSimulator                  mMouseSimulator = new MouseSimulator(new InputSimulator());
        private KeyboardSimulator               mKeyboardSimulator = new KeyboardSimulator(new InputSimulator());

        private Macro2                          mMacro = null;

        private Thread                          mThread = null;
        private volatile bool                   mPlaying = false;
        private volatile bool                   mStop = false;
        private volatile bool                   mPaused = false;

        private volatile float                  mSpeed;

        private DataStoreRecord2                 mDataStoreRecord = null;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroPlayer2(Macro2 macro = null) {
            mMacro = macro;
        }
        
            
        // MACRO ================================================================================
        //--------------------------------------------------------------------------------
        public Macro2 Macro2 { set { mMacro = value; } get { return mMacro; } }

            
        // PLAYING ================================================================================
        //--------------------------------------------------------------------------------
        private void Play() {
            // Temporary - need to make play handle or work with input sets and so on
            // Start
            mPlaying = true;

            // Events
            MacroEvent2[] events = mMacro.Events;
            for (int i = 0; i < events.Length;) {
                // Stop
                if (mStop) {
                    mStop = false;
                    break;
                }

                // Play
                if (!mPaused) {
                    events[i].Play(this);
                    ++i;
                }
            }

            // Stop
            mPlaying = false;
        }
        
        //--------------------------------------------------------------------------------
        public void PlayAsync(float speed = 1.0f) {
            // Speed
            mSpeed = speed;

            // Start
            mStop = false;
            mPaused = false;
            mThread = new Thread(Play);
            mThread.Start();
        }
        
        //--------------------------------------------------------------------------------
        public void Stop() {
            mStop = true;
            mPaused = false;
        }
        
        //--------------------------------------------------------------------------------
        public void Pause() {
            if (mPlaying)
                mPaused = true;
        }
        
        //--------------------------------------------------------------------------------
        public void Unpause() {
            mPaused = false;
        }

        //--------------------------------------------------------------------------------
        public bool Playing { get { return mPlaying; } }
        public bool Paused { get { return mPaused; } }
        public float Speed { get { return mSpeed; } }

            
        // SIMULATORS ================================================================================
        //--------------------------------------------------------------------------------
        public MouseSimulator MouseSimulator { get { return mMouseSimulator; } }
        public KeyboardSimulator KeyboardSimulator { get { return mKeyboardSimulator; }}


        // DATA STORE ================================================================================
        //--------------------------------------------------------------------------------
        public DataStoreRecord2 DataStoreRecord { set { mDataStoreRecord = value; } get { return mDataStoreRecord; } }
    }

}

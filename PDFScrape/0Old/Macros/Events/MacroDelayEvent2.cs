using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace PDFScrape.Macros.Events {

    public class MacroDelayEvent2 : MacroEvent2 {
        //================================================================================
        private long                            mDelay;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroDelayEvent2(long delay) {
            mDelay = delay;
        }

        
        // PROPERTIES ================================================================================
        //--------------------------------------------------------------------------------
        public long Delay { get { return mDelay; } }


        // PLAYING ================================================================================
        //--------------------------------------------------------------------------------
        public override void Play(MacroPlayer2 player) {
            long delay = (long)((double)mDelay * (double)player.Speed);
            //Console.Out.WriteLine("delay: " + delay);
            Thread.Sleep(new TimeSpan(delay));
        }
    }

}

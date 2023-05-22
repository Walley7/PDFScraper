using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace PDFScrape.Macros.Events {

    public class MacroDelayEvent : MacroEvent {
        //================================================================================
        private long                            mDelay;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroDelayEvent() : this(0) { }

        //--------------------------------------------------------------------------------
        public MacroDelayEvent(long delay) {
            mDelay = delay;
        }


        // PLAYING ================================================================================
        //--------------------------------------------------------------------------------
        public override bool Play(MacroPlayer player) {
            // Notify
            player.NotifyPlayingElement(this);

            // Play
            long delay = (long)((double)mDelay * (double)player.Speed);
            Thread.Sleep(new TimeSpan(delay));

            // Return
            return true;
        }

        
        // PROPERTIES ================================================================================
        //--------------------------------------------------------------------------------
        public long Delay { get { return mDelay; } }


        // PRESENTATION ================================================================================
        //--------------------------------------------------------------------------------
        public override Color Colour { get { return Color.FromArgb(127, 127, 127); } }


        // INFORMATION ================================================================================
        //--------------------------------------------------------------------------------
        public override string[] Information { get { return new string[] { "Delay", "milliseconds: " + Delay / (double)TimeSpan.TicksPerMillisecond }; } }


        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public override void WriteJSON(JsonTextWriter writer) {
            writer.WritePropertyName("delay"); writer.WriteValue(mDelay);
        }

        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            mDelay = (long)token.SelectToken("delay");
        }
    }

}

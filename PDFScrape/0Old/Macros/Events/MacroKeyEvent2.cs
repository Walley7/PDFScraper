using Silence.Simulation.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Macros.Events {

    public class MacroKeyEvent2 : MacroEvent2 {
        //================================================================================
        public enum EventType {
            UP,
            DOWN
        }


        //================================================================================
        private EventType                       mType;

        private int                             mKey;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroKeyEvent2(EventType type, int key) {
            mType = type;
            mKey = key;
        }


        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public EventType Type { get { return mType; } }
        public bool IsUpEvent { get { return (mType == EventType.UP); } }
        public bool IsDownEvent { get { return (mType == EventType.DOWN); } }


        // PROPERTIES ================================================================================
        //--------------------------------------------------------------------------------
        public int Key { get { return mKey; } }


        // PLAYING ================================================================================
        //--------------------------------------------------------------------------------
        public override void Play(MacroPlayer2 player) {
            switch (mType) {
                case EventType.UP:      player.KeyboardSimulator.KeyUp((VirtualKeyCode)mKey); break;
                case EventType.DOWN:    player.KeyboardSimulator.KeyDown((VirtualKeyCode)mKey); break;
            }
        }
    }

}

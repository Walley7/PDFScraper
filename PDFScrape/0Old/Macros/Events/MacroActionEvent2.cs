using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Macros.Events {

    public class MacroActionEvent2 : MacroEvent2 {
        //================================================================================
        public enum EventType {
            INPUT
        }


        //================================================================================
        private EventType                       mType;

        private string                          mInputName;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroActionEvent2(EventType type, string inputName) {
            mType = type;
            mInputName = inputName;
        }

        
        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public EventType Type { get { return mType; } }
        public bool IsInputEvent { get { return (mType == EventType.INPUT); } }
        
        
        // PROPERTIES ================================================================================
        //--------------------------------------------------------------------------------
        public string InputName { get { return mInputName; } }


        // PLAYING ================================================================================
        //--------------------------------------------------------------------------------
        public override void Play(MacroPlayer2 player) {

        }
    }

}

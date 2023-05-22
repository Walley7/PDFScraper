using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Macros {

    public abstract class MacroEvent : MacroElement {
        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroEvent() { }


        // PLAYING ================================================================================
        //--------------------------------------------------------------------------------
        public override bool Play(MacroPlayer player) {
            // Checks
            if (player.Stopping)
                return false;

            // Return
            return true;
        }


        // MACRO ================================================================================
        //--------------------------------------------------------------------------------
        protected override Macro _Macro { get { return (HasParent ? Parent.Macro : null); } }


        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public override bool IsNode { get { return false; } }
        public override bool IsEvent { get { return true; } }
    }

}

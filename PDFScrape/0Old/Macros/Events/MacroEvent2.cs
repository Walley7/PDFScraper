using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Macros.Events {

    public abstract class MacroEvent2 {
        // PLAYING ================================================================================
        //--------------------------------------------------------------------------------
        public abstract void Play(MacroPlayer2 player);


        // XML ================================================================================
        //--------------------------------------------------------------------------------
        //public override string XMLString { get { return ""; } }
        //Save(XmlDoc)?
    }

}

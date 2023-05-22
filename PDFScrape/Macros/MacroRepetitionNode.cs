using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDFScrape.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Macros {

    public class MacroRepetitionNode : MacroNode {
        //================================================================================
        public enum RepetitionMode {
            PER_ROW
        }


        //================================================================================
        private RepetitionMode                  mMode;

        private string                          mExtractorName;

        private ScrapeRow                       mRow = null;
        private int                             mRowIndex = -1;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroRepetitionNode() : this(RepetitionMode.PER_ROW, "") { }

        //--------------------------------------------------------------------------------
        public MacroRepetitionNode(RepetitionMode mode, string extractorName) {
            mMode = mode;
            mExtractorName = extractorName;
        }


        // PLAYING ================================================================================
        //--------------------------------------------------------------------------------
        public override bool Play(MacroPlayer player) {
            // Notify
            player.NotifyPlayingElement(this);

            // Repetition
            player.PushRepetition(this);
            bool result = PlayRepetition(player);
            player.PopRepetition(this);
           
            // Return
            return result;
        }
        
        //--------------------------------------------------------------------------------
        protected bool PlayRepetition(MacroPlayer player) {
            // Scrape set
            ScrapeSet scrapeSet = player.ScrapeSet;

            // Table
            if (!scrapeSet.HasTable(mExtractorName))
                Console.WriteLine("ERROR: Missing extractor '" + mExtractorName + "'");
            else {
                // Rows
                ScrapeTable table = scrapeSet.Table(mExtractorName);
                for (int i = 0; i < table.RowCount; ++i) {
                    mRow = table.Row(i);
                    mRowIndex = i;

                    if (!PlayElements(player)) {
                        mRow = null;
                        mRowIndex = -1;
                        return false;
                    }
                }
            }

            // Return
            mRow = null;
            mRowIndex = -1;
            return true;
        }


        // MODE ================================================================================
        //--------------------------------------------------------------------------------
        public RepetitionMode Mode { get { return mMode; } }


        // EXTRACTOR ================================================================================
        //--------------------------------------------------------------------------------
        public string ExtractorName { get { return mExtractorName; } }


        // REPETITION ================================================================================
        //--------------------------------------------------------------------------------
        public ScrapeRow Row { get { return mRow; } }
        public int RowIndex { get { return mRowIndex; } }

        
        // PRESENTATION ================================================================================
        //--------------------------------------------------------------------------------
        public override Color Colour { get { return Color.FromArgb(255, 127, 127); } }
        public override OverlayType Overlay { get { return OverlayType.CENTRAL; } }
        public override string OverlayMessage { get { return "Began repetition"; } }
        public override Color OverlayColour1 { get { return Color.FromArgb(255, 127, 127); } }
        public override Color OverlayColour2 { get { return Color.FromArgb(127, 0, 0); } }


        // INFORMATION ================================================================================
        //--------------------------------------------------------------------------------
        public override string[] Information {
            get {
                // Type
                string type = "Repetition";

                // Mode
                switch (mMode) {
                    case RepetitionMode.PER_ROW: type += " (Per Row)";  break;
                }

                // Parameters
                string parameters = "extractor: " + ExtractorName;

                // Return
                return new string[] { type, parameters };
            }
        }

        
        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public override void WriteJSON(JsonTextWriter writer) {
            // Fields
            writer.WritePropertyName("mode"); writer.WriteValue((int)mMode);
            writer.WritePropertyName("extractor_name"); writer.WriteValue(mExtractorName);

            // Node
            base.WriteJSON(writer);
        }

        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            // Fields
            mMode = (RepetitionMode)((int)token.SelectToken("mode"));
            mExtractorName = (string)token.SelectToken("extractor_name");

            // Node
            base.ReadJSON(token);
        }
    }

}

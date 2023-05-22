using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDFScrape.Data;
using PDFScrape.PDF.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.PDF.Extractor_Filters {

    public class PEFInsertColumns : PDFColumnExtractorFilter {
        //================================================================================
        protected ScrapeColumnSpecifier         mPosition = new ScrapeColumnSpecifier(0);
        protected int                           mCount = 1;


        //================================================================================
        //--------------------------------------------------------------------------------
        public PEFInsertColumns() : base() { }
        

        // FILTERING ================================================================================
        //--------------------------------------------------------------------------------
        public override ScrapeTable Apply(ScrapeTable table) {
            // Settings
            string headerPrefix = NewHeaderPrefix;
            int position = Position.ColumnIndex(table);
            if (Position.RelativeTo == ScrapeColumnSpecifier.RelativePoint.END)
                ++position;
            position = Math.Min(Math.Max(position, 0), table.ColumnCount);

            // Table
            ScrapeTable newTable = new ScrapeTable(table);
            
            // Insert
            for (int i = 0; i < Count; ++i) {
                newTable.AddColumn(position + i, newTable.FirstFreeHeader(headerPrefix));
            }

            return newTable;
        }


        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public override string TypeName { get { return "Insert Columns"; } }


        // SETTINGS ================================================================================
        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Naming", Width = 100, Control = APDFFilterSetting.ControlType.STRING)]
        public override string NewHeaderPrefix {
            set { base.NewHeaderPrefix = value; }
            get { return base.NewHeaderPrefix; }
        }

        //--------------------------------------------------------------------------------
        public ScrapeColumnSpecifier Position { get { return mPosition; } }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Position", Control = APDFFilterSetting.ControlType.COLUMN_RELATIVE_POINT)]
        public int PositionRelativeTo {
            set {
                mPosition.RelativeTo = (ScrapeColumnSpecifier.RelativePoint)value;
                OnPropertyChanged("PositionRelativeTo");
            }
            get { return (int)mPosition.RelativeTo; }
        }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Control = APDFFilterSetting.ControlType.POSITIVE_ZERO_INTEGER)]
        public int PositionPosition {
            set {
                mPosition.Position = value;
                OnPropertyChanged("PositionPosition");
            }
            get { return mPosition.Position; }
        }
        
        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Count", Control = APDFFilterSetting.ControlType.POSITIVE_INTEGER)]
        public int Count {
            set { SetProperty("Count", ref mCount, value); }
            get { return mCount; }
        }


        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public override void WriteJSON(JsonTextWriter writer) {
            // Base
            base.WriteJSON(writer);

            // Fields
            writer.WritePropertyName("position"); Position.WriteJSON(writer);
            writer.WritePropertyName("count"); writer.WriteValue(Count);
        }

        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            // Base
            base.ReadJSON(token);

            // Fields
            Position.ReadJSON(token.SelectToken("position"));
            Count = (int)token.SelectToken("count");
        }
    }

}

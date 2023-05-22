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

    public class PEFMoveColumn : PDFColumnExtractorFilter {
        //================================================================================
        protected ScrapeColumnSpecifier         mColumn = new ScrapeColumnSpecifier(0);
        protected int                           mAmount = 1;

        
        //================================================================================
        //--------------------------------------------------------------------------------
        public PEFMoveColumn() : base() { }


        // FILTERING ================================================================================
        //--------------------------------------------------------------------------------
        public override ScrapeTable Apply(ScrapeTable table) {
            // Settings
            int column = Column.ColumnIndex(table);
            if (column < 0 || column >= table.ColumnCount)
                return table;

            // Apply
            ScrapeTable newTable = new ScrapeTable(table);
            newTable.MoveColumn(column, Amount);
            return newTable;
        }


        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public override string TypeName { get { return "Move Column"; } }


        // SETTINGS ================================================================================
        //--------------------------------------------------------------------------------
        public ScrapeColumnSpecifier Column { get { return mColumn; } }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Column", Control = APDFFilterSetting.ControlType.COLUMN_RELATIVE_POINT)]
        public int ColumnRelativeTo {
            set {
                mColumn.RelativeTo = (ScrapeColumnSpecifier.RelativePoint)value;
                OnPropertyChanged("PositionRelativeTo");
            }
            get { return (int)mColumn.RelativeTo; }
        }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Control = APDFFilterSetting.ControlType.POSITIVE_INTEGER)]
        public int ColumnPosition {
            set {
                mColumn.DisplayPosition = value;
                OnPropertyChanged("ColumnPosition");
            }
            get { return mColumn.DisplayPosition; }
        }
        
        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Amount", Control = APDFFilterSetting.ControlType.INTEGER)]
        public int Amount {
            set { SetProperty("Amount", ref mAmount, value); }
            get { return mAmount; }
        }


        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public override void WriteJSON(JsonTextWriter writer) {
            // Base
            base.WriteJSON(writer);

            // Fields
            writer.WritePropertyName("column"); Column.WriteJSON(writer);
            writer.WritePropertyName("amount"); writer.WriteValue(Amount);
        }

        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            // Base
            base.ReadJSON(token);

            // Fields
            Column.ReadJSON(token.SelectToken("column"));
            Amount = (int)token.SelectToken("column_end");
        }
    }

}

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

    public class PEFRemoveRowsWhere : PDFExtractorFilter {
        //================================================================================
        protected ScrapeColumnSpecifier         mColumn = new ScrapeColumnSpecifier(0);
        
        protected ScrapeTable.Condition         mCondition = ScrapeTable.Condition.HAS_VALUE;
        protected string                        mConditionArgument = "";


        //================================================================================
        //--------------------------------------------------------------------------------
        public PEFRemoveRowsWhere() : base() { }


        // FILTERING ================================================================================
        //--------------------------------------------------------------------------------
        public override ScrapeTable Apply(ScrapeTable table) {
            // Settings
            int column = Column.ColumnIndex(table);
            if (column < 0 || column >= table.ColumnCount)
                return table;

            // Apply
            ScrapeTable newTable = new ScrapeTable(table);
            newTable.RemoveRowsWhere(mCondition, mConditionArgument, column);
            return newTable;
        }

        
        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public override string TypeName { get { return "Remove Rows Where"; } }


        // SETTINGS ================================================================================
        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Condition", Width = 120, Control = APDFFilterSetting.ControlType.CONDITION)]
        public int Condition {
            set {
                mCondition = (ScrapeTable.Condition)value;
                OnPropertyChanged("Condition");
            }
            get { return (int)mCondition; }
        }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "", Width = 100, Control = APDFFilterSetting.ControlType.STRING)]
        public string ConditionArgument {
            set { SetProperty("ConditionArgument", ref mConditionArgument, value); }
            get { return mConditionArgument; }
        }

        //--------------------------------------------------------------------------------
        public ScrapeColumnSpecifier Column { get { return mColumn; } }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Column", Padding = -3, Control = APDFFilterSetting.ControlType.COLUMN_RELATIVE_POINT)]
        public int ColumnRelativeTo {
            set {
                mColumn.RelativeTo = (ScrapeColumnSpecifier.RelativePoint)value;
                OnPropertyChanged("ColumnRelativeTo");
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
        
        
        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public override void WriteJSON(JsonTextWriter writer) {
            // Fields
            writer.WritePropertyName("column"); Column.WriteJSON(writer);

            writer.WritePropertyName("condition"); writer.WriteValue(Condition);
            writer.WritePropertyName("condition_argument"); writer.WriteValue(ConditionArgument);
        }

        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            // Fields
            Column.ReadJSON(token.SelectToken("column"));

            Condition = (int)token.SelectToken("condition");
            ConditionArgument = (string)token.SelectToken("condition_argument");
        }
    }

}

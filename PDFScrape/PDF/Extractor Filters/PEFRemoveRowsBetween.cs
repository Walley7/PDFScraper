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

    public class PEFRemoveRowsBetween : PDFExtractorFilter {
        //================================================================================
        protected bool                          mUseStartCondition = false;
        protected ScrapeTable.Condition         mStartCondition = ScrapeTable.Condition.HAS_VALUE;
        protected string                        mStartConditionArgument = "";
        protected ScrapeColumnSpecifier         mStartConditionColumn = new ScrapeColumnSpecifier(0);
        protected bool                          mIncludeStartRow = true;

        protected bool                          mUseEndCondition = false;
        protected ScrapeTable.Condition         mEndCondition = ScrapeTable.Condition.HAS_VALUE;
        protected string                        mEndConditionArgument = "";
        protected ScrapeColumnSpecifier         mEndConditionColumn = new ScrapeColumnSpecifier(0);
        protected bool                          mIncludeEndRow = true;


        //================================================================================
        //--------------------------------------------------------------------------------
        public PEFRemoveRowsBetween() : base() { }


        // FILTERING ================================================================================
        //--------------------------------------------------------------------------------
        public override ScrapeTable Apply(ScrapeTable table) {
            // Checks
            if (table.RowCount == 0)
                return table;

            // Settings
            int startConditionColumn = StartConditionColumn.ColumnIndex(table);
            if (startConditionColumn < 0 || startConditionColumn >= table.ColumnCount)
                return table;

            int endConditionColumn = EndConditionColumn.ColumnIndex(table);
            if (endConditionColumn < 0 || endConditionColumn >= table.ColumnCount)
                return table;

            // Apply
            ScrapeTable newTable = new ScrapeTable(table);
            newTable.KeepRowsBetween(UseStartCondition ? mStartCondition : (ScrapeTable.Condition?)null, mStartConditionArgument, startConditionColumn, IncludeStartRow,
                                     UseEndCondition ? mEndCondition : (ScrapeTable.Condition?)null, mEndConditionArgument, endConditionColumn, IncludeEndRow);
            return newTable;
        }


        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public override string TypeName { get { return "Remove Rows Between"; } }

        
        // SETTINGS ================================================================================
        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Start condition", LabelInFront = false, Padding = 15, Control = APDFFilterSetting.ControlType.BOOLEAN)]
        public bool UseStartCondition {
            set { SetProperty("UseStartCondition", ref mUseStartCondition, value); }
            get { return mUseStartCondition; }
        }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "", Width = 120, Control = APDFFilterSetting.ControlType.CONDITION)]
        public int StartCondition {
            set {
                mStartCondition = (ScrapeTable.Condition)value;
                OnPropertyChanged("StartCondition");
            }
            get { return (int)mStartCondition; }
        }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "", Width = 100, Control = APDFFilterSetting.ControlType.STRING)]
        public string StartConditionArgument {
            set { SetProperty("StartConditionArgument", ref mStartConditionArgument, value); }
            get { return mStartConditionArgument; }
        }
        
        //--------------------------------------------------------------------------------
        public ScrapeColumnSpecifier StartConditionColumn { get { return mStartConditionColumn; } }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Column", Padding = -3, Control = APDFFilterSetting.ControlType.COLUMN_RELATIVE_POINT)]
        public int StartConditionColumnRelativeTo {
            set {
                mStartConditionColumn.RelativeTo = (ScrapeColumnSpecifier.RelativePoint)value;
                OnPropertyChanged("StartConditionColumnRelativeTo");
            }
            get { return (int)mStartConditionColumn.RelativeTo; }
        }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Control = APDFFilterSetting.ControlType.POSITIVE_INTEGER)]
        public int StartConditionColumnPosition {
            set {
                mStartConditionColumn.DisplayPosition = value;
                OnPropertyChanged("StartConditionColumnPosition");
            }
            get { return mStartConditionColumn.DisplayPosition; }
        }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Include start row", LabelInFront = false, Control = APDFFilterSetting.ControlType.BOOLEAN)]
        public bool IncludeStartRow {
            set { SetProperty("IncludeStartRow", ref mIncludeStartRow, value); }
            get { return mIncludeStartRow; }
        }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "End condition", LabelInFront = false, Padding = 15, Width = 100, Control = APDFFilterSetting.ControlType.BOOLEAN)]
        public bool UseEndCondition {
            set { SetProperty("UseEndCondition", ref mUseEndCondition, value); }
            get { return mUseEndCondition; }
        }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "", Width = 120, Control = APDFFilterSetting.ControlType.CONDITION)]
        public int EndCondition {
            set {
                mEndCondition = (ScrapeTable.Condition)value;
                OnPropertyChanged("EndCondition");
            }
            get { return (int)mEndCondition; }
        }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "", Width = 100, Control = APDFFilterSetting.ControlType.STRING)]
        public string EndConditionArgument {
            set { SetProperty("EndConditionArgument", ref mEndConditionArgument, value); }
            get { return mEndConditionArgument; }
        }
        
        //--------------------------------------------------------------------------------
        public ScrapeColumnSpecifier EndConditionColumn { get { return mEndConditionColumn; } }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Column", Padding = -3, Control = APDFFilterSetting.ControlType.COLUMN_RELATIVE_POINT)]
        public int EndConditionColumnRelativeTo {
            set {
                mEndConditionColumn.RelativeTo = (ScrapeColumnSpecifier.RelativePoint)value;
                OnPropertyChanged("EndConditionColumnRelativeTo");
            }
            get { return (int)mEndConditionColumn.RelativeTo; }
        }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Control = APDFFilterSetting.ControlType.POSITIVE_INTEGER)]
        public int EndConditionColumnPosition {
            set {
                mEndConditionColumn.DisplayPosition = value;
                OnPropertyChanged("EndConditionColumnPosition");
            }
            get { return mEndConditionColumn.DisplayPosition; }
        }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Include end row", LabelInFront = false, Control = APDFFilterSetting.ControlType.BOOLEAN)]
        public bool IncludeEndRow {
            set { SetProperty("IncludeEndRow", ref mIncludeEndRow, value); }
            get { return mIncludeEndRow; }
        }

        //--------------------------------------------------------------------------------
        public override float SettingsRows { get { return 2.0f; } }
        
        
        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public override void WriteJSON(JsonTextWriter writer) {
            // Fields
            writer.WritePropertyName("use_start_condition"); writer.WriteValue(UseStartCondition);
            writer.WritePropertyName("start_condition"); writer.WriteValue((int)StartCondition);
            writer.WritePropertyName("start_condition_argument"); writer.WriteValue(StartConditionArgument);
            writer.WritePropertyName("start_condition_column"); StartConditionColumn.WriteJSON(writer);
            writer.WritePropertyName("include_start_row"); writer.WriteValue(IncludeStartRow);

            writer.WritePropertyName("use_end_condition"); writer.WriteValue(UseEndCondition);
            writer.WritePropertyName("end_condition"); writer.WriteValue((int)EndCondition);
            writer.WritePropertyName("end_condition_argument"); writer.WriteValue(EndConditionArgument);
            writer.WritePropertyName("end_condition_column"); EndConditionColumn.WriteJSON(writer);
            writer.WritePropertyName("include_end_row"); writer.WriteValue(IncludeEndRow);
        }

        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            // Fields
            UseStartCondition = (bool)token.SelectToken("use_start_condition");
            StartCondition = (int)token.SelectToken("start_condition");
            StartConditionArgument = (string)token.SelectToken("start_condition_argument");
            StartConditionColumn.ReadJSON(token.SelectToken("start_condition_column"));
            IncludeStartRow = (bool)token.SelectToken("include_start_row");
            
            UseEndCondition = (bool)token.SelectToken("use_end_condition");
            EndCondition = (int)token.SelectToken("end_condition");
            EndConditionArgument = (string)token.SelectToken("end_condition_argument");
            EndConditionColumn.ReadJSON(token.SelectToken("end_condition_column"));
            IncludeEndRow = (bool)token.SelectToken("include_end_row");
        }
    }

}

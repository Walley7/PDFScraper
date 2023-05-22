using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDFScrape.Data;
using PDFScrape.PDF.Attributes;
using PDFScrape.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.PDF.Extractor_Filters {

    public class PEFAddSetText : PDFExtractorFilter {
        //================================================================================
        public enum ActionType {
            SET,
            APPEND_START,
            APPEND_END
        }


        //================================================================================
        protected ScrapeColumnSpecifier                 mColumnStart = new ScrapeColumnSpecifier(0);
        protected ScrapeColumnSpecifier                 mColumnEnd = new ScrapeColumnSpecifier(0, ScrapeColumnSpecifier.RelativePoint.END);

        protected ScrapeRowSpecifier                    mRowStart = new ScrapeRowSpecifier(0);
        protected ScrapeRowSpecifier                    mRowEnd = new ScrapeRowSpecifier(0, ScrapeRowSpecifier.RelativePoint.END);

        protected ActionType                            mAction = ActionType.SET;

        protected bool                                  mUseCondition = false;
        protected ScrapeTable.Condition                 mCondition = ScrapeTable.Condition.HAS_VALUE;
        protected string                                mConditionArgument = "";

        protected string                                mText = "";


        //================================================================================
        //--------------------------------------------------------------------------------
        public PEFAddSetText() : base() { }


        // FILTERING ================================================================================
        //--------------------------------------------------------------------------------
        public override ScrapeTable Apply(ScrapeTable table) {
            // Checks
            if (table.ColumnCount == 0 || table.RowCount == 0)
                return table;

            // Settings
            int columnStart = Math.Max(Math.Min(ColumnStart.ColumnIndex(table), table.ColumnCount - 1), 0);
            int columnEnd = Math.Max(Math.Min(ColumnEnd.ColumnIndex(table), table.ColumnCount - 1), 0);
            if (columnEnd < columnStart)
                UGeneral.Swap(ref columnStart, ref columnEnd);

            int rowStart = Math.Max(Math.Min(RowStart.RowIndex(table), table.RowCount - 1), 0);
            int rowEnd = Math.Max(Math.Min(RowEnd.RowIndex(table), table.RowCount - 1), 0);
            if (rowEnd < rowStart)
                UGeneral.Swap(ref rowStart, ref rowEnd);

            // New table
            ScrapeTable newTable = new ScrapeTable(table);

            // Apply
            for (int i = rowStart; i <= rowEnd; ++i) {
                for (int j = columnStart; j <= columnEnd; ++j) {
                    string cellValue = newTable.Row(i).Get(j);

                    if (!UseCondition || ScrapeTable.MeetsCondition(cellValue, mCondition, ConditionArgument)) {
                        switch (mAction) {
                            case ActionType.SET:            newTable.Row(i).Set(j, Text); break;
                            case ActionType.APPEND_START:   newTable.Row(i).Set(j, Text + newTable.Row(i).Get(j)); break;
                            case ActionType.APPEND_END:     newTable.Row(i).Set(j, newTable.Row(i).Get(j) + Text); break;
                        }
                    }
                }
            }

            return newTable;
        }


        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public override string TypeName { get { return "Add / Set Text"; } }


        // SETTINGS ================================================================================
        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Text", Width = 100, Control = APDFFilterSetting.ControlType.STRING)]
        public string Text {
            set { SetProperty("Text", ref mText, value); }
            get { return mText; }
        }
        
        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Action", Width = 100, ListValues = new string[] { "Set", "Append to start", "Append to end" }, Control = APDFFilterSetting.ControlType.LIST)]
        public int Action {
            set {
                mAction = (ActionType)value;
                OnPropertyChanged("Action");
            }
            get { return (int)mAction; }
        }

        //--------------------------------------------------------------------------------
        public ScrapeColumnSpecifier ColumnStart { get { return mColumnStart; } }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Columns", Control = APDFFilterSetting.ControlType.COLUMN_RELATIVE_POINT)]
        public int ColumnStartRelativeTo {
            set {
                mColumnStart.RelativeTo = (ScrapeColumnSpecifier.RelativePoint)value;
                OnPropertyChanged("ColumnStartRelativeTo");
            }
            get { return (int)mColumnStart.RelativeTo; }
        }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Control = APDFFilterSetting.ControlType.POSITIVE_INTEGER)]
        public int ColumnStartPosition {
            set {
                mColumnStart.DisplayPosition = value;
                OnPropertyChanged("ColumnStartPosition");
            }
            get { return mColumnStart.DisplayPosition; }
        }
        
        //--------------------------------------------------------------------------------
        public ScrapeColumnSpecifier ColumnEnd { get { return mColumnEnd; } }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "to", Padding = -5, Control = APDFFilterSetting.ControlType.COLUMN_RELATIVE_POINT)]
        public int ColumnEndRelativeTo {
            set {
                mColumnEnd.RelativeTo = (ScrapeColumnSpecifier.RelativePoint)value;
                OnPropertyChanged("ColumnEndRelativeTo");
            }
            get { return (int)mColumnEnd.RelativeTo; }
        }
        
        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Control = APDFFilterSetting.ControlType.POSITIVE_INTEGER)]
        public int ColumnEndPosition {
            set {
                mColumnEnd.DisplayPosition = value;
                OnPropertyChanged("ColumnEndPosition");
            }
            get { return mColumnEnd.DisplayPosition; }
        }

        //--------------------------------------------------------------------------------
        public ScrapeRowSpecifier RowStart { get { return mRowStart; } }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Rows", Control = APDFFilterSetting.ControlType.ROW_RELATIVE_POINT)]
        public int RowStartRelativeTo {
            set {
                mRowStart.RelativeTo = (ScrapeRowSpecifier.RelativePoint)value;
                OnPropertyChanged("RowStartRelativeTo");
            }
            get { return (int)mRowStart.RelativeTo; }
        }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Control = APDFFilterSetting.ControlType.POSITIVE_INTEGER)]
        public int RowStartPosition {
            set {
                mRowStart.DisplayPosition = value;
                OnPropertyChanged("RowStartPosition");
            }
            get { return mRowStart.DisplayPosition; }
        }
        
        //--------------------------------------------------------------------------------
        public ScrapeRowSpecifier RowEnd { get { return mRowEnd; } }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "to", Padding = -5, Control = APDFFilterSetting.ControlType.ROW_RELATIVE_POINT)]
        public int RowEndRelativeTo {
            set {
                mRowEnd.RelativeTo = (ScrapeRowSpecifier.RelativePoint)value;
                OnPropertyChanged("RowEndRelativeTo");
            }
            get { return (int)mRowEnd.RelativeTo; }
        }
        
        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Control = APDFFilterSetting.ControlType.POSITIVE_INTEGER)]
        public int RowEndPosition {
            set {
                mRowEnd.DisplayPosition = value;
                OnPropertyChanged("RowEndPosition");
            }
            get { return mRowEnd.DisplayPosition; }
        }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Condition", LabelInFront = false, Padding = 15, Control = APDFFilterSetting.ControlType.BOOLEAN)]
        public bool UseCondition {
            set { SetProperty("UseCondition", ref mUseCondition, value); }
            get { return mUseCondition; }
        }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "", Width = 120, Control = APDFFilterSetting.ControlType.CONDITION)]
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
        public override float SettingsRows { get { return 2.0f; } }
        
        
        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public override void WriteJSON(JsonTextWriter writer) {
            // Fields
            writer.WritePropertyName("column_start"); ColumnStart.WriteJSON(writer);
            writer.WritePropertyName("column_end"); ColumnEnd.WriteJSON(writer);

            writer.WritePropertyName("row_start"); RowStart.WriteJSON(writer);
            writer.WritePropertyName("row_end"); RowEnd.WriteJSON(writer);

            writer.WritePropertyName("action"); writer.WriteValue(Action);

            writer.WritePropertyName("use_condition"); writer.WriteValue(UseCondition);
            writer.WritePropertyName("condition"); writer.WriteValue(Condition);
            writer.WritePropertyName("condition_argument"); writer.WriteValue(ConditionArgument);

            writer.WritePropertyName("text"); writer.WriteValue(Text);
        }

        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            // Fields
            ColumnStart.ReadJSON(token.SelectToken("column_start"));
            ColumnEnd.ReadJSON(token.SelectToken("column_end"));

            RowStart.ReadJSON(token.SelectToken("row_start"));
            RowEnd.ReadJSON(token.SelectToken("row_end"));
            
            Action = (int)token.SelectToken("action");

            UseCondition = (bool)token.SelectToken("use_condition");
            Condition = (int)token.SelectToken("condition");
            ConditionArgument = (string)token.SelectToken("condition_argument");

            Text = (string)token.SelectToken("text");
        }
    }

}

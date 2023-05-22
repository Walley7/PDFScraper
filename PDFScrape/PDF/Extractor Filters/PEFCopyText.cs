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

    public class PEFCopyText : PDFExtractorFilter {
        //================================================================================
        public enum ActionType {
            OVERWRITE,
            APPEND_START,
            APPEND_END
        }


        //================================================================================
        protected ScrapeColumnSpecifier         mColumnFrom = new ScrapeColumnSpecifier(0);
        protected ScrapeColumnSpecifier         mColumnTo = new ScrapeColumnSpecifier(0, ScrapeColumnSpecifier.RelativePoint.END);

        protected ScrapeRowSpecifier            mRowStart = new ScrapeRowSpecifier(0);
        protected ScrapeRowSpecifier            mRowEnd = new ScrapeRowSpecifier(0, ScrapeRowSpecifier.RelativePoint.END);
        
        protected ActionType                    mAction = ActionType.OVERWRITE;

        protected string                        mPadding = " ";

        
        //================================================================================
        //--------------------------------------------------------------------------------
        public PEFCopyText() : base() { }


        // FILTERING ================================================================================
        //--------------------------------------------------------------------------------
        public override ScrapeTable Apply(ScrapeTable table) {
            // Checks
            if (table.ColumnCount == 0 || table.RowCount == 0)
                return table;

            // Settings
            int columnFrom = Math.Max(Math.Min(ColumnFrom.ColumnIndex(table), table.ColumnCount - 1), 0);
            int columnTo = Math.Max(Math.Min(ColumnTo.ColumnIndex(table), table.ColumnCount - 1), 0);

            int rowStart = Math.Max(Math.Min(RowStart.RowIndex(table), table.RowCount - 1), 0);
            int rowEnd = Math.Max(Math.Min(RowEnd.RowIndex(table), table.RowCount - 1), 0);
            if (rowEnd < rowStart)
                UGeneral.Swap(ref rowStart, ref rowEnd);

            // New table
            ScrapeTable newTable = new ScrapeTable(table);

            // Apply
            for (int i = rowStart; i <= rowEnd; ++i) {
                string toCellValue = newTable.Row(i).Get(columnTo);
                string fromCellValue = newTable.Row(i).Get(columnFrom);
                bool bothHaveValue = !string.IsNullOrEmpty(toCellValue) && !string.IsNullOrEmpty(fromCellValue);

                switch (mAction) {
                    case ActionType.OVERWRITE:      newTable.Row(i).Set(columnTo, fromCellValue); break;
                    case ActionType.APPEND_START:   newTable.Row(i).Set(columnTo, fromCellValue + (bothHaveValue ? Padding : "") + toCellValue); break;
                    case ActionType.APPEND_END:     newTable.Row(i).Set(columnTo, toCellValue + (bothHaveValue ? Padding : "") + fromCellValue); break;
                }
            }

            return newTable;
        }


        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public override string TypeName { get { return "Copy Text"; } }

            
        // SETTINGS ================================================================================
        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Action", Width = 100, ListValues = new string[] { "Overwrite", "Append to start", "Append to end" }, Control = APDFFilterSetting.ControlType.LIST)]
        public int Action {
            set {
                mAction = (ActionType)value;
                OnPropertyChanged("Action");
            }
            get { return (int)mAction; }
        }

        //--------------------------------------------------------------------------------
        public ScrapeColumnSpecifier ColumnFrom { get { return mColumnFrom; } }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "From column", Control = APDFFilterSetting.ControlType.COLUMN_RELATIVE_POINT)]
        public int ColumnFromRelativeTo {
            set {
                mColumnFrom.RelativeTo = (ScrapeColumnSpecifier.RelativePoint)value;
                OnPropertyChanged("ColumnFromRelativeTo");
            }
            get { return (int)mColumnFrom.RelativeTo; }
        }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Control = APDFFilterSetting.ControlType.POSITIVE_INTEGER)]
        public int ColumnFromPosition {
            set {
                mColumnFrom.DisplayPosition = value;
                OnPropertyChanged("ColumnFromPosition");
            }
            get { return mColumnFrom.DisplayPosition; }
        }
        
        //--------------------------------------------------------------------------------
        public ScrapeColumnSpecifier ColumnTo { get { return mColumnTo; } }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "To column", Padding = -5, Control = APDFFilterSetting.ControlType.COLUMN_RELATIVE_POINT)]
        public int ColumnToRelativeTo {
            set {
                mColumnTo.RelativeTo = (ScrapeColumnSpecifier.RelativePoint)value;
                OnPropertyChanged("ColumnToRelativeTo");
            }
            get { return (int)mColumnTo.RelativeTo; }
        }
        
        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Control = APDFFilterSetting.ControlType.POSITIVE_INTEGER)]
        public int ColumnToPosition {
            set {
                mColumnTo.DisplayPosition = value;
                OnPropertyChanged("ColumnToPosition");
            }
            get { return mColumnTo.DisplayPosition; }
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
        [APDFFilterSetting(Display = "Padding", Control = APDFFilterSetting.ControlType.STRING)]
        public string Padding {
            set { SetProperty("Padding", ref mPadding, value); }
            get { return mPadding; }
        }
        
        //--------------------------------------------------------------------------------
        public override float SettingsRows { get { return 2.0f; } }
        
        
        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public override void WriteJSON(JsonTextWriter writer) {
            // Fields
            writer.WritePropertyName("column_from"); ColumnFrom.WriteJSON(writer);
            writer.WritePropertyName("column_to"); ColumnTo.WriteJSON(writer);

            writer.WritePropertyName("row_start"); RowStart.WriteJSON(writer);
            writer.WritePropertyName("row_end"); RowEnd.WriteJSON(writer);

            writer.WritePropertyName("action"); writer.WriteValue((int)Action);
            writer.WritePropertyName("padding"); writer.WriteValue(Padding);
        }

        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            // Fields
            ColumnFrom.ReadJSON(token.SelectToken("column_start"));
            ColumnTo.ReadJSON(token.SelectToken("column_end"));

            RowStart.ReadJSON(token.SelectToken("row_start"));
            RowEnd.ReadJSON(token.SelectToken("row_end"));
            
            Action = (int)token.SelectToken("action");
            Padding = (string)token.SelectToken("padding");
        }
    }

}

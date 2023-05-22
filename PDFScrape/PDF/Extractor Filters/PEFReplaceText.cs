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

    public class PEFReplaceText : PDFExtractorFilter {
        //================================================================================
        protected ScrapeColumnSpecifier         mColumnStart = new ScrapeColumnSpecifier(0);
        protected ScrapeColumnSpecifier         mColumnEnd = new ScrapeColumnSpecifier(0, ScrapeColumnSpecifier.RelativePoint.END);

        protected ScrapeRowSpecifier            mRowStart = new ScrapeRowSpecifier(0);
        protected ScrapeRowSpecifier            mRowEnd = new ScrapeRowSpecifier(0, ScrapeRowSpecifier.RelativePoint.END);

        protected string                        mFindText = "";
        protected string                        mReplaceText = "";


        //================================================================================
        //--------------------------------------------------------------------------------
        public PEFReplaceText() : base() { }


        // FILTERING ================================================================================
        //--------------------------------------------------------------------------------
        public override ScrapeTable Apply(ScrapeTable table) {
            // Checks
            if (table.ColumnCount == 0 || table.RowCount == 0)
                return table;
            if (string.IsNullOrEmpty(mFindText))
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
                    newTable.Row(i).Set(j, newTable.Row(i).Get(j).Replace(FindText, ReplaceText));
                }
            }

            return newTable;
        }


        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public override string TypeName { get { return "Replace Text"; } }


        // SETTINGS ================================================================================
        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Find", Width = 100, Control = APDFFilterSetting.ControlType.STRING)]
        public string FindText {
            set { SetProperty("FindText", ref mFindText, value); }
            get { return mFindText; }
        }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Replace", Width = 100, Control = APDFFilterSetting.ControlType.STRING)]
        public string ReplaceText {
            set { SetProperty("ReplaceText", ref mReplaceText, value); }
            get { return mReplaceText; }
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
        public override float SettingsRows { get { return 2.0f; } }
        
        
        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public override void WriteJSON(JsonTextWriter writer) {
            // Fields
            writer.WritePropertyName("column_start"); ColumnStart.WriteJSON(writer);
            writer.WritePropertyName("column_end"); ColumnEnd.WriteJSON(writer);

            writer.WritePropertyName("row_start"); RowStart.WriteJSON(writer);
            writer.WritePropertyName("row_end"); RowEnd.WriteJSON(writer);
            
            writer.WritePropertyName("find_text"); writer.WriteValue(FindText);
            writer.WritePropertyName("replace_text"); writer.WriteValue(ReplaceText);
        }

        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            // Fields
            ColumnStart.ReadJSON(token.SelectToken("column_start"));
            ColumnEnd.ReadJSON(token.SelectToken("column_end"));

            RowStart.ReadJSON(token.SelectToken("row_start"));
            RowEnd.ReadJSON(token.SelectToken("row_end"));

            FindText = (string)token.SelectToken("find_text");
            ReplaceText = (string)token.SelectToken("replace_text");
        }
    }

}

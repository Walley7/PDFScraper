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

    public class PEFMergeRows : PDFExtractorFilter {
        //================================================================================
        protected ScrapeRowSpecifier            mRowStart = new ScrapeRowSpecifier(0);
        protected ScrapeRowSpecifier            mRowEnd = new ScrapeRowSpecifier(0);

        protected string                        mPadding = " ";
        protected bool                          mNewLinePerRow = false;

   
        //================================================================================
        //--------------------------------------------------------------------------------
        public PEFMergeRows() : base() { }


        // FILTERING ================================================================================
        //--------------------------------------------------------------------------------
        public override ScrapeTable Apply(ScrapeTable table) {
            // Checks
            if (table.RowCount == 0)
                return table;

            // Settings
            int rowStart = Math.Max(Math.Min(RowStart.RowIndex(table), table.RowCount - 1), 0);
            int rowEnd = Math.Max(Math.Min(RowEnd.RowIndex(table), table.RowCount - 1), 0);
            if (rowEnd < rowStart)
                UGeneral.Swap(ref rowStart, ref rowEnd);
            
            // Apply
            ScrapeTable newTable = new ScrapeTable(table);
            newTable.MergeRows(rowStart, rowEnd, Padding, NewLinePerRow);
            return newTable;
        }

        
        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public override string TypeName { get { return "Merge Rows"; } }
        
        
        // SETTINGS ================================================================================
        //--------------------------------------------------------------------------------
        public ScrapeRowSpecifier RowStart { get { return mRowStart; } }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Rows", Control = APDFFilterSetting.ControlType.COLUMN_RELATIVE_POINT)]
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
        [APDFFilterSetting(Display = "to", Padding = -5, Control = APDFFilterSetting.ControlType.COLUMN_RELATIVE_POINT)]
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
        [APDFFilterSetting(Display = "New line per row", LabelInFront = false, Padding = 8, Control = APDFFilterSetting.ControlType.BOOLEAN)]
        public bool NewLinePerRow {
            set {
                SetProperty("NewLinePerRow", ref mNewLinePerRow, value);
                if (mNewLinePerRow && Padding.Equals(" "))
                    Padding = "";
                else if (!mNewLinePerRow && Padding.Equals(""))
                    Padding = " ";
            }
            get { return mNewLinePerRow; }
        }
        
        
        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public override void WriteJSON(JsonTextWriter writer) {
            // Fields
            writer.WritePropertyName("row_start"); RowStart.WriteJSON(writer);
            writer.WritePropertyName("row_end"); RowEnd.WriteJSON(writer);

            writer.WritePropertyName("padding"); writer.WriteValue(Padding);
            writer.WritePropertyName("new_line_per_row"); writer.WriteValue(NewLinePerRow);
        }

        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            // Fields
            RowStart.ReadJSON(token.SelectToken("row_start"));
            RowEnd.ReadJSON(token.SelectToken("row_end"));

            Padding = (string)token.SelectToken("padding");
            NewLinePerRow = (bool)token.SelectToken("new_line_per_row");
        }
    }
}

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

    public class PEFMergeColumns : PDFColumnExtractorFilter {
        //================================================================================
        protected ScrapeColumnSpecifier         mColumnStart = new ScrapeColumnSpecifier(0);
        protected ScrapeColumnSpecifier         mColumnEnd = new ScrapeColumnSpecifier(0);
        
        protected string                        mPadding = " ";
        protected bool                          mMergeHeaders = false;


        //================================================================================
        //--------------------------------------------------------------------------------
        public PEFMergeColumns() : base() { }


        // FILTERING ================================================================================
        //--------------------------------------------------------------------------------
        public override ScrapeTable Apply(ScrapeTable table) {
            // Checks
            if (table.ColumnCount == 0)
                return table;

            // Settings
            int columnStart = Math.Max(Math.Min(ColumnStart.ColumnIndex(table), table.ColumnCount - 1), 0);
            int columnEnd = Math.Max(Math.Min(ColumnEnd.ColumnIndex(table), table.ColumnCount - 1), 0);
            if (columnEnd < columnStart)
                UGeneral.Swap(ref columnStart, ref columnEnd);
            
            // Apply
            ScrapeTable newTable = new ScrapeTable(table);
            newTable.MergeColumns(columnStart, columnEnd, Padding, MergeHeaders);
            return newTable;
        }


        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public override string TypeName { get { return "Merge Columns"; } }
        

        // SETTINGS ================================================================================
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
        [APDFFilterSetting(Display = "Padding", Control = APDFFilterSetting.ControlType.STRING)]
        public string Padding {
            set { SetProperty("Padding", ref mPadding, value); }
            get { return mPadding; }
        }
        
        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Merge headers", LabelInFront = false, Padding = 8, Control = APDFFilterSetting.ControlType.BOOLEAN)]
        public bool MergeHeaders {
            set { SetProperty("MergeHeaders", ref mMergeHeaders, value); }
            get { return mMergeHeaders; }
        }


        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public override void WriteJSON(JsonTextWriter writer) {
            // Base
            base.WriteJSON(writer);

            // Fields
            writer.WritePropertyName("column_start"); ColumnStart.WriteJSON(writer);
            writer.WritePropertyName("column_end"); ColumnEnd.WriteJSON(writer);
            writer.WritePropertyName("padding"); writer.WriteValue(Padding);
            writer.WritePropertyName("merge_headers"); writer.WriteValue(MergeHeaders);
        }

        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            // Base
            base.ReadJSON(token);

            // Fields
            ColumnStart.ReadJSON(token.SelectToken("column_start"));
            ColumnEnd.ReadJSON(token.SelectToken("column_end"));
            Padding = (string)token.SelectToken("padding");
            MergeHeaders = (bool)token.SelectToken("merge_headers");
        }
    }

}

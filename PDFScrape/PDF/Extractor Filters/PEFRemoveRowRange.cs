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

    public class PEFRemoveRowRange : PDFExtractorFilter {
        //================================================================================
        protected ScrapeRowSpecifier            mRowStart = new ScrapeRowSpecifier(0);
        protected ScrapeRowSpecifier            mRowEnd = new ScrapeRowSpecifier(0);


        //================================================================================
        //--------------------------------------------------------------------------------
        public PEFRemoveRowRange() : base() { }


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
            newTable.RemoveRows(rowStart, rowEnd);
            return newTable;
        }


        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public override string TypeName { get { return "Remove Row Range"; } }


        // SETTINGS ================================================================================
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
        
        
        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public override void WriteJSON(JsonTextWriter writer) {
            // Fields
            writer.WritePropertyName("row_start"); RowStart.WriteJSON(writer);
            writer.WritePropertyName("row_end"); RowEnd.WriteJSON(writer);
        }

        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            // Fields
            RowStart.ReadJSON(token.SelectToken("row_start"));
            RowEnd.ReadJSON(token.SelectToken("row_end"));
        }
    }

}

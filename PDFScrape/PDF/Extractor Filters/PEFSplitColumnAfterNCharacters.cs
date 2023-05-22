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

    public class PEFSplitColumnAfterNCharacters : PDFColumnExtractorFilter {
        //================================================================================
        protected ScrapeColumnSpecifier         mColumn = new ScrapeColumnSpecifier(0);

        protected int                           mN = 1;

        protected bool                          mLimitSplits = false;
        protected int                           mMaxSplits = 1;
        protected bool                          mStartFromEnd = false;

        
        //================================================================================
        //--------------------------------------------------------------------------------
        public PEFSplitColumnAfterNCharacters() : base() { }


        // FILTERING ================================================================================
        //--------------------------------------------------------------------------------
        public override ScrapeTable Apply(ScrapeTable table) {
            // Settings
            int column = Column.ColumnIndex(table);
            if (column < 0 || column >= table.ColumnCount)
                return table;

            int maxSplits = LimitSplits ? MaxSplits : -1;

            ScrapeTable.SplitOptions options = 0;
            if (StartFromEnd)
                options |= ScrapeTable.SplitOptions.START_FROM_END;

            // Apply
            ScrapeTable newTable = new ScrapeTable(table);
            newTable.SplitColumnAfterNCharacters(column, N, maxSplits, NewHeaderPrefix, options);
            return newTable;
        }


        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public override string TypeName { get { return "Split Column After # Characters"; } }


        // SETTINGS ================================================================================
        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Naming", Width = 100, Control = APDFFilterSetting.ControlType.STRING)]
        public override string NewHeaderPrefix {
            set { base.NewHeaderPrefix = value; }
            get { return base.NewHeaderPrefix; }
        }

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
        [APDFFilterSetting(Display = "#", Control = APDFFilterSetting.ControlType.POSITIVE_INTEGER)]
        public int N {
            set { SetProperty("N", ref mN, value); }
            get { return mN; }
        }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Limit splits", LabelInFront = false, Padding = 8, Control = APDFFilterSetting.ControlType.BOOLEAN)]
        public bool LimitSplits {
            set { SetProperty("LimitSplits", ref mLimitSplits, value); }
            get { return mLimitSplits; }
        }
        
        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "", Control = APDFFilterSetting.ControlType.POSITIVE_INTEGER)]
        public int MaxSplits {
            set { SetProperty("MaxSplits", ref mMaxSplits, value); }
            get { return mMaxSplits; }
        }
        
        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Start from end", LabelInFront = false, Padding = 8, Control = APDFFilterSetting.ControlType.BOOLEAN)]
        public bool StartFromEnd {
            set { SetProperty("StartFromEnd", ref mStartFromEnd, value); }
            get { return mStartFromEnd; }
        }
        
        
        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public override void WriteJSON(JsonTextWriter writer) {
            // Base
            base.WriteJSON(writer);

            // Fields
            writer.WritePropertyName("column"); Column.WriteJSON(writer);

            writer.WritePropertyName("n"); writer.WriteValue(N);

            writer.WritePropertyName("limit_splits"); writer.WriteValue(LimitSplits);
            writer.WritePropertyName("max_splits"); writer.WriteValue(MaxSplits);
            writer.WritePropertyName("start_from_end"); writer.WriteValue(StartFromEnd);
        }

        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            // Base
            base.ReadJSON(token);

            // Fields
            Column.ReadJSON(token.SelectToken("column"));

            N = (int)token.SelectToken("n");

            LimitSplits = (bool)token.SelectToken("limit_splits");
            MaxSplits = (int)token.SelectToken("max_splits");
            StartFromEnd = (bool)token.SelectToken("start_from_end");
        }
    }

}

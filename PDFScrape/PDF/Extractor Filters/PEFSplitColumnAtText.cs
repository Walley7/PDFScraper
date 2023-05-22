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

    public class PEFSplitColumnAtText : PDFColumnExtractorFilter {
        //================================================================================
        protected ScrapeColumnSpecifier         mColumn = new ScrapeColumnSpecifier(0);

        protected string                        mText = "";

        protected bool                          mLimitSplits = false;
        protected int                           mMaxSplits = 1;
        protected bool                          mCaseSensitive = true;
        protected bool                          mStartFromEnd = false;
        protected bool                          mKeepSeparator = false;
        protected bool                          mSeparatorToPrevious = false;
        protected bool                          mSeparatorToOwn = false;


        //================================================================================
        //--------------------------------------------------------------------------------
        public PEFSplitColumnAtText() : base() { }


        // FILTERING ================================================================================
        //--------------------------------------------------------------------------------
        public override ScrapeTable Apply(ScrapeTable table) {
            // Settings
            int column = Column.ColumnIndex(table);
            if (column < 0 || column >= table.ColumnCount)
                return table;
            if (string.IsNullOrEmpty(Text))
                return table;

            int maxSplits = LimitSplits ? MaxSplits : -1;

            ScrapeTable.SplitOptions options = 0;
            if (!CaseSensitive)
                options |= ScrapeTable.SplitOptions.CASE_INSENSITIVE;
            if (StartFromEnd)
                options |= ScrapeTable.SplitOptions.START_FROM_END;
            if (KeepSeparator)
                options |= ScrapeTable.SplitOptions.KEEP_SEPARATOR;
            if (SeparatorToPrevious)
                options |= ScrapeTable.SplitOptions.SEPARATOR_TO_PREVIOUS;
            if (SeparatorToOwn)
                options |= ScrapeTable.SplitOptions.SEPARATOR_TO_OWN;

            // Apply
            ScrapeTable newTable = new ScrapeTable(table);
            newTable.SplitColumn(column, Text, maxSplits, NewHeaderPrefix, options);
            return newTable;
        }


        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public override string TypeName { get { return "Split Column At Text"; } }


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
        [APDFFilterSetting(Display = "Text", Width = 100, Control = APDFFilterSetting.ControlType.STRING)]
        public string Text {
            set { SetProperty("Text", ref mText, value); }
            get { return mText; }
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
        [APDFFilterSetting(Display = "Case sensitive", LabelInFront = false, Padding = 8, Control = APDFFilterSetting.ControlType.BOOLEAN)]
        public bool CaseSensitive {
            set { SetProperty("CaseSensitive", ref mCaseSensitive, value); }
            get { return mCaseSensitive; }
        }
        
        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Start from end", LabelInFront = false, Control = APDFFilterSetting.ControlType.BOOLEAN)]
        public bool StartFromEnd {
            set { SetProperty("StartFromEnd", ref mStartFromEnd, value); }
            get { return mStartFromEnd; }
        }
        
        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "Keep text", LabelInFront = false, Control = APDFFilterSetting.ControlType.BOOLEAN)]
        public bool KeepSeparator {
            set { SetProperty("KeepSeparator", ref mKeepSeparator, value); }
            get { return mKeepSeparator; }
        }
        
        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "To left", LabelInFront = false, Control = APDFFilterSetting.ControlType.BOOLEAN)]
        public bool SeparatorToPrevious {
            set { SetProperty("SeparatorToPrevious", ref mSeparatorToPrevious, value); }
            get { return mSeparatorToPrevious; }
        }
        
        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "To own", LabelInFront = false, Control = APDFFilterSetting.ControlType.BOOLEAN)]
        public bool SeparatorToOwn {
            set { SetProperty("SeparatorToOwn", ref mSeparatorToOwn, value); }
            get { return mSeparatorToOwn; }
        }

        //--------------------------------------------------------------------------------
        public override float SettingsRows { get { return 2.0f; } }
        
        
        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public override void WriteJSON(JsonTextWriter writer) {
            // Base
            base.WriteJSON(writer);

            // Fields
            writer.WritePropertyName("column"); Column.WriteJSON(writer);

            writer.WritePropertyName("text"); writer.WriteValue(Text);

            writer.WritePropertyName("limit_splits"); writer.WriteValue(LimitSplits);
            writer.WritePropertyName("max_splits"); writer.WriteValue(MaxSplits);
            writer.WritePropertyName("case_sensitive"); writer.WriteValue(CaseSensitive);
            writer.WritePropertyName("start_from_end"); writer.WriteValue(StartFromEnd);
            writer.WritePropertyName("keep_separator"); writer.WriteValue(KeepSeparator);
            writer.WritePropertyName("separator_to_previous"); writer.WriteValue(SeparatorToPrevious);
            writer.WritePropertyName("separator_to_own"); writer.WriteValue(SeparatorToOwn);
        }

        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            // Base
            base.ReadJSON(token);

            // Fields
            Column.ReadJSON(token.SelectToken("column"));

            Text = (string)token.SelectToken("text");
            
            LimitSplits = (bool)token.SelectToken("limit_splits");
            MaxSplits = (int)token.SelectToken("max_splits");
            CaseSensitive = (bool)token.SelectToken("case_sensitive");
            StartFromEnd = (bool)token.SelectToken("start_from_end");
            KeepSeparator = (bool)token.SelectToken("keep_separator");
            SeparatorToPrevious = (bool)token.SelectToken("separator_to_previous");
            SeparatorToOwn = (bool)token.SelectToken("separator_to_own");
        }
    }
}

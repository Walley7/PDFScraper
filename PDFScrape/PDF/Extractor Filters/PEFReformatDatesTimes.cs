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

    public class PEFReformatDatesTimes : PDFExtractorFilter {
        //================================================================================
        protected ScrapeColumnSpecifier         mColumnStart = new ScrapeColumnSpecifier(0);
        protected ScrapeColumnSpecifier         mColumnEnd = new ScrapeColumnSpecifier(0, ScrapeColumnSpecifier.RelativePoint.END);

        protected ScrapeRowSpecifier            mRowStart = new ScrapeRowSpecifier(0);
        protected ScrapeRowSpecifier            mRowEnd = new ScrapeRowSpecifier(0, ScrapeRowSpecifier.RelativePoint.END);
        
        protected bool                          mUseFromFormat;
        protected string                        mFromFormat = "";
        protected string                        mToFormat = "";


        //================================================================================
        //--------------------------------------------------------------------------------
        public PEFReformatDatesTimes() : base() { }


        // FILTERING ================================================================================
        //--------------------------------------------------------------------------------
        public override ScrapeTable Apply(ScrapeTable table) {
            //https://www.c-sharpcorner.com/blogs/date-and-time-format-in-c-sharp-programming1
            
            // add a button, that shows message for date rules
            // "Message button".
            // Caption: "Help"


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
                    string value = newTable.Row(i).Get(j);

                    // Reformat
                    if (!UseFromFormat) {
                        try {
                            DateTime dateTimeValue = DateTime.Parse(value);
                            newTable.Row(i).Set(j, dateTimeValue.ToString(ToFormat));
                        }
                        catch (Exception) { } // Ignore errors
                    }
                }
            }
            
            return newTable;
        }


        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public override string TypeName { get { return "Reformat Dates / Times"; } }


        // SETTINGS ================================================================================
        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "From", Width = 100, LabelInFront = false, Control = APDFFilterSetting.ControlType.BOOLEAN)]
        public bool UseFromFormat {
            set { SetProperty("UseFromFormat", ref mUseFromFormat, value); }
            get { return mUseFromFormat; }
        }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "", Control = APDFFilterSetting.ControlType.STRING)]
        public string FromFormat {
            set { SetProperty("FromFormat", ref mFromFormat, value); }
            get { return mFromFormat; }
        }

        //--------------------------------------------------------------------------------
        [APDFFilterSetting(Display = "To", Width = 100, Control = APDFFilterSetting.ControlType.STRING)]
        public string ToFormat {
            set { SetProperty("ToFormat", ref mToFormat, value); }
            get { return mToFormat; }
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
        [APDFFilterSetting(Caption = "? Help", Width = 60, Padding = 15, LabelInFront = false, Control = APDFFilterSetting.ControlType.MESSAGE_BUTTON)]
        public string HelpMessage {
            get {
                return "DATE / TIME FORMATTING:\n" +
                       " y:\tYear (0-99).\n" +
                       " yy:\tYear (00-99).\n" +
                       " yyy:\tYear (2000).\n" +
                       " yyyy:\tYear (2000).\n" +
                       "\n" +
                       " M:\tMonth (1-12).\n" +
                       " MM:\tMonth (01-12).\n" +
                       " MMM:\tAbbreviated name of month (Nov, Dec).\n" +
                       " MMMM:\tName of month (November, December).\n" +
                       "\n" +
                       " d:\tDay (1 - 31).\n" +
                       " dd:\tDay (01 - 31).\n" +
                       " ddd:\tAbbreviated name of day (Mon, Tue).\n" +
                       " dddd:\tName of day (Monday, Tuesday).\n" +
                       "\n" +
                       " h:\tHour (1 - 12).\n" +
                       " hh:\tHour (01 - 12).\n" +
                       "\n" +
                       " H:\tHour (0 - 23).\n" +
                       " HH:\tHour (00 - 23).\n" +
                       "\n" +
                       " m:\tMinute (0 - 59).\n" +
                       " mm:\tMinute (00 - 59).\n" +
                       "\n" +
                       " s:\tSecond (0 - 59).\n" +
                       " ss:\tSecond (00 - 59).\n" +
                       "\n" +
                       " t:\tAbbreviated AM / PM (A, P).\n" +
                       " tt:\tAM / PM (AM, PM).";
            }
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

            writer.WritePropertyName("use_from_format"); writer.WriteValue(UseFromFormat);
            writer.WritePropertyName("from_format"); writer.WriteValue(FromFormat);
            writer.WritePropertyName("to_format"); writer.WriteValue(ToFormat);
        }

        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            // Fields
            ColumnStart.ReadJSON(token.SelectToken("column_start"));
            ColumnEnd.ReadJSON(token.SelectToken("column_end"));
            
            RowStart.ReadJSON(token.SelectToken("row_start"));
            RowEnd.ReadJSON(token.SelectToken("row_end"));
            
            UseFromFormat = (bool)token.SelectToken("use_from_format");
            FromFormat = (string)token.SelectToken("from_format");
            ToFormat = (string)token.SelectToken("to_format");
        }
    }

}

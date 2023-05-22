using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDFScrape.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Data {

    public class ScrapeColumnSpecifier {
        //================================================================================
        public enum RelativePoint {
            START = 0,
            END = 1
        }


        //================================================================================
        public RelativePoint                    mRelativeTo = RelativePoint.START;
        public int                              mPosition = 0;
        
        //--------------------------------------------------------------------------------        
        public event EventDelegate              Changed = delegate { };


        //================================================================================
        //--------------------------------------------------------------------------------
        public ScrapeColumnSpecifier(int position, RelativePoint relativeTo = RelativePoint.START) {
            mRelativeTo = relativeTo;
            mPosition = position;
        }
        
    
        // COLUMN ================================================================================
        //--------------------------------------------------------------------------------
        public RelativePoint RelativeTo {
            set {
                mRelativeTo = value;
                Changed(this);
            }
            get { return mRelativeTo; }
        }

        //--------------------------------------------------------------------------------
        public int Position {
            set {
                mPosition = value;
                Changed(this);
            }
            get { return mPosition; }
        }

        //--------------------------------------------------------------------------------
        public int DisplayPosition {
            set { Position = value - 1; }
            get { return Position + 1; }
        }
        
        //--------------------------------------------------------------------------------
        public int ColumnIndex(ScrapeTable table) {
            switch (mRelativeTo) {
                case RelativePoint.START:
                    return mPosition;
                case RelativePoint.END:
                    return table.ColumnCount - mPosition - 1;
                default:
                    return 0;
            }
        }
        
        //--------------------------------------------------------------------------------
        public int ColumnIndex(ScrapeRow row) {
            switch (mRelativeTo) {
                case RelativePoint.START:
                    return mPosition;
                case RelativePoint.END:
                    return row.ColumnCount - mPosition - 1;
                default:
                    return 0;
            }
        }
        
    
        // COMPARISON ================================================================================
        //--------------------------------------------------------------------------------
        public bool Equals(ScrapeTable table, ScrapeColumnSpecifier other) { return (ColumnIndex(table) == other.ColumnIndex(table)); }
        public bool LessThanOrEqual(ScrapeTable table, ScrapeColumnSpecifier other) { return (ColumnIndex(table) <= other.ColumnIndex(table)); }
        public bool LessThan(ScrapeTable table, ScrapeColumnSpecifier other) { return (ColumnIndex(table) < other.ColumnIndex(table)); }
        public bool GreaterThanOrEqual(ScrapeTable table, ScrapeColumnSpecifier other) { return (ColumnIndex(table) >= other.ColumnIndex(table)); }
        public bool GreaterThan(ScrapeTable table, ScrapeColumnSpecifier other) { return (ColumnIndex(table) > other.ColumnIndex(table)); }


        // STRING ================================================================================
        //--------------------------------------------------------------------------------
        public override string ToString() {
            string text = mPosition.ToString();

            switch (mRelativeTo) {
                case RelativePoint.START:   text += " from start"; break;
                case RelativePoint.END:     text += " from end"; break;
            }

            return text;
        }


        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public void WriteJSON(JsonTextWriter writer) {
            // Start
            writer.WriteStartObject();

            // Specifier
            writer.WritePropertyName("relative_to");
            writer.WriteValue((int)RelativeTo);
            writer.WritePropertyName("position");
            writer.WriteValue(Position);

            // End
            writer.WriteEndObject();
        }

        //--------------------------------------------------------------------------------
        public void ReadJSON(JToken token) {
            RelativeTo = (RelativePoint)((int)token.SelectToken("relative_to"));
            Position = (int)token.SelectToken("position");
        }


        //================================================================================
        //********************************************************************************
        public delegate void EventDelegate(ScrapeColumnSpecifier column);
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.PDF.Attributes {

    public class APDFFilterSetting : Attribute {
        //================================================================================
        public enum ControlType {
            STRING,
            BOOLEAN,
            INTEGER,
            POSITIVE_INTEGER,
            POSITIVE_ZERO_INTEGER,
            LIST,
            COLUMN_RELATIVE_POINT,
            ROW_RELATIVE_POINT,
            CONDITION,
            MESSAGE_BUTTON
        }


        //================================================================================


        //================================================================================
        //--------------------------------------------------------------------------------
        public string Display { get; set; }
        public ControlType Control { get; set; }
        public string Caption { get; set; } = "";
        public bool LabelInFront { get; set; } = true;
        public int Width { get; set; } = 50;
        public int Padding { get; set; } = 3;
        public string[] ListValues { get; set; } = new string[] { };
    }

}

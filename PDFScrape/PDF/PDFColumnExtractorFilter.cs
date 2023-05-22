using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDFScrape.PDF.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.PDF {

    public class PDFColumnExtractorFilter : PDFExtractorFilter {
        //================================================================================
        protected string                        mNewHeaderPrefix = "";


        //================================================================================
        //--------------------------------------------------------------------------------
        public PDFColumnExtractorFilter() : base() { }


        // SETTINGS ================================================================================
        //--------------------------------------------------------------------------------
        public virtual string NewHeaderPrefix {
            set { SetProperty("NewHeaderPrefix", ref mNewHeaderPrefix, value); }
            get { return mNewHeaderPrefix; }
        }


        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public override void WriteJSON(JsonTextWriter writer) {
            writer.WritePropertyName("new_header_prefix");
            writer.WriteValue(NewHeaderPrefix);
        }
        
        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            mNewHeaderPrefix = (string)token.SelectToken("new_header_prefix");
        }
    }

}

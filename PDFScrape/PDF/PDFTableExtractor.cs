using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.PDF {

    public class PDFTableExtractor : PDFExtractor {
        //================================================================================
        //--------------------------------------------------------------------------------
        public PDFTableExtractor(PDFScrapeModel model, string name, JToken jsonToken = null) : base(model, name, new PDFRegion(), jsonToken) {

        }


        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public override string TypeString { get { return "Table"; } }
    }

}

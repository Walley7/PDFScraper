using PDFScrape.Data;
using PDFScrape.PDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Scraping {

    public class PDFScraper2 {
        //================================================================================
        private PDFScrapeModel2                  mModel;


        //================================================================================
        //--------------------------------------------------------------------------------
        public PDFScraper2() {
            mModel = new PDFScrapeModel2(this);
        }


        // MODEL ================================================================================
        //--------------------------------------------------------------------------------
        public PDFScrapeModel2 Model { get { return mModel; } }


        // SCRAPING ================================================================================
        //--------------------------------------------------------------------------------
        public DataStoreRecord2 Scrape(PDFReader2 reader, DataStore2 dataStore) {
            return mModel.Scrape(reader, dataStore);
        }
    }

}

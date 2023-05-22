using PDFScrape.Data;
using PDFScrape.PDF;
using PDFScrape.Scraping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Scraping {

    public class PDFScrapeModel2 {
        //================================================================================                      
        private PDFScraper2                              mScraper;

        private List<PDFScrapeRegion2>                   mRegions = new List<PDFScrapeRegion2>();


        //================================================================================
        //--------------------------------------------------------------------------------
        public PDFScrapeModel2(PDFScraper2 scraper) {
            mScraper = scraper;
        }


        // REGIONS ================================================================================
        //--------------------------------------------------------------------------------
        public PDFScrapeRegion2 AddRegion(string name, int page = 1, int x = 0, int y = 0, int width = 0, int height = 0) {
            // Checks
            if (HasRegion(name))
                throw new DuplicateException();
            if (x < 0 || y < 0)
                throw new ArgumentException();

            // Add
            PDFScrapeRegion2 region = new PDFScrapeRegion2(this, name, page, x, y, width, height);
            mRegions.Add(region);
            return region;
        }

        //--------------------------------------------------------------------------------
        public PDFScrapeRegion2 AddRegion(int page = 1, int x = 0, int y = 0, int width = 0, int height = 0) {
            // Name
            string name;
            int i = 1;
            do {
                name = "field" + i++;
            }
            while (HasRegion(name));
            
            // Add
            return AddRegion(name, page, x, y, width, height);
        }

        //--------------------------------------------------------------------------------
        public void RemoveRegion(int index) { mRegions.RemoveAt(index); }
        public void RemoveRegion(PDFScrapeRegion2 region) { mRegions.Remove(region); }

        //--------------------------------------------------------------------------------
        public void RemoveRegion(string name) {
            for (int i = 0; i < mRegions.Count; ++i) {
                if (mRegions[i].Name.Equals(name)) {
                    RemoveRegion(i);
                    break;
                }
            }
        }

        //--------------------------------------------------------------------------------
        public void RemoveAllRegions() {
            mRegions.Clear();
        }

        //--------------------------------------------------------------------------------
        public PDFScrapeRegion2 Region(int index) { return mRegions[index]; }
        
        //--------------------------------------------------------------------------------
        public PDFScrapeRegion2 Region(string name) {
            foreach (PDFScrapeRegion2 r in mRegions) {
                if (r.Name.Equals(name))
                    return r;
            }
            return null;
        }

        //--------------------------------------------------------------------------------
        public bool HasRegion(string name) { return (Region(name) != null); }
        public List<PDFScrapeRegion2> Regions { get { return mRegions; } }


        // SCRAPING ================================================================================
        //--------------------------------------------------------------------------------
        public DataStoreRecord2 Scrape(PDFReader2 reader, DataStore2 dataStore) {
            // Record
            DataStoreRecord2 record = dataStore.AddRecord();

            // Scrape
            foreach (PDFScrapeRegion2 r in mRegions) {
                record.AddValue(r.Name, r.Scrape(reader));
            }

            // Record
            return record;
        }
    }

}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDFScrape.Data;
using PDFScrape.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.PDF {

    public abstract class PDFExtractorFilter : Bindable {
        //================================================================================
        private PDFExtractor                        mExtractor = null;


        //================================================================================
        //--------------------------------------------------------------------------------
        public PDFExtractorFilter() {

        }

        //--------------------------------------------------------------------------------
        public virtual void Dispose() { }


        // EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        internal void OnAdded(PDFExtractor extractor) {
            mExtractor = extractor;
        }
        
        //--------------------------------------------------------------------------------
        internal void OnRemove(PDFExtractor extractor) {
            mExtractor = null;
        }


        // FILTERING ================================================================================
        //--------------------------------------------------------------------------------
        public virtual ScrapeTable Apply(ScrapeTable table) {
            return table;
        }


        // EXTRACTOR ================================================================================
        //--------------------------------------------------------------------------------
        public PDFExtractor Extractor { get { return mExtractor; } }


        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public virtual string TypeName { get { return "Filter"; } }


        // SETTINGS ================================================================================
        //--------------------------------------------------------------------------------
        public virtual float SettingsRows { get { return 1.0f; } }


        // CHANGE COUNT ================================================================================
        //--------------------------------------------------------------------------------
        internal void IncrementChangeCount() {
            if (Extractor != null)
                Extractor.IncrementChangeCount();
        }


        // PROPERTIES ================================================================================
        //--------------------------------------------------------------------------------
        protected override void OnPropertyChanged(string property) {
            base.OnPropertyChanged(property);
            IncrementChangeCount();
        }


        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public abstract void WriteJSON(JsonTextWriter writer);
        public abstract void ReadJSON(JToken token);
    }

}

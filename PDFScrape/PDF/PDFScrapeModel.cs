using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDFScrape.Data;
using PDFScrape.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.PDF {

    public class PDFScrapeModel : Bindable {
        //================================================================================
        public enum ExtractorType {
            TABLE
        }


        //================================================================================
        private string                          mName = "Untitled";

        private string                          mTemplatePDFPath = "";

        private List<PDFExtractor>              mExtractors = new List<PDFExtractor>();

        private long                            mChangeCount = 0;
        
        //--------------------------------------------------------------------------------        
        public event EventDelegate              Changed = delegate { };


        //================================================================================
        //--------------------------------------------------------------------------------
        public PDFScrapeModel(string filePath = null, string name = "Untitled") {
            // Name
            mName = name;

            // JSON
            if (filePath != null)
                LoadFromJSON(filePath);
        }


        // EXTRACTION ================================================================================
        //--------------------------------------------------------------------------------
        public ScrapeSet Extract(PDFReader reader) {
            // Scrape set
            ScrapeSet set = new ScrapeSet();

            // Extractors
            foreach (PDFExtractor e in mExtractors) {
                set.AddTable(e.Extract(reader));
            }
            return set;
        }


        // NAME ================================================================================
        //--------------------------------------------------------------------------------
        public string Name { get { return mName; } }


        // TEMPLATE PDF ================================================================================
        //--------------------------------------------------------------------------------
        public string TemplatePDFPath {
            set {
                IncrementChangeCount();
                SetProperty("TemplatePDFPath", ref mTemplatePDFPath, value);
            }
            get { return mTemplatePDFPath; }
        }


        // EXTRACTORS ================================================================================
        //--------------------------------------------------------------------------------
        private PDFExtractor CreateExtractor(ExtractorType type, string name, JToken jsonToken) {
            switch (type) {
                case ExtractorType.TABLE:   return new PDFTableExtractor(this, name, jsonToken);
                default:                    throw new NotFoundException();
            }
        }

        //--------------------------------------------------------------------------------
        private PDFExtractor _AddExtractor(string name, ExtractorType type, JToken jsonToken) {
            // Create
            PDFExtractor extractor = CreateExtractor(type, name, jsonToken);

            // Checks
            if (HasExtractor(extractor.Name))
                throw new DuplicateException();

            // Add
            mExtractors.Add(extractor);
            IncrementChangeCount();
            return extractor;
        }
        
        //--------------------------------------------------------------------------------
        public PDFExtractor AddExtractor(string name, ExtractorType type) { return _AddExtractor(name, type, null); }
        public PDFExtractor AddExtractor(ExtractorType type, JToken jsonToken) { return _AddExtractor("", type, jsonToken); }

        //--------------------------------------------------------------------------------
        public PDFExtractor AddExtractor(ExtractorType type) {
            // Name
            string name;
            int i = 1;
            do {
                name = "Extractor " + i++;
            }
            while (HasExtractor(name));

            // Add
            return AddExtractor(name, type);
        }

        //--------------------------------------------------------------------------------
        public void RemoveExtractor(int index) {
            // Dispose
            PDFExtractor extractor = mExtractors[index];
            extractor.Dispose();

            // Remove
            mExtractors.RemoveAt(index);

            // Change count
            IncrementChangeCount();
        }

        //--------------------------------------------------------------------------------
        public void RemoveExtractor(PDFExtractor extractor) {
            // Remove / dispose
            mExtractors.Remove(extractor);
            extractor.Dispose();

            // Change count
            IncrementChangeCount();
        }

        //--------------------------------------------------------------------------------
        public void RemoveExtractor(string name) {
            for (int i = 0; i < mExtractors.Count; ++i) {
                if (mExtractors[i].Name.Equals(name)) {
                    RemoveExtractor(i);
                    break;
                }
            }
        }

        //--------------------------------------------------------------------------------
        public void RemoveAllExtractors() {
            // Dispose
            foreach (PDFExtractor e in mExtractors) {
                e.Dispose();
            }

            // Remove all
            mExtractors.Clear();

            // Change count
            IncrementChangeCount();
        }

        //--------------------------------------------------------------------------------
        public PDFExtractor Extractor(int index) { return mExtractors[index]; }

        //--------------------------------------------------------------------------------
        public PDFExtractor Extractor(string name) {
            foreach (PDFExtractor e in mExtractors) {
                if (e.Name.Equals(name))
                    return e;
            }
            return null;
        }

        //--------------------------------------------------------------------------------
        public bool HasExtractor(string name) { return (Extractor(name) != null); }
        public int ExtractorCount { get { return mExtractors.Count; } }
        public List<PDFExtractor> Extractors { get { return mExtractors; } }

        //--------------------------------------------------------------------------------
        public string[] ExtractorNames {
            get {
                string[] names = new string[ExtractorCount];
                for (int i = 0; i < ExtractorCount; ++i) {
                    names[i] = Extractor(i).Name;
                }
                return names;
            }
        }


        // CHANGE COUNT ================================================================================
        //--------------------------------------------------------------------------------
        public void IncrementChangeCount() {
            ++mChangeCount;
            Changed(this);
        }

        //--------------------------------------------------------------------------------
        public void ResetChangeCount() {
            mChangeCount = 0;
        }
        
        //--------------------------------------------------------------------------------
        public long ChangeCount { get { return mChangeCount; } }
        public bool HasChanged { get { return mChangeCount != 0; } }


        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public void SaveToJSON(string filePath) {
            // Save
            StreamWriter streamWriter = new StreamWriter(filePath);
            WriteJSON(new JsonTextWriter(streamWriter));
            streamWriter.Close();
            
            // Name
            mName = Path.GetFileName(filePath);

            // Change count
            ResetChangeCount();
        }

        //--------------------------------------------------------------------------------
        public void LoadFromJSON(string filePath) {
            // Name
            mName = Path.GetFileName(filePath);

            // Load
            StreamReader streamReader = new StreamReader(filePath);
            string json = streamReader.ReadToEnd();
            streamReader.Close();

            // Read
            ReadJSON(json);

            // Change count
            ResetChangeCount();
        }
        
        //--------------------------------------------------------------------------------
        public void WriteJSON(JsonTextWriter writer) {
            // Formatting
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 3;

            // Start
            writer.WriteStartObject();

            // Model
            writer.WritePropertyName("template_pdf_path");
            writer.WriteValue(TemplatePDFPath);

            // Extractors
            writer.WritePropertyName("extractors");
            writer.WriteStartArray();
            foreach (PDFExtractor e in mExtractors) {
                writer.WriteStartObject();

                writer.WritePropertyName("type");
                writer.WriteValue(e.TypeString);
                e.WriteJSON(writer);

                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            // End
            writer.WriteEndObject();
        }
        
        //--------------------------------------------------------------------------------
        public void ReadJSON(string json) {
            // Reset
            mTemplatePDFPath = "";
            RemoveAllExtractors();

            // Parse
            JObject jsonObject = JObject.Parse(json);

            // Model
            mTemplatePDFPath = (string)jsonObject.SelectToken("template_pdf_path");

            // Extractors
            JArray extractors = (JArray)jsonObject.SelectToken("extractors");
            if (extractors != null) {
                foreach (JToken e in extractors) {
                    string type = (string)e.SelectToken("type");
                    switch (type) {
                        case "Table":   AddExtractor(ExtractorType.TABLE, e); break;
                        default:        throw new NotFoundException();
                    }
                }
            }
        }


        //================================================================================
        //********************************************************************************
        public delegate void EventDelegate(PDFScrapeModel model);
    }
}

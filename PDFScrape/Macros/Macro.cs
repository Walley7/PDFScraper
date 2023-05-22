using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDFScrape.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Macros {

    public class Macro : MacroNode {
        //================================================================================
        public enum SourceType {
            EXTRACTOR,
            REPETITION_ROW,
            EXTRACTOR_ROW_AT_REPETITION_POSITION
        }


        //================================================================================
        private string                          mName = "Untitled";

        private string                          mSampleModelPath = "";
        private string                          mSamplePDFPath = "";

        private long                            mChangeCount = 0;
        
        //--------------------------------------------------------------------------------        
        public event EventDelegate              Changed = delegate { };


        //================================================================================
        //--------------------------------------------------------------------------------
        public Macro(string filePath = null, string name = "Untitled") {
            // Name
            mName = name;

            // JSON
            if (filePath != null)
                LoadFromJSON(filePath);
        }


        // MACRO ================================================================================
        //--------------------------------------------------------------------------------
        protected override Macro _Macro { get { return this; } }


        // NAME ================================================================================
        //--------------------------------------------------------------------------------
        public string Name { get { return mName; } }


        // SAMPLE MODEL / PDF ================================================================================
        //--------------------------------------------------------------------------------
        public string SampleModelPath {
            set {
                IncrementChangeCount();
                SetProperty("SampleModelPath", ref mSampleModelPath, value);
            }
            get { return mSampleModelPath; }
        }
        
        //--------------------------------------------------------------------------------
        public string SamplePDFPath {
            set {
                IncrementChangeCount();
                SetProperty("SamplePDFPath", ref mSamplePDFPath, value);
            }
            get { return mSamplePDFPath; }
        }


        // CHANGE COUNT ================================================================================
        //--------------------------------------------------------------------------------
        public override void IncrementChangeCount() {
            ++mChangeCount;
            Changed(this);
        }

        //--------------------------------------------------------------------------------
        public override void ResetChangeCount() {
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
        public override void WriteJSON(JsonTextWriter writer) {
            // Formatting
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 3;

            // Start
            writer.WriteStartObject();

            // Model / pdf
            writer.WritePropertyName("sample_model_path");
            writer.WriteValue(SampleModelPath);
            writer.WritePropertyName("sample_pdf_path");
            writer.WriteValue(SamplePDFPath);

            // Node
            base.WriteJSON(writer);

            // End
            writer.WriteEndObject();
        }

        //--------------------------------------------------------------------------------
        public void ReadJSON(string json) {
            // Reset
            mSampleModelPath = "";

            // Parse
            JObject jsonObject = JObject.Parse(json);

            // Model / pdf
            mSampleModelPath = (string)jsonObject.SelectToken("sample_model_path");
            mSamplePDFPath = (string)jsonObject.SelectToken("sample_pdf_path");

            // Node
            ReadJSON(jsonObject.Root);
        }


        //================================================================================
        //********************************************************************************
        public delegate void EventDelegate(Macro macro);
    }

}
 
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDFScrape.Data;
using PDFScrape.PDF.Extractor_Filters;
using PDFScrape.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PDFScrape.PDF {

    public class PDFExtractor : Bindable {
        //================================================================================
        private static Dictionary<string, Type> sFilterTypes = new Dictionary<string, Type>();

        //--------------------------------------------------------------------------------
        private PDFScrapeModel                  mModel;

        private string                          mName;

        protected PDFRegion                     mRegion = null;

        private List<string>                    mHeaders = new List<string>();

        private bool                            mSplitLinesIntoRows = true;

        private List<PDFExtractorFilter>        mFilters = new List<PDFExtractorFilter>();

        private PDFRegion.EventDelegate         mRegionChangedDelegate = null;


        //================================================================================
        //--------------------------------------------------------------------------------
        static PDFExtractor() {
            // Columns
            sFilterTypes["InsertColumns"] = typeof(PEFInsertColumns);
            sFilterTypes["KeepColumnRange"] = typeof(PEFKeepColumnRange);
            sFilterTypes["RemoveColumnRange"] = typeof(PEFRemoveColumnRange);
            sFilterTypes["RemoveEmptyColumns"] = typeof(PEFRemoveEmptyColumns);
            sFilterTypes["MoveColumn"] = typeof(PEFMoveColumn);
            sFilterTypes["MergeColumns"] = typeof(PEFMergeColumns);
            sFilterTypes["SplitColumnAtText"] = typeof(PEFSplitColumnAtText);
            sFilterTypes["SplitColumnAtWhitespace"] = typeof(PEFSplitColumnAtWhitespace);
            sFilterTypes["SplitColumnAtLineBreaks"] = typeof(PEFSplitColumnAtLineBreaks);
            sFilterTypes["SplitColumnAfterNWords"] = typeof(PEFSplitColumnAfterNWords);
            sFilterTypes["SplitColumnAfterNCharacters"] = typeof(PEFSplitColumnAfterNCharacters);

            // Rows
            sFilterTypes["KeepRowRange"] = typeof(PEFKeepRowRange);
            sFilterTypes["KeepRowsBetween"] = typeof(PEFKeepRowsBetween);
            sFilterTypes["KeepRowsWhere"] = typeof(PEFKeepRowsWhere);
            sFilterTypes["RemoveRowRange"] = typeof(PEFRemoveRowRange);
            sFilterTypes["RemoveRowsBetween"] = typeof(PEFRemoveRowsBetween);
            sFilterTypes["RemoveRowsWhere"] = typeof(PEFRemoveRowsWhere);
            sFilterTypes["MergeRows"] = typeof(PEFMergeRows);

            // Cells
            sFilterTypes["AddSetText"] = typeof(PEFAddSetText);
            sFilterTypes["CopyText"] = typeof(PEFCopyText);
            sFilterTypes["ReplaceText"] = typeof(PEFReplaceText);
            sFilterTypes["ReformatNumbers"] = typeof(PEFReformatNumbers);
            sFilterTypes["ReformatDatesTimes"] = typeof(PEFReformatDatesTimes);
        }


        //================================================================================
        //--------------------------------------------------------------------------------
        public PDFExtractor(PDFScrapeModel model, string name, PDFRegion region, JToken jsonToken = null) {
            // Extractor
            mModel = model;
            mName = name;
            mRegion = region;

            // Change tracking
            if (mRegion != null) {
                mRegionChangedDelegate = (r) => IncrementChangeCount();
                mRegion.Changed += mRegionChangedDelegate;
            }

            // JSON
            if (jsonToken != null)
                ReadJSON(jsonToken);
        }
        
        //--------------------------------------------------------------------------------
        public void Dispose() {
            if (mRegion != null) {
                mRegion.Changed -= mRegionChangedDelegate;
                mRegionChangedDelegate = null;
            }
        }


        // EXTRACTION ================================================================================
        //--------------------------------------------------------------------------------
        public ScrapeTable Extract(PDFReader reader, int filtersToApply = -1) {
            // Scrape
            ScrapeTable scrapeTable = reader.Scrape(mRegion, mName, mSplitLinesIntoRows);

            // Headers
            for (int i = 0; i < scrapeTable.ColumnCount && i < mHeaders.Count; ++i) {
                scrapeTable.SetHeader(i, mHeaders[i]);
            }

            // Filters
            int limit = filtersToApply >= 0 ? filtersToApply : FilterCount;
            for (int i = 0; i < limit; ++i) {
                scrapeTable = Filter(i).Apply(scrapeTable);
            }

            return scrapeTable;
        }


        // MODEL ================================================================================
        //--------------------------------------------------------------------------------
        public PDFScrapeModel Model { get { return mModel; } }


        // NAME ================================================================================
        //--------------------------------------------------------------------------------
        public bool SetName(string name) {
            if (!mModel.HasExtractor(name)) {
                SetProperty("Name", ref mName, name);
                IncrementChangeCount();
                return true;
            }
            else
                return mName.Equals(name);
        }

        //--------------------------------------------------------------------------------
        public string Name { get { return mName; } }


        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public virtual string TypeString { get; }


        // REGION ================================================================================
        //--------------------------------------------------------------------------------
        public PDFRegion Region { get { return mRegion; } }


        // HEADERS ================================================================================
        //--------------------------------------------------------------------------------
        public bool SetHeader(int index, string header) {
            // Checks
            if (header == null)
                header = "";

            // Duplicate check
            if (!header.Equals("")) {
                for (int i = 0; i < mHeaders.Count; ++i) {
                    if (i != index && mHeaders[i].Equals(header))
                        return false;
                }
            }

            // Expand
            while (index >= mHeaders.Count) {
                mHeaders.Add("");
            }

            // Add
            mHeaders[index] = header;

            // Change count
            IncrementChangeCount();
            return true;
        }

        //--------------------------------------------------------------------------------
        public string Header(int index) {
            // Checks
            if (index < 0)
                throw new IndexOutOfRangeException();
            if (index >= mHeaders.Count)
                return "";

            // Header
            return mHeaders[index];
        }

        //--------------------------------------------------------------------------------
        public int HeaderIndex(string header) {
            // Checks
            if (string.IsNullOrEmpty(header))
                return -1;

            // Header index
            for (int i = 0; i < mHeaders.Count; ++i) {
                if (mHeaders[i].Equals(header))
                    return i;
            }
            return -1;
        }

        //--------------------------------------------------------------------------------
        public void SetHeaderGridColumns(DataGridView grid, int columnCount) {
            grid.Columns.Clear();
            for (int i = 0; i < columnCount; ++i) {
                grid.Columns.Add(grid.Name + "Column" + grid.Columns.Count, "Header #" + (i + 1));
            }
        }

        //--------------------------------------------------------------------------------
        public void SetHeaderGridRows(DataGridView grid, int columnCount) {
            // Clear
            grid.Rows.Clear();

            // Headers
            List<string> rowHeaders = new List<string>();
            for (int i = 0; i < columnCount; ++i) {
                if (i < mHeaders.Count)
                    rowHeaders.Add(mHeaders[i]);
                else
                    rowHeaders.Add("");
            }

            // Row
            grid.Rows.Add(rowHeaders.ToArray());
        }


        //--------------------------------------------------------------------------------
        public string[] DistinctHeaderPrefixes {
            get {
                // Headers
                SortedSet<string> headerPrefixes = new SortedSet<string>();

                // Extractor headers
                foreach (string h in mHeaders) {
                    headerPrefixes.Add(h);
                }

                // Filter headers
                foreach (PDFExtractorFilter f in mFilters) {
                    if (f is PDFColumnExtractorFilter) {
                        if (!string.IsNullOrEmpty(((PDFColumnExtractorFilter)f).NewHeaderPrefix))
                            headerPrefixes.Add(((PDFColumnExtractorFilter)f).NewHeaderPrefix);
                    }
                }

                // Sort
                return headerPrefixes.ToArray();
            }
        }


        // SETTINGS ================================================================================
        //--------------------------------------------------------------------------------
        public bool SplitLinesIntoRows {
            set {
                SetProperty("SplitLinesIntoRows", ref mSplitLinesIntoRows, value);
                IncrementChangeCount();
            }
            get { return mSplitLinesIntoRows; }
        }


        // FILTERS ================================================================================
        //--------------------------------------------------------------------------------
        public void AddFilter(int index, PDFExtractorFilter filter) {
            mFilters.Insert(index, filter);
            filter.OnAdded(this);
            IncrementChangeCount();
        }

        //--------------------------------------------------------------------------------
        public void AddFilter(PDFExtractorFilter filter) { AddFilter(mFilters.Count, filter); }

        //--------------------------------------------------------------------------------
        private void AddFilter(string type, JToken jsonToken) {
            ConstructorInfo constructor = sFilterTypes[type].GetConstructor(new Type[] {});
            PDFExtractorFilter filter = (PDFExtractorFilter)constructor.Invoke(new object[] {});
            AddFilter(filter);
            filter.ReadJSON(jsonToken);
        }

        //--------------------------------------------------------------------------------
        public void RemoveFilter(int index) {
            // Remove
            PDFExtractorFilter filter = mFilters[index];
            mFilters.RemoveAt(index);
            filter.OnRemove(this);

            // Dispose
            filter.Dispose();

            // Change count
            IncrementChangeCount();
        }

        //--------------------------------------------------------------------------------
        public void RemoveAllFilters() {
            // Dispose
            foreach (PDFExtractorFilter f in mFilters) {
                f.OnRemove(this);
                f.Dispose();
            }

            // Remove all
            mFilters.Clear();

            // Change count
            IncrementChangeCount();
        }
        
        //--------------------------------------------------------------------------------
        public PDFExtractorFilter Filter(int index) { return mFilters[index]; }
        public int FilterIndex(PDFExtractorFilter filter) { return mFilters.IndexOf(filter); }
        public int FilterCount { get { return mFilters.Count; } }


        // CHANGE COUNT ================================================================================
        //--------------------------------------------------------------------------------
        internal void IncrementChangeCount() {
            if (Model != null)
                Model.IncrementChangeCount();
        }


        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public void WriteJSON(JsonTextWriter writer) {
            // Extractor
            writer.WritePropertyName("name");
            writer.WriteValue(Name);
            writer.WritePropertyName("split_lines_into_rows");
            writer.WriteValue(SplitLinesIntoRows);

            // Region
            writer.WritePropertyName("region");
            Region.WriteJSON(writer);

            // Headers
            writer.WritePropertyName("headers");
            writer.WriteStartArray();
            foreach (string h in mHeaders) {
                writer.WriteValue(h);
            }
            writer.WriteEndArray();

            // Filters
            writer.WritePropertyName("filters");
            writer.WriteStartArray();
            foreach (PDFExtractorFilter f in mFilters) {
                writer.WriteStartObject();

                writer.WritePropertyName("type");
                writer.WriteValue(sFilterTypes.FirstOrDefault(t => t.Value == f.GetType()).Key); //writer.WriteValue(sFilterTypes.IndexOf(f.GetType()));
                f.WriteJSON(writer);

                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }

        //--------------------------------------------------------------------------------
        public void ReadJSON(JToken token) {
            // Reset
            mSplitLinesIntoRows = true;
            mHeaders.Clear();
            mFilters.Clear();

            // Extractor
            mName = (string)token.SelectToken("name");
            mSplitLinesIntoRows = (bool)token.SelectToken("split_lines_into_rows");

            // Region
            mRegion.ReadJSON(token.SelectToken("region"));

            // Headers
            JArray headers = (JArray)token.SelectToken("headers");
            if (headers != null) {
                foreach(JToken h in headers) {
                    mHeaders.Add((string)h);
                }
            }

            // Filters
            JArray filters = (JArray)token.SelectToken("filters");
            if (filters != null) {
                foreach(JToken f in filters) {
                    string type = (string)f.SelectToken("type");
                    AddFilter(type, f);
                }
            }
        }


        //================================================================================
        //********************************************************************************
        public class ColumnSpecifier {
            private string mHeader = null;
            private ScrapeColumnSpecifier mColumn = null;

            public ColumnSpecifier() { }

            public ColumnSpecifier(string header) {
                mHeader = header;
                mColumn = null;
            }

            public ColumnSpecifier(ScrapeColumnSpecifier column) {
                mColumn = column;
                mHeader = null;
            }

            public string Header { get { return mHeader; } }
            public ScrapeColumnSpecifier Column { get { return mColumn; } }

            public override string ToString() {
                if (mHeader != null)
                    return '\'' + mHeader + '\'';
                else if (mColumn != null)
                    return mColumn.ToString();
                else
                    return "";
            }

            public void WriteJSON(JsonTextWriter writer) {
                writer.WriteStartObject();
                if (mHeader != null) {
                    writer.WritePropertyName("header");
                    writer.WriteValue(mHeader);
                }
                if (mColumn != null) {
                    writer.WritePropertyName("column");
                    mColumn.WriteJSON(writer);
                }
                writer.WriteEndObject();
            }
            
            public void ReadJSON(JToken token) {
                JToken subToken = token.SelectToken("header");
                mHeader = (subToken != null ? (string)subToken : null);
                
                mColumn = null;
                subToken = token.SelectToken("column");
                if (subToken != null) {
                    mColumn = new ScrapeColumnSpecifier(0);
                    mColumn.ReadJSON(subToken);
                }
            }
        }
    }

}

using PDFScrape.PDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Data {

    public class ScrapeRow {
        //================================================================================
        private ScrapeTable                     mTable;

        private List<string>                    mValues = new List<string>();


        //================================================================================
        //--------------------------------------------------------------------------------
        public ScrapeRow(ScrapeTable table, int columnCount) {
            // Table
            mTable = table;

            // Values
            for (int i = 0; i < columnCount; ++i) {
                mValues.Add("");
            }
        }

        //--------------------------------------------------------------------------------
        public ScrapeRow(ScrapeTable table, string[] values) {
            // Table
            mTable = table;

            // Values
            mValues = values.ToList();
        }


        // TABLE ================================================================================
        //--------------------------------------------------------------------------------
        public ScrapeTable Table { get { return mTable; } }


        // COLUMNS ================================================================================
        //--------------------------------------------------------------------------------
        internal void AddColumn(int index) {
            mValues.Insert(index, "");
        }

        //--------------------------------------------------------------------------------
        internal void RemoveColumn(int index) {
            mValues.RemoveAt(index);
        }

        //--------------------------------------------------------------------------------
        internal void RemoveAllColumns() {
            mValues.Clear();
        }
        
        //--------------------------------------------------------------------------------
        public int ColumnCount { get { return mValues.Count; } }

        
        // COLUMN OPERATIONS ================================================================================
        //--------------------------------------------------------------------------------
        public void MoveColumn(int index, int amount) {
            // Checks
            if (index < 0 || index >= ColumnCount || amount == 0)
                return;

            // To index
            int toIndex = Math.Min(Math.Max(index + amount, 0), ColumnCount - 1);
            if (toIndex == index)
                return;

            // Move
            string value = mValues[index];
            mValues.RemoveAt(index);
            mValues.Insert(toIndex, value);
        }

        //--------------------------------------------------------------------------------
        internal void MergeColumns(int startIndex, int endIndex, string padding) {
            // Checks
            if (startIndex < 0 || startIndex >= ColumnCount || endIndex < 0 || endIndex >= ColumnCount)
                throw new IndexOutOfRangeException();
            if (startIndex == endIndex)
                return;

            // Merge
            for (int i = startIndex; i < endIndex; ++i) {
                if (!string.IsNullOrEmpty(mValues[startIndex]) && !string.IsNullOrEmpty(mValues[startIndex + 1]))
                    mValues[startIndex] += padding;
                mValues[startIndex] += mValues[startIndex + 1];
                mValues.RemoveAt(startIndex + 1);
            }
        }


        // VALUES ================================================================================
        //--------------------------------------------------------------------------------
        public void Set(int columnIndex, string value) { mValues[columnIndex] = value; }
        public string Get(int columnIndex) { return mValues[columnIndex]; }

        //--------------------------------------------------------------------------------
        public string Get(PDFExtractor.ColumnSpecifier column) {
            if (column.Header != null) {
                // Header
                int index = Table.HeaderIndex(column.Header);
                return (index != -1 ? Get(index) : null);
            }
            else {
                // Column
                int index = column.Column.ColumnIndex(this);
                return ((index >= 0) && (index < ColumnCount) ? Get(index) : null);
            }
        }

        //--------------------------------------------------------------------------------
        public void Append(int columnIndex, string value) { mValues[columnIndex] += value; }
        public string[] Values { get { return mValues.ToArray(); } }
            
        
        // VALUE OPERATIONS ================================================================================
        //--------------------------------------------------------------------------------
        public void Merge(ScrapeRow other, string padding, bool newLinePerRow) {
            for (int i = 0; i < ColumnCount; ++i) {
                if (!string.IsNullOrEmpty(mValues[i]) && !string.IsNullOrEmpty(other.Get(i)))
                    mValues[i] += (newLinePerRow ? "\n" : "") + padding;
                mValues[i] += other.Get(i);
            }
        }

        //--------------------------------------------------------------------------------
        public ScrapeRow[] Split(string separator, ScrapeTable.SplitOptions options = 0, bool ignoreFrontSeparator = false) {
            // Checks
            if (string.IsNullOrEmpty(separator))
                throw new ArgumentException();

            // Flags
            bool flagKeepSeparator = options.HasFlag(ScrapeTable.SplitOptions.KEEP_SEPARATOR);
            bool flagSeparatorToPrevious = options.HasFlag(ScrapeTable.SplitOptions.SEPARATOR_TO_PREVIOUS);
            bool flagSeparatorToOwn = options.HasFlag(ScrapeTable.SplitOptions.SEPARATOR_TO_OWN);
            bool flagCaseInsensitive = options.HasFlag(ScrapeTable.SplitOptions.CASE_INSENSITIVE);

            // New rows
            List<ScrapeRow> newRows = new List<ScrapeRow>();

            // Split
            for (int i = 0; i < ColumnCount; ++i) {
                // Case
                string casedSeparator = !flagCaseInsensitive ? separator : separator.ToLower();
                string casedValue = !flagCaseInsensitive ? mValues[i] : mValues[i].ToLower();

                // Search
                int searchIndex = 0;
                if (ignoreFrontSeparator && casedValue.StartsWith(casedSeparator))
                    searchIndex = separator.Length;
                int separatorIndex = casedValue.IndexOf(casedSeparator, searchIndex);
                //bool separatorAtEnd = (separatorIndex != -1) && (mValues[i].Length == separatorIndex + separator.Length);

                // Apply
                if (separatorIndex != -1) {
                    string beforeSeparator = mValues[i].Substring(0, separatorIndex);
                    string afterSeparator = mValues[i].Substring(separatorIndex + separator.Length);
                    if (newRows.Count == 0)
                        newRows.Add(new ScrapeRow(Table, ColumnCount));

                    // Old row
                    mValues[i] = beforeSeparator;
                    if (flagKeepSeparator && flagSeparatorToPrevious)
                        mValues[i] += separator;

                    // Separator row
                    if (flagKeepSeparator && !flagSeparatorToPrevious && flagSeparatorToOwn) {
                        if (newRows.Count == 1)
                            newRows.Add(new ScrapeRow(Table, ColumnCount));
                        newRows[0].Set(i, separator);
                    }

                    // New row
                    if (flagKeepSeparator && !flagSeparatorToPrevious && !flagSeparatorToOwn)
                        newRows[newRows.Count - 1].Set(i, separator);
                    newRows[newRows.Count - 1].Append(i, afterSeparator);
                }
            }

            // Return
            return newRows.ToArray();
        }
    }

}

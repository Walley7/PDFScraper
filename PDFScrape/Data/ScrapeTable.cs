using PDFScrape.Exceptions;
using PDFScrape.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PDFScrape.Data {
    
    public class ScrapeTable {
        //================================================================================
        public enum SplitOptions {
            CASE_INSENSITIVE = 1 << 0,
            START_FROM_END = 1 << 1,
            KEEP_SEPARATOR = 1 << 2,
            SEPARATOR_TO_PREVIOUS = 1 << 3,
            SEPARATOR_TO_OWN = 1 << 4
        }

        public enum Condition {
            HAS_VALUE = 0,
            HAS_NO_VALUE = 1,
            CONTAINS = 2,
            //CONTAINS_WILDCARDS,
            DOES_NOT_CONTAIN = 3,
            //DOES_NOT_CONTAIN_WILDCARDS,
            STARTS_WITH = 4,
            DOES_NOT_START_WITH = 5,
            IS_AN_INTEGER = 6,
            IS_NOT_AN_INTEGER = 7,
            IS_A_DECIMAL = 8,
            IS_NOT_A_DECIMAL = 9,
            IS_A_CURRENCY = 10,
            IS_NOT_A_CURRENCY = 11,
            HAS_ONE_WORD = 12,
            HAS_MULTIPLE_WORDS = 13,
            HAS_N_WORDS = 14
            //MATCHES_REGEX,
            //DOES_NOT_MATCH_REGEX
        }


        //================================================================================
        private string                          mName = null;

        private int                             mColumnCount;
        private List<string>                    mHeaders = new List<string>();

        private List<ScrapeRow>                 mRows = new List<ScrapeRow>();


        //================================================================================
        //--------------------------------------------------------------------------------
        public ScrapeTable(string name, int columnCount, string[] headers, string[][] rows) {
            // Name
            if (name != null)
                Name = name;

            // Columns / headers
            mColumnCount = columnCount;
            if (headers != null)
                SetHeaders(headers);
            else
                SetHeaders(Enumerable.Repeat("", columnCount).ToArray());

            // Rows
            if (rows != null)
                AddRows(rows);
        }

        //--------------------------------------------------------------------------------
        public ScrapeTable(string name, string[] headers, string[][] rows) : this(name, headers.Length, headers, rows) { }
        public ScrapeTable(string name, string[] headers) : this(name, headers.Length, headers, null) { }
        public ScrapeTable(string name, int columnCount, string[][] rows) : this(name, columnCount, null, rows) { }
        public ScrapeTable(string name, int columnCount) : this(name, columnCount, null, null) { }
        public ScrapeTable(int columnCount, string[] headers, string[][] rows) : this(null, columnCount, headers, rows) { }
        public ScrapeTable(string[] headers, string[][] rows) : this(null, headers, rows) { }
        public ScrapeTable(string[] headers) : this(null, headers) { }
        public ScrapeTable(int columnCount, string[][] rows) : this(null, columnCount, rows) { }
        public ScrapeTable(int columnCount) : this(null, columnCount) { }
        
        //--------------------------------------------------------------------------------
        public ScrapeTable(ScrapeTable table) : this(table.Name, (string[])table.Headers.Clone()) {
            // Rows
            for (int i = 0; i < table.RowCount; ++i) {
                AddRow((string[])table.Row(i).Values.Clone());
            }
        }


        // NAME ================================================================================
        //--------------------------------------------------------------------------------
        public string Name {
            set {
                if (mName != null)
                    throw new InvalidCallException();
                if (value == null)
                    throw new ArgumentNullException();
                if (value.Equals(""))
                    throw new ArgumentException();
                mName = value;
            }
            get { return mName; }
        }


        // COLUMNS ================================================================================
        //--------------------------------------------------------------------------------
        public void AddColumn(int index, string header) {
            // Checks
            if (header == null)
                throw new ArgumentNullException();
            if (!header.Equals("") && HasHeader(header))
                throw new DuplicateException();

            // Header
            mHeaders.Insert(index, header);

            // Rows
            foreach (ScrapeRow r in mRows) {
                r.AddColumn(index);
            }

            // Column count
            ++mColumnCount;
        }
        
        //--------------------------------------------------------------------------------
        public void AddColumn(int index) { AddColumn(index, ""); }
        public void AddColumn(string header) { AddColumn(mColumnCount, header); }
        public void AddColumn() { AddColumn(""); }
        
        //--------------------------------------------------------------------------------
        public void RemoveColumn(int index, bool removeRowsOnNoColumns = true) {
            // Header
            mHeaders.RemoveAt(index);
            
            // Rows
            foreach (ScrapeRow r in mRows) {
                r.RemoveColumn(index);
            }

            // Column count
            --mColumnCount;

            // Row removal
            if (removeRowsOnNoColumns && mColumnCount == 0)
                RemoveAllRows();
        }
        
        //--------------------------------------------------------------------------------
        public void RemoveColumn(string header, bool removeRowsOnNoColumns = true) {
            // Checks
            if (header == null)
                throw new ArgumentNullException();
            if (header.Equals(""))
                throw new ArgumentException();

            // Remove
            int index = HeaderIndex(header);
            if (index == -1)
                throw new ArgumentException();
            RemoveColumn(index, removeRowsOnNoColumns);
        }

        //--------------------------------------------------------------------------------
        public void RemoveAllColumns(bool removeRows = true) {
            // Headers
            mHeaders.Clear();

            // Rows
            foreach (ScrapeRow r in mRows) {
                r.RemoveAllColumns();
            }
            
            // Column count
            mColumnCount = 0;
            
            // Row removal
            if (removeRows)
                RemoveAllRows();
        }

        //--------------------------------------------------------------------------------
        public int ColumnCount { get { return mColumnCount; } }
        

        // COLUMN OPERATIONS ================================================================================
        //--------------------------------------------------------------------------------
        public void RemoveColumns(int startIndex, int endIndex) {
            for (int i = startIndex; i <= endIndex && startIndex < ColumnCount; ++i) {
                RemoveColumn(startIndex);
            }
        }

        //--------------------------------------------------------------------------------
        public void KeepColumns(int startIndex, int endIndex) {
            // Remove end columns
            while (endIndex + 1 < ColumnCount) {
                RemoveColumn(ColumnCount - 1);
            }

            // Remove start columns
            for (int i = 0; i < startIndex; ++i) {
                RemoveColumn(0);
            }
        }

        //--------------------------------------------------------------------------------
        public void MoveColumn(int index, int amount) {
            // Checks
            if (amount == 0)
                return;
            if (index < 0 || index >= ColumnCount)
                throw new IndexOutOfRangeException();

            // To index
            int toIndex = Math.Min(Math.Max(index + amount, 0), ColumnCount - 1);
            if (toIndex == index)
                return;

            // Move
            string header = mHeaders[index];
            mHeaders.RemoveAt(index);
            mHeaders.Insert(toIndex, header);

            // Rows
            foreach (ScrapeRow r in mRows) {
                r.MoveColumn(index, amount);
            }
        }

        //--------------------------------------------------------------------------------
        public void MergeColumns(int startIndex, int endIndex, string padding = " ", bool mergeHeaders = false) {
            // Checks
            if (startIndex < 0 || startIndex >= ColumnCount || endIndex < 0 || endIndex >= ColumnCount)
                throw new IndexOutOfRangeException();
            if (startIndex == endIndex)
                return;

            // Headers
            for (int i = startIndex; i < endIndex; ++i) {
                if (mergeHeaders) {
                    if (!string.IsNullOrEmpty(mHeaders[startIndex]) && !string.IsNullOrEmpty(mHeaders[startIndex + 1]))
                        mHeaders[startIndex] += padding;
                    mHeaders[startIndex] += mHeaders[startIndex + 1];
                }

                mHeaders.RemoveAt(startIndex + 1);
                --mColumnCount;
            }

            // Rows
            foreach (ScrapeRow r in mRows) {
                r.MergeColumns(startIndex, endIndex, padding);
            }
        }

        //--------------------------------------------------------------------------------
        public void SplitColumn(int index, string separator, int maxSplits, string headerPrefix = "", ScrapeTable.SplitOptions options = 0) {
            // Checks
            if (index < 0 || index >= ColumnCount)
                throw new ArgumentOutOfRangeException();
            if (string.IsNullOrEmpty(separator))
                throw new ArgumentException();

            // Flags
            bool flagCaseInsensitive = options.HasFlag(ScrapeTable.SplitOptions.CASE_INSENSITIVE);
            bool flagStartFromEnd = options.HasFlag(ScrapeTable.SplitOptions.START_FROM_END);
            bool flagKeepSeparator = options.HasFlag(ScrapeTable.SplitOptions.KEEP_SEPARATOR);
            bool flagSeparatorToPrevious = options.HasFlag(ScrapeTable.SplitOptions.SEPARATOR_TO_PREVIOUS);
            bool flagSeparatorToOwn = options.HasFlag(ScrapeTable.SplitOptions.SEPARATOR_TO_OWN);

            // Variables
            int splitCount = 0;
            int startIndex = index;
            int lastNewColumnIndex = index;

            // Split
            while (splitCount < maxSplits || maxSplits < 0) {
                // Variables
                List<string[]> newColumns = new List<string[]>();

                // Rows
                for (int i = 0; i < RowCount; ++i) {
                    // Case
                    string casedSeparator = !flagCaseInsensitive ? separator : separator.ToLower();
                    string casedRow = !flagCaseInsensitive ? Row(i).Get(index) : Row(i).Get(index).ToLower();

                    // Search
                    int searchIndex, separatorIndex;
                    if (!flagStartFromEnd) {
                        // From start
                        searchIndex = 0;
                        if (splitCount > 0 && flagKeepSeparator && !flagSeparatorToPrevious && !flagSeparatorToOwn && casedRow.StartsWith(casedSeparator))
                            searchIndex = separator.Length;
                        separatorIndex = casedRow.IndexOf(casedSeparator, searchIndex);
                    }
                    else {
                        // From end
                        searchIndex = casedRow.Length - 1;
                        if (splitCount > 0 && flagKeepSeparator && flagSeparatorToPrevious && !flagSeparatorToOwn && casedRow.EndsWith(casedSeparator))
                            searchIndex -= separator.Length;
                        separatorIndex = searchIndex >= 0 ? casedRow.LastIndexOf(casedSeparator, searchIndex) : -1;
                    }

                    // Split
                    if (separatorIndex != -1) {
                        // Regions
                        string beforeSeparator = Row(i).Get(index).Substring(0, separatorIndex);
                        string rowSeparator = Row(i).Get(index).Substring(separatorIndex, separator.Length);
                        string afterSeparator = Row(i).Get(index).Substring(separatorIndex + separator.Length);
                        if (newColumns.Count == 0)
                            newColumns.Add(Enumerable.Repeat("", RowCount).ToArray());

                        // Old column
                        Row(i).Set(index, beforeSeparator);
                        if (flagKeepSeparator && flagSeparatorToPrevious)
                            Row(i).Set(index, Row(i).Get(index) + rowSeparator);

                        // Separator column
                        if (flagKeepSeparator && !flagSeparatorToPrevious && flagSeparatorToOwn) {
                            if (newColumns.Count == 1)
                                newColumns.Add(Enumerable.Repeat("", RowCount).ToArray());
                            newColumns[0][i] = rowSeparator;
                        }

                        // New column
                        if (flagKeepSeparator && !flagSeparatorToPrevious && !flagSeparatorToOwn)
                            newColumns[newColumns.Count - 1][i] = rowSeparator;
                        newColumns[newColumns.Count - 1][i] += afterSeparator;
                    }
                }

                // Insert new columns
                if (newColumns.Count > 0) {
                    for (int i = 0; i < newColumns.Count; ++i) {
                        // Column
                        AddColumn(index + i + 1);
                        ++lastNewColumnIndex;

                        // Rows
                        for (int j = 0; j < RowCount; ++j) {
                            Row(j).Set(index + i + 1, newColumns[i][j]);
                        }
                    }
                    
                    if (!flagStartFromEnd)
                        index += newColumns.Count;
                    ++splitCount;
                }
                else
                    break;
            }
            
            // Shift values (when starting from end they end up right aligned - not what we want)
            PackColumnsToLeft(startIndex + 1, lastNewColumnIndex);

            // Headers (done here due to not being in left to right order when start from end option is set)
            for (int i = startIndex + 1; i <= lastNewColumnIndex; ++i) {
                SetHeader(i, FirstFreeHeader(headerPrefix));
            }
        }

        //--------------------------------------------------------------------------------
        public void SplitColumn(int index, string separator, string headerPrefix = "", ScrapeTable.SplitOptions options = 0) { SplitColumn(index, separator, -1, headerPrefix, options); }

        //--------------------------------------------------------------------------------
        public void SplitColumnAtWhitespace(int index, int maxSplits, string headerPrefix = "", ScrapeTable.SplitOptions options = 0) {
            // Checks            
            if (index < 0 || index >= ColumnCount)
                throw new ArgumentOutOfRangeException();

            // Flags
            bool flagStartFromEnd = options.HasFlag(ScrapeTable.SplitOptions.START_FROM_END);

            // Variables
            int splitCount = 0;
            int startIndex = index;
            int lastNewColumnIndex = index;

            // Split
            while (splitCount < maxSplits || maxSplits < 0) {
                // Variables
                List<string[]> newColumns = new List<string[]>();

                // Rows
                for (int i = 0; i < RowCount; ++i) {
                    int whitespaceStart = -1;
                    int whitespaceEnd = -1;
                    string row = Row(i).Get(index);

                    // Search
                    bool reachedWhitespace = false;
                    int j = (!flagStartFromEnd ? 0 : row.Length - 1);
                    while ((!flagStartFromEnd && j < row.Length) || (flagStartFromEnd && j >= 0)) {
                        // Whitespace
                        if (!reachedWhitespace) {
                            if (char.IsWhiteSpace(row[j])) {
                                whitespaceStart = whitespaceEnd = j;
                                reachedWhitespace = true;
                            }
                        }
                        else {
                            if (char.IsWhiteSpace(row[j]))
                                whitespaceEnd = j; // Keep moving the end forward, just so that we don't have to manually handle it being -1 below
                            else
                                break;
                        }

                        // Increment
                        if (!flagStartFromEnd)
                            ++j;
                        else
                            --j;
                    }

                    // Reverse
                    if (flagStartFromEnd)
                        UGeneral.Swap(ref whitespaceStart, ref whitespaceEnd);

                    // Split
                    if (whitespaceStart != -1 && whitespaceEnd != -1) {
                        // Regions
                        string beforeWhitespace = row.Substring(0, whitespaceStart);
                        string afterWhitespace = row.Substring(whitespaceEnd + 1);
                        if (newColumns.Count == 0)
                            newColumns.Add(Enumerable.Repeat("", RowCount).ToArray());

                        // Columns
                        Row(i).Set(index, beforeWhitespace);
                        newColumns[0][i] = afterWhitespace;
                    }
                }

                // Insert new column
                if (newColumns.Count > 0) {
                    // Column
                    AddColumn(index + 1);
                    ++lastNewColumnIndex;

                    // Rows
                    for (int j = 0; j < RowCount; ++j) {
                        Row(j).Set(index + 1, newColumns[0][j]);
                    }
                    
                    if (!flagStartFromEnd)
                        ++index;
                    ++splitCount;
                }
                else
                    break;
            }

            // Shift values (when starting from end they end up right aligned - not what we want)
            PackColumnsToLeft(startIndex + 1, lastNewColumnIndex);

            // Headers (done here due to not being in left to right order when start from end option is set)
            for (int i = startIndex + 1; i <= lastNewColumnIndex; ++i) {
                SetHeader(i, FirstFreeHeader(headerPrefix));
            }
        }

        //--------------------------------------------------------------------------------
        public void SplitColumnAtWhitespace(int index, string headerPrefix = "", ScrapeTable.SplitOptions options = 0) { SplitColumnAtWhitespace(index, -1, headerPrefix, options); }

        //--------------------------------------------------------------------------------
        public void SplitColumnAfterNWords(int index, int n, int maxSplits, string headerPrefix = "", ScrapeTable.SplitOptions options = 0) {
            // Checks            
            if (index < 0 || index >= ColumnCount)
                throw new ArgumentOutOfRangeException();

            // Flags
            bool flagStartFromEnd = options.HasFlag(ScrapeTable.SplitOptions.START_FROM_END);

            // Variables
            int splitCount = 0;
            int startIndex = index;
            int lastNewColumnIndex = index;

            // Split
            while (splitCount < maxSplits || maxSplits < 0) {
                // Variables
                List<string[]> newColumns = new List<string[]>();

                // Rows
                for (int i = 0; i < RowCount; ++i) {
                    int wordBreakStart = -1;
                    int wordBreakEnd = -1;
                    string row = Row(i).Get(index);

                    // Search
                    bool lastWasWhitespace = true;
                    int wordCount = 0;

                    int j = (!flagStartFromEnd ? 0 : row.Length - 1);
                    while ((!flagStartFromEnd && j < row.Length) || (flagStartFromEnd && j >= 0)) {
                        // Words
                        if (char.IsWhiteSpace(row[j])) {
                            if (!lastWasWhitespace) {
                                ++wordCount;
                                if (wordCount == n)
                                    wordBreakStart = j;
                            }

                            wordBreakEnd = j;
                            lastWasWhitespace = true;
                        }
                        else {
                            if (wordCount == n)
                                break;
                            lastWasWhitespace = false;
                        }

                        // Increment
                        if (!flagStartFromEnd)
                            ++j;
                        else
                            --j;
                    }

                    // Reverse
                    if (flagStartFromEnd)
                        UGeneral.Swap(ref wordBreakStart, ref wordBreakEnd);

                    // Split
                    if (wordBreakStart != -1 && wordBreakEnd != -1) {
                        // Regions
                        string beforeWordBreak = row.Substring(0, wordBreakStart);
                        string afterWorkBreak = row.Substring(wordBreakEnd + 1);
                        if (newColumns.Count == 0)
                            newColumns.Add(Enumerable.Repeat("", RowCount).ToArray());

                        // Columns
                        Row(i).Set(index, beforeWordBreak);
                        newColumns[0][i] = afterWorkBreak;
                    }
                }

                // Insert new column
                if (newColumns.Count > 0) {
                    // Column
                    AddColumn(index + 1);
                    ++lastNewColumnIndex;

                    // Rows
                    for (int j = 0; j < RowCount; ++j) {
                        Row(j).Set(index + 1, newColumns[0][j]);
                    }
                    
                    if (!flagStartFromEnd)
                        ++index;
                    ++splitCount;
                }
                else
                    break;
            }

            // Shift values (when starting from end they end up right aligned - not what we want)
            PackColumnsToLeft(startIndex + 1, lastNewColumnIndex);

            // Headers (done here due to not being in left to right order when start from end option is set)
            for (int i = startIndex + 1; i <= lastNewColumnIndex; ++i) {
                SetHeader(i, FirstFreeHeader(headerPrefix));
            }
        }

        //--------------------------------------------------------------------------------
        public void SplitColumnAfterNWords(int index, int n, string headerPrefix = "", ScrapeTable.SplitOptions options = 0) { SplitColumnAfterNWords(index, n, -1, headerPrefix, options); }

        //--------------------------------------------------------------------------------
        public void SplitColumnAfterNCharacters(int index, int n, int maxSplits, string headerPrefix = "", ScrapeTable.SplitOptions options = 0) {
            // Checks            
            if (index < 0 || index >= ColumnCount)
                throw new ArgumentOutOfRangeException();

            // Flags
            bool flagStartFromEnd = options.HasFlag(ScrapeTable.SplitOptions.START_FROM_END);

            // Variables
            int splitCount = 0;
            int startIndex = index;
            int lastNewColumnIndex = index;

            // Split
            while (splitCount < maxSplits || maxSplits < 0) {
                // Variables
                List<string[]> newColumns = new List<string[]>();

                // Rows
                for (int i = 0; i < RowCount; ++i) {
                    string row = Row(i).Get(index);

                    if (row.Length > n) {
                        string beforeSplit = row.Substring(0, !flagStartFromEnd ? n : row.Length - n);
                        string afterSplit = row.Substring(!flagStartFromEnd ? n : row.Length - n);
                        if (newColumns.Count == 0)
                            newColumns.Add(Enumerable.Repeat("", RowCount).ToArray());

                        Row(i).Set(index, beforeSplit);
                        newColumns[0][i] = afterSplit;
                    }
                }

                 // Insert new column
                if (newColumns.Count > 0) {
                    // Column
                    AddColumn(index + 1);
                    ++lastNewColumnIndex;

                    // Rows
                    for (int j = 0; j < RowCount; ++j) {
                        Row(j).Set(index + 1, newColumns[0][j]);
                    }
                    
                    if (!flagStartFromEnd)
                        ++index;
                    ++splitCount;
                }
                else
                    break;
            }

            // Shift values (when starting from end they end up right aligned - not what we want)
            PackColumnsToLeft(startIndex + 1, lastNewColumnIndex);

            // Headers (done here due to not being in left to right order when start from end option is set)
            for (int i = startIndex + 1; i <= lastNewColumnIndex; ++i) {
                SetHeader(i, FirstFreeHeader(headerPrefix));
            }
        }

        //--------------------------------------------------------------------------------
        public void SplitColumnAfterNCharacters(int index, int n, string headerPrefix = "", ScrapeTable.SplitOptions options = 0) { SplitColumnAfterNCharacters(index, n, -1, headerPrefix, options); }

        //--------------------------------------------------------------------------------
        public void PackColumnsToLeft(int startIndex, int endIndex) {
            // Rows
            for (int i = 0; i < RowCount; ++i) {
                // Columns
                for (int j = startIndex; j < endIndex; ++j) {
                    ScrapeRow row = Row(i);

                    // Pack
                    if (string.IsNullOrEmpty(row.Get(j))) {
                        for (int k = j + 1; k <= endIndex; ++k) {
                            if (!string.IsNullOrEmpty(row.Get(k))) {
                                row.Set(j, row.Get(k));
                                row.Set(k, "");
                                break;
                            }
                        }
                    }
                }
            }
        }

        //--------------------------------------------------------------------------------
        public void PackColumnsToLeft() { PackColumnsToLeft(0, ColumnCount - 1); }


        // HEADERS ================================================================================
        //--------------------------------------------------------------------------------
        public void SetHeader(int columnIndex, string header) {
            // Checks
            if (header == null)
                throw new ArgumentNullException();
            int headerIndex = HeaderIndex(header);
            if (!header.Equals("") && (headerIndex != -1) && (headerIndex != columnIndex))
                throw new DuplicateException();

            // Set
            mHeaders[columnIndex] = header;
        }

        //--------------------------------------------------------------------------------
        public void SetHeaders(string[] headers) {
            if (headers.Length != mColumnCount)
                throw new ArgumentException();
            mHeaders = headers.ToList();
        }

        //--------------------------------------------------------------------------------
        public string Header(int columnIndex) { return mHeaders[columnIndex]; }
        public string[] Headers { get { return mHeaders.ToArray(); } }

        //--------------------------------------------------------------------------------
        public int HeaderIndex(string header) {
            // Checks
            if (string.IsNullOrEmpty(header))
                return -1;

            // Header index
            for (int i = 0; i < mColumnCount; ++i) {
                if (mHeaders[i].Equals(header))
                    return i;
            }
            return -1;
        }

        //--------------------------------------------------------------------------------
        public bool HasHeader(string header) { return (HeaderIndex(header) != -1); }

        //--------------------------------------------------------------------------------
        public string FirstFreeHeader(string headerPrefix) {
            // Checks
            if (headerPrefix.Equals(""))
                return "";

            // Prefix only
            if (!HasHeader(headerPrefix))
                return headerPrefix;

            // First free header
            int i = 2;
            while (HasHeader(headerPrefix + i)) {
                ++i;
            }

            return headerPrefix + i;
        }


        // ROWS ================================================================================
        //--------------------------------------------------------------------------------
        private ScrapeRow _AddRow(int index, ScrapeRow row) {
            mRows.Insert(index, row);
            return row;
        }

        //--------------------------------------------------------------------------------
        public ScrapeRow AddRow(int index, string[] values) {
            // Checks
            if ((values != null) && (values.Length != mColumnCount))
                throw new ArgumentException();

            // Row
            ScrapeRow row;
            if (values != null)
                row = new ScrapeRow(this, values);
            else
                row = new ScrapeRow(this, mColumnCount);

            // Add
            return _AddRow(index, row);
        }

        //--------------------------------------------------------------------------------
        public ScrapeRow AddRow(int index) { return AddRow(index, null); }
        public ScrapeRow AddRow(string[] values) { return AddRow(mRows.Count, values); }
        public ScrapeRow AddRow() { return AddRow(null); }

        //--------------------------------------------------------------------------------
        public void AddRows(string[][] rows) {
            foreach (string[] r in rows) {
                AddRow(r);
            }
        }
        
        //--------------------------------------------------------------------------------
        public void RemoveRow(int index) {
            mRows.RemoveAt(index);
        }

        //--------------------------------------------------------------------------------
        public void RemoveAllRows() {
            mRows.Clear();
        }

        //--------------------------------------------------------------------------------
        public ScrapeRow Row(int index) { return mRows[index]; }
        public ScrapeRow[] Rows { get { return mRows.ToArray(); } }
        public int RowCount { get { return mRows.Count; } }
            
        
        // ROW OPERATIONS ================================================================================
        //--------------------------------------------------------------------------------
        public void RemoveRows(int startIndex, int endIndex) {
            if (endIndex < startIndex)
                throw new ArgumentException();
            mRows.RemoveRange(startIndex, (endIndex - startIndex) + 1);
        }
        
        //--------------------------------------------------------------------------------
        public void RemoveRowsBetween(Condition? startCondition, string startConditionArgument, int startConditionColumnIndex, bool includeStartRow,
                                    Condition? endCondition, string endConditionArgument, int endConditionColumnIndex, bool includeEndRow)
        {
            // Conditions
            bool useStartCondition = (startCondition != null);
            bool useEndCondition = (endCondition != null);

            // Checks
            if (useStartCondition && (startConditionColumnIndex < 0 || startConditionColumnIndex >= ColumnCount))
                throw new ArgumentOutOfRangeException();
            if (useEndCondition && (endConditionColumnIndex < 0 || endConditionColumnIndex >= ColumnCount))
                throw new ArgumentOutOfRangeException();

            // Row indexes
            int rowStart = useStartCondition ? FindFirstRowWhere(startConditionColumnIndex, (Condition)startCondition, startConditionArgument) : 0;
            int rowEnd = useEndCondition && (rowStart < RowCount) ? FindFirstRowWhere(endConditionColumnIndex, rowStart, (Condition)endCondition, endConditionArgument) : RowCount - 1;

            if (useStartCondition && !includeStartRow && (rowStart != -1))
                ++rowStart;
            if (useEndCondition && !includeEndRow && (rowEnd != -1))
                --rowEnd;
            if (rowEnd == -1)
                rowEnd = RowCount - 1;

            // Keep
            if (rowStart != -1 && rowStart <= rowEnd)
                RemoveRows(rowStart, rowEnd);
        }
        
        //--------------------------------------------------------------------------------
        public void RemoveRowsWhere(Condition condition, string conditionArgument, int conditionColumnIndex) {
            // Checks
            if (conditionColumnIndex < 0 || conditionColumnIndex >= ColumnCount)
                throw new ArgumentOutOfRangeException();

            // Keep
            for (int i = RowCount - 1; i >= 0; --i) {
                if (RowMeetsCondition(i, conditionColumnIndex, condition, conditionArgument))
                    RemoveRow(i);
            }
        }

        //--------------------------------------------------------------------------------
        public void KeepRows(int startIndex, int endIndex) {
            // Limit
            startIndex = Math.Max(startIndex, 0);
            endIndex = Math.Min(endIndex, RowCount - 1);
            
            // Remove end columns
            if (endIndex < RowCount - 1)
                RemoveRows(endIndex + 1, RowCount - 1); 

            // Remove start columns
            if (startIndex > 0)
                RemoveRows(0, startIndex - 1);
        }
        
        //--------------------------------------------------------------------------------
        public void KeepRowsBetween(Condition? startCondition, string startConditionArgument, int startConditionColumnIndex, bool includeStartRow,
                                    Condition? endCondition, string endConditionArgument, int endConditionColumnIndex, bool includeEndRow)
        {
            // Conditions
            bool useStartCondition = (startCondition != null);
            bool useEndCondition = (endCondition != null);

            // Checks
            if (useStartCondition && (startConditionColumnIndex < 0 || startConditionColumnIndex >= ColumnCount))
                throw new ArgumentOutOfRangeException();
            if (useEndCondition && (endConditionColumnIndex < 0 || endConditionColumnIndex >= ColumnCount))
                throw new ArgumentOutOfRangeException();

            // Row indexes
            int rowStart = useStartCondition ? FindFirstRowWhere(startConditionColumnIndex, (Condition)startCondition, startConditionArgument) : 0;
            int rowEnd = useEndCondition && (rowStart < RowCount) ? FindFirstRowWhere(endConditionColumnIndex, rowStart, (Condition)endCondition, endConditionArgument) : RowCount - 1;

            if (useStartCondition && !includeStartRow && (rowStart != -1))
                ++rowStart;
            if (useEndCondition && !includeEndRow && (rowEnd != -1))
                --rowEnd;
            if (rowEnd == -1)
                rowEnd = RowCount - 1;

            // Keep
            if (rowStart != -1 && rowStart <= rowEnd)
                KeepRows(rowStart, rowEnd);
            else
                RemoveAllRows();
        }
        
        //--------------------------------------------------------------------------------
        public void KeepRowsWhere(Condition condition, string conditionArgument, int conditionColumnIndex) {
            // Checks
            if (conditionColumnIndex < 0 || conditionColumnIndex >= ColumnCount)
                throw new ArgumentOutOfRangeException();

            // Keep
            for (int i = RowCount - 1; i >= 0; --i) {
                if (!RowMeetsCondition(i, conditionColumnIndex, condition, conditionArgument))
                    RemoveRow(i);
            }
        }
        
        //--------------------------------------------------------------------------------
        public void MergeRows(int startIndex, int endIndex, string padding = " ", bool newLinePerRow = false) {
            // Checks
            if (startIndex < 0 || startIndex >= RowCount || endIndex < 0 || endIndex >= RowCount)
                throw new IndexOutOfRangeException();
            if (startIndex == endIndex)
                return;

            // Rows
            for (int i = startIndex; i < endIndex; ++i) {
                Row(startIndex).Merge(Row(startIndex + 1), padding, newLinePerRow);
                RemoveRow(startIndex + 1);
            }
        }

        //--------------------------------------------------------------------------------
        public void SplitRows(string separator, SplitOptions options = 0) {
            for (int i = 0; i < RowCount; ++i) {
                // Split
                ScrapeRow row = Row(i);
                ScrapeRow[] newRows = row.Split(separator, options);

                // Add
                for (int j = 0; j < newRows.Length; ++j) {
                    _AddRow(i + j + 1, newRows[j]);
                }
            }
        }

        //--------------------------------------------------------------------------------
        public int FindFirstRowWhere(int columnIndex, int startRowIndex, Condition condition, string argument) {
            // Checks
            if (startRowIndex < 0 || startRowIndex >= RowCount)
                throw new ArgumentOutOfRangeException();

            // Find
            for (int i = startRowIndex; i < RowCount; ++i) {
                if (RowMeetsCondition(i, columnIndex, condition, argument))
                    return i;
            }
            
            return -1;
        }

        //--------------------------------------------------------------------------------
        public int FindFirstRowWhere(int columnIndex, Condition condition, string argument) { return FindFirstRowWhere(columnIndex, 0, condition, argument); }
        
        //--------------------------------------------------------------------------------
        public bool RowMeetsCondition(int rowIndex, int columnStartIndex, int columnEndIndex, Condition condition, string argument) {
            // Checks
            if (rowIndex < 0 || rowIndex >= RowCount)
                throw new ArgumentOutOfRangeException();
            if (columnStartIndex > columnEndIndex)
                throw new ArgumentException();
            if (columnStartIndex < 0 || columnStartIndex >= ColumnCount || columnEndIndex < 0 || columnEndIndex >= ColumnCount)
                throw new ArgumentOutOfRangeException();

            // Row
            ScrapeRow row = Row(rowIndex);
            for (int i = columnStartIndex; i <= columnEndIndex; ++i) {
                if (MeetsCondition(row.Get(i), condition, argument))
                    return true;
            }

            return false;
        }
        
        //--------------------------------------------------------------------------------
        public bool RowMeetsCondition(int rowIndex, int columnIndex, Condition condition, string argument) { return RowMeetsCondition(rowIndex, columnIndex, columnIndex, condition, argument); }
        public bool RowMeetsCondition(int rowIndex, Condition condition, string argument) { return RowMeetsCondition(rowIndex, 0, ColumnCount - 1, condition, argument); }


        // CONDITIONS ================================================================================
        //--------------------------------------------------------------------------------
        public static string ConditionString(Condition condition) {
            switch (condition) {
                case Condition.HAS_VALUE:           return "Has value";
                case Condition.HAS_NO_VALUE:        return "Has no value";
                case Condition.CONTAINS:            return "Contains";
                case Condition.DOES_NOT_CONTAIN:    return "Does not contain";
                case Condition.STARTS_WITH:         return "Starts with";
                case Condition.DOES_NOT_START_WITH: return "Does not start with";
                case Condition.IS_AN_INTEGER:       return "Is an integer";
                case Condition.IS_NOT_AN_INTEGER:   return "Is not an integer";
                case Condition.IS_A_DECIMAL:        return "Is a decimal";
                case Condition.IS_NOT_A_DECIMAL:    return "Is not a decimal";
                case Condition.IS_A_CURRENCY:       return "Is a currency";
                case Condition.IS_NOT_A_CURRENCY:   return "Is not a currency";
                case Condition.HAS_ONE_WORD:        return "Has one word";
                case Condition.HAS_MULTIPLE_WORDS:  return "Has multiple words";
                case Condition.HAS_N_WORDS:         return "Has # words";
                default:                            return "INVALID";
            }
        }

        //--------------------------------------------------------------------------------
        public static Condition[] ConditionList() {
            Array conditions = Enum.GetValues(typeof(Condition));
            Condition[] conditionList = new Condition[conditions.Length];
            for (int i = 0; i < conditions.Length; ++i) {
                conditionList[i] = (Condition)conditions.GetValue(i);
            }
            return conditionList;
        }

        //--------------------------------------------------------------------------------
        public static string[] ConditionStringList() {
            Condition[] conditionList = ConditionList();
            string[] stringList = new string[conditionList.Length];
            for (int i = 0; i < conditionList.Length; ++i) {
                stringList[i] = ConditionString(conditionList[i]);
            }
            return stringList;
        }

        //--------------------------------------------------------------------------------
        public static bool MeetsCondition(string value, Condition condition, string argument) {
            switch (condition) {
                case Condition.HAS_VALUE:
                    return !string.IsNullOrEmpty(value);

                case Condition.HAS_NO_VALUE:
                    return string.IsNullOrEmpty(value);

                case Condition.CONTAINS:
                    return value.Contains(argument);

                case Condition.DOES_NOT_CONTAIN:
                    return !value.Contains(argument);

                case Condition.STARTS_WITH:
                    return value.StartsWith(argument);

                case Condition.DOES_NOT_START_WITH:
                    return !value.StartsWith(argument);

                case Condition.IS_AN_INTEGER:
                    return UString.IsInteger(value);

                case Condition.IS_NOT_AN_INTEGER:
                    return !UString.IsInteger(value);

                case Condition.IS_A_DECIMAL:
                    return UString.IsDecimal(value);

                case Condition.IS_NOT_A_DECIMAL:
                    return !UString.IsDecimal(value);

                case Condition.IS_A_CURRENCY:
                    return UString.IsCurrency(value);

                case Condition.IS_NOT_A_CURRENCY:
                    return !UString.IsCurrency(value);

                case Condition.HAS_ONE_WORD:
                    return (UString.WordCount(value) == 1);

                case Condition.HAS_MULTIPLE_WORDS:
                    return (UString.WordCount(value) > 1);

                case Condition.HAS_N_WORDS:
                    int n = 0;
                    if (!Int32.TryParse(value, out n))
                        return false;
                    return (UString.WordCount(value) == n);

                default:
                    return false;
            }
        }

        
        // DATA GRID VIEW ================================================================================
        //--------------------------------------------------------------------------------
        public void AddGridColumns(DataGridView grid, bool readOnly = true) {
            for (int i = 0; i < ColumnCount; ++i) {
                string header = Header(i);
                grid.Columns.Add(grid.Name + "Column" + grid.Columns.Count, !header.Equals("") ? header : "Col #" + (i + 1));
                grid.Columns[i].ReadOnly = readOnly;
            }
        }
            
        //--------------------------------------------------------------------------------
        public void SetGridColumns(DataGridView grid) {
            grid.Columns.Clear();
            AddGridColumns(grid);
        }
        
        //--------------------------------------------------------------------------------
        public void AddGridRows(DataGridView grid) {
            for (int i = 0; i < RowCount; ++i) {
                grid.Rows.Add(Row(i).Values);
            }
        }

        //--------------------------------------------------------------------------------
        public void SetGridRows(DataGridView grid) {
            grid.Rows.Clear();
            AddGridRows(grid);
        }

        //--------------------------------------------------------------------------------
        public string[] GridHeaders {
            get {
                List<string> gridHeaders = new List<string>();
                for (int i = 0; i < ColumnCount; ++i) {
                    string header = Header(i);
                    if (string.IsNullOrEmpty(header))
                        header = "Col #" + (i + 1);
                    gridHeaders.Add(header);
                }
                return gridHeaders.ToArray();
            }
        }
            
        
        // TO STRING ================================================================================
        //--------------------------------------------------------------------------------
        //public override string ToString() {
        //    return "Page " + mPage + ", " + (mWholePage ? "Whole Page" : "(" + mX + ", " + mY + ") - (" + mWidth + ", " + mHeight + ")");
        //}
    }

}

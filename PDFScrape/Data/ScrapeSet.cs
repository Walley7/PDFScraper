using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Data {

    public class ScrapeSet {
        //================================================================================
        private List<ScrapeTable>               mTables = new List<ScrapeTable>();

        
        //================================================================================
        //--------------------------------------------------------------------------------
        public ScrapeSet() {

        }

        
        // TABLES ================================================================================
        //--------------------------------------------------------------------------------
        public ScrapeTable AddTable(int index, ScrapeTable table) {
            // Checks
            if (HasTable(table.Name))
                throw new DuplicateException();

            // Add
            mTables.Insert(index, table);
            return table;
        }

        //--------------------------------------------------------------------------------
        public ScrapeTable AddTable(int index, string name, int columnCount, string[] headers, string[][] rows) { return AddTable(index, new ScrapeTable(name, columnCount, headers, rows)); }
        public ScrapeTable AddTable(int index, string name, string[] headers, string[][] rows) { return AddTable(index, name, headers.Length, headers, rows); }
        public ScrapeTable AddTable(int index, string name, string[] headers) { return AddTable(index, name, headers.Length, headers, null); }
        public ScrapeTable AddTable(int index, string name, int columnCount, string[][] rows) { return AddTable(index, name, columnCount, null, rows); }
        public ScrapeTable AddTable(int index, string name, int columnCount) { return AddTable(index, name, columnCount, null, null); }
        public ScrapeTable AddTable(ScrapeTable table) { return AddTable(mTables.Count, table); }
        public ScrapeTable AddTable(string name, int columnCount, string[] headers, string[][] rows) { return AddTable(mTables.Count, name, columnCount, headers, rows); }
        public ScrapeTable AddTable(string name, string[] headers, string[][] rows) { return AddTable(mTables.Count, name, headers.Length, headers, rows); }
        public ScrapeTable AddTable(string name, string[] headers) { return AddTable(mTables.Count, name, headers.Length, headers, null); }
        public ScrapeTable AddTable(string name, int columnCount, string[][] rows) { return AddTable(mTables.Count, name, columnCount, null, rows); }
        public ScrapeTable AddTable(string name, int columnCount) { return AddTable(mTables.Count, name, columnCount, null, null); }
            
        //--------------------------------------------------------------------------------
        public void RemoveTable(int index) {
            mTables.RemoveAt(index);
        }

        //--------------------------------------------------------------------------------
        public void RemoveTable(string name) {
            // Checks
            if (name == null)
                throw new ArgumentNullException();
            if (name.Equals(""))
                throw new ArgumentException();

            // Remove
            int index = TableIndex(name);
            if (index == -1)
                throw new NotFoundException();
            RemoveTable(index);
        }

        //--------------------------------------------------------------------------------
        public void RemoveAllTables() {
            mTables.Clear();
        }
        
        //--------------------------------------------------------------------------------
        public ScrapeTable Table(int index) { return mTables[index]; }
        
        //--------------------------------------------------------------------------------
        public ScrapeTable Table(string name) {
            // Checks
            if (name == null)
                throw new ArgumentNullException();
            if (name.Equals(""))
                throw new ArgumentException();

            // Table
            int index = TableIndex(name);
            if (index == -1)
                throw new NotFoundException();
            return mTables[index];
        }

        //--------------------------------------------------------------------------------
        public int TableIndex(string name) {
            // Checks
            if (string.IsNullOrEmpty(name))
                return -1;

            // Table index
            for (int i = 0; i < mTables.Count; ++i) {
                if ((mTables[i].Name != null) && mTables[i].Name.Equals(name))
                    return i;
            }
            return -1;
        }

        //--------------------------------------------------------------------------------
        public bool HasTable(string name) { return (TableIndex(name) != -1); }
        public int TableCount { get { return mTables.Count; } }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Data {

    public class DataStore2 {
        //================================================================================
        protected List<DataStoreRecord2>         mRecords = new List<DataStoreRecord2>();
        

        // RECORDS ================================================================================
        //--------------------------------------------------------------------------------


        // VALUES ================================================================================
        //--------------------------------------------------------------------------------
        public DataStoreRecord2 AddRecord() {
            DataStoreRecord2 record = new DataStoreRecord2(mRecords.Count);
            mRecords.Add(record);
            return record;
        }

        //--------------------------------------------------------------------------------
        public void RemoveRecord(int index) { mRecords.RemoveAt(index); }
        public void RemoveAllRecords() { mRecords.Clear(); }
        public DataStoreRecord2 Record(int index) { return mRecords[index]; }
        public int RecordCount { get { return mRecords.Count; } }
        public List<DataStoreRecord2> Records { get { return mRecords; } }


        // STORAGE ================================================================================
        //--------------------------------------------------------------------------------
        // to/from CSV
        // to/from databases
        // Probably implement this as CSVDataStore and SQLDataStore, etc
    }

}

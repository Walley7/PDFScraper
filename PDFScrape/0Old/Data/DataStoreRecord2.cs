using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Data {

    public class DataStoreRecord2 {
        //================================================================================
        private int                             mIndex;

        private Dictionary<string, string>      mValues = new Dictionary<string, string>();


        //================================================================================
        //--------------------------------------------------------------------------------
        public DataStoreRecord2(int index) {
            mIndex = index;
        }
        
        
        // VALUES ================================================================================
        //--------------------------------------------------------------------------------
        public void AddValue(string field, string value) { mValues.Add(field, value); }
        public void RemoveValue(string field) { mValues.Remove(field); }
        public void RemoveAllValues() { mValues.Clear(); }
        public string Value(string field) { return mValues[field]; }
        public bool HasValue(string field) { return mValues.ContainsKey(field); }
        public int ValueCount { get { return mValues.Count; } }
        public Dictionary<string, string> Values { get { return mValues; } } 
    }

}

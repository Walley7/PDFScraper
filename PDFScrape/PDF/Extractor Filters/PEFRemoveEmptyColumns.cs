using PDFScrape.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.PDF.Extractor_Filters {

    public class PEFRemoveEmptyColumns : PDFColumnExtractorFilter {
        //================================================================================
        

        //================================================================================
        //--------------------------------------------------------------------------------
        public PEFRemoveEmptyColumns() : base() { }

        
        // FILTERING ================================================================================
        //--------------------------------------------------------------------------------
        public override ScrapeTable Apply(ScrapeTable table) {
            // Table
            ScrapeTable newTable = new ScrapeTable(table);

            // Remove empty columns
            for (int i = newTable.ColumnCount - 1; i >= 0; --i) {
                bool empty = true;
                for (int j = 0; j < table.RowCount; ++j) {
                    if (!string.IsNullOrEmpty(newTable.Row(j).Get(i)))
                        empty = false;
                }

                if (empty)
                    newTable.RemoveColumn(i);
            }

            return newTable;
        }


        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public override string TypeName { get { return "Remove Empty Columns"; } }

        
        // SETTINGS ================================================================================
        //--------------------------------------------------------------------------------
        public override float SettingsRows { get { return 0.25f; } }
    }

}

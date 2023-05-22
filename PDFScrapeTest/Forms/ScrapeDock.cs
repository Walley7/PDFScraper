using PDFScrape.Data;
using PDFScrape.PDF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;



namespace PDFScrapeTest.Forms {

    public partial class ScrapeDock : DockContent {
        //================================================================================
        private MainForm                        mMainForm;

        private DataStore2                       mDataStore = new DataStore2();


        //================================================================================
        //--------------------------------------------------------------------------------
        public ScrapeDock(MainForm mainForm) {
            InitializeComponent();
            mMainForm = mainForm;
        }
        

        // SCRAPING ================================================================================
        //--------------------------------------------------------------------------------
        private void btnScrape_Click(object sender, EventArgs e) {
            // Open
            if (dlgOpenPDF.ShowDialog() != DialogResult.OK)
                return;

            // PDF
            PDFReader2 reader = new PDFReader2(dlgOpenPDF.FileName, 72);

            // Scrape
            mDataStore.RemoveAllRecords();
            mMainForm.ModelDock.Model.Scrape(reader, mDataStore);
            //mDataStore.Save (to csv, database, etc)

            // Dispose
            reader.Dispose();



            // Region type - Scrape "$3,000" as 3000 if integer, 3000.00 if currency, etc
            // aka the type causes it to SCRAPE as that, so that data store can stay as is with strings
        }
        
        
        // INPUT ================================================================================
        //--------------------------------------------------------------------------------
        private void btnInput_Click(object sender, EventArgs e) {
            mMainForm.MacroDock.MacroPlayer.DataStoreRecord = mDataStore.Record(0);
            mMainForm.MacroDock.MacroPlayer.PlayAsync(1.0f);

            /*voterID = -1;
            while (voterID == -1) {
                string voterIDString = Interaction.InputBox("Voter ID", "Enter a voter ID", "");
                if (string.IsNullOrEmpty(voterIDString))
                    return false;
                if (!int.TryParse(voterIDString, out voterID))
                    voterID = -1;
            }
            return true;*/
        }
    }

}

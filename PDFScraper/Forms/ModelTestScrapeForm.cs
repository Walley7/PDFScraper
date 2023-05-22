using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PDFScraper.Forms {

    public partial class ModelTestScrapeForm : Form {
        //================================================================================
        //--------------------------------------------------------------------------------
        public ModelTestScrapeForm() {
            InitializeComponent();
        }
        

        // EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        private void ModelTestScrapeForm_Load(object sender, EventArgs e) {
            dgvScrape.Rows.Add();
            dgvScrape.Rows[0].Cells[0].Value = "SomeRegion";
            dgvScrape.Rows[0].Cells[1].Value = "Integer";
            dgvScrape.Rows[0].Cells[2].Value = true;
            dgvScrape.Rows[0].Cells[3].Value = "scraped 123";
            dgvScrape.Rows[0].Cells[4].Value = "123";
            dgvScrape.Rows.Add();
            dgvScrape.Rows[1].Cells[0].Value = "OtherRegion";
            dgvScrape.Rows[1].Cells[1].Value = "Text";
            dgvScrape.Rows[1].Cells[2].Value = false;
            dgvScrape.Rows[1].Cells[3].Value = "scraped text";
            dgvScrape.Rows[1].Cells[4].Value = "scraped text";
        }
        
        //--------------------------------------------------------------------------------
        private void btnClose_Click(object sender, EventArgs e) {
            Close();
        }
    }

}

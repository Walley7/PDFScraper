using PDFScrape.Scraping;
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

    public partial class RegionPropertiesDock : DockContent {
        //================================================================================
        private ModelDock                       mModelDock = null;

        private PDFScrapeRegion2                 mRegion = null;


        //================================================================================
        //--------------------------------------------------------------------------------
        public RegionPropertiesDock() {
            InitializeComponent();
        }


        // MODEL DOCK ================================================================================
        //--------------------------------------------------------------------------------
        public ModelDock ModelDock { set { mModelDock = value; } get { return mModelDock; } }

        
        // REGION ================================================================================
        //--------------------------------------------------------------------------------
        public void SetRegion(PDFScrapeRegion2 region) {
            mRegion = region;
            txtName.Text = region.Name;
            chkMandatory.Checked = region.Mandatory;
        }
        
        //--------------------------------------------------------------------------------
        private void txtName_TextChanged(object sender, EventArgs e) {
            if (mRegion != null) {
                if (!mRegion.Name.Equals(txtName.Text)) {
                    if (!mRegion.SetName(txtName.Text)) {
                        MessageBox.Show("The name '" + txtName.Text + "' is already in use.");
                        txtName.Text = mRegion.Name;
                    }
                }
            }
        }
        
        //--------------------------------------------------------------------------------
        private void chkMandatory_CheckedChanged(object sender, EventArgs e) {
            if (mRegion != null) {
                mRegion.Mandatory = chkMandatory.Checked;
                ModelDock.InvalidateRegionDrawArea(mRegion);
            }
        }
        
        //--------------------------------------------------------------------------------
        private void btnTestScrape_Click(object sender, EventArgs e) {
            if (mRegion != null)
                MessageBox.Show("Scrape: '" + mRegion.Scrape(ModelDock.PDFReader) + "'");
        }
    }

}

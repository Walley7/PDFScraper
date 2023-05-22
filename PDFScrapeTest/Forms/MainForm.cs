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

    public partial class MainForm : Form {
        //================================================================================
        private PDFScraper2                      mScraper = new PDFScraper2();

        private ModelDock                       mModelDock;
        private MacroDock                       mMacroDock;
        private ScrapeDock                      mScrapeDock;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MainForm() {
            // Initialise
            InitializeComponent();

            // Dock panel
            dckMain.Theme = new VS2015DarkTheme();
            
            // Region properties dock
            RegionPropertiesDock regionPropertiesDock = new RegionPropertiesDock();
            regionPropertiesDock.Show(dckMain, DockState.DockRight);

            // Model dock
            mModelDock = new ModelDock(regionPropertiesDock, mScraper.Model);
            mModelDock.Show(dckMain, DockState.Document);

            // Macro dock
            mMacroDock = new MacroDock();
            mMacroDock.Show(dckMain, DockState.Document);

            // Scrape dock
            mScrapeDock = new ScrapeDock(this);
            mScrapeDock.Show(dckMain, DockState.Document);
        }


        // DOCKS ================================================================================
        //--------------------------------------------------------------------------------
        public ModelDock ModelDock { get { return mModelDock; } }
        public MacroDock MacroDock { get { return mMacroDock; } }
    }

}

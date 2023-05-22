using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraWaitForm;
using PDFScrape.PDF;
using DevExpress.XtraBars.Docking;



namespace PDFScraper.Forms2.Docks {

    public partial class ModelDock : DockControl {
        //================================================================================
        public const int                        PDF_DPI = 96;


        //================================================================================
        private PDFScrapeModel                  mModel;
        private PDFScrapeModel.EventDelegate    mModelChangedDelegate = null;
        
        private PDFReader                       mPDFReader = null;


        //================================================================================
        //--------------------------------------------------------------------------------
        public ModelDock(MainForm form, DockPanel dockPanel, PDFScrapeModel model, string path = "") :
        base(form, dockPanel, path) {
            // Initialise
            InitializeComponent();

            // Model
            mModel = model;
            if (string.IsNullOrEmpty(Path)) 
                mModel.IncrementChangeCount(); // Mark new model as changed

            // Title
            UpdateTitle();

            // Fields
            txtTemplatePDFPath.DataBindings.Add("Text", mModel, "TemplatePDFPath", false, DataSourceUpdateMode.OnPropertyChanged);
        }


        // https://github.com/DevExpress-Examples/how-to-create-and-populate-an-unbound-column-e3354/blob/11.1.5%2B/CS/WindowsApplication1/Form1.cs
        // https://documentation.devexpress.com/WindowsForms/3500/Controls-and-Libraries/Data-Grid/Grouping
        // https://documentation.devexpress.com/WindowsForms/1967/Controls-and-Libraries/Data-Grid/Grouping/Working-with-Groups-in-Code


        // EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        protected override void OnCreate(object sender, EventArgs e) {
            // Model events
            mModelChangedDelegate = (m) => OnModelChanged(m);
            mModel.Changed += mModelChangedDelegate;

            // Extractors
            RefreshExtractorsGrid();
            //UpdateExtractReview();
        }

        //--------------------------------------------------------------------------------
        protected override void OnDestroy(object sender, EventArgs e) {
            // Model events
            mModel.Changed -= mModelChangedDelegate;
            mModelChangedDelegate = null;

            // Dispose
            if (mPDFReader != null)
                mPDFReader.Dispose();
        }

        //--------------------------------------------------------------------------------
        private void OnModelChanged(PDFScrapeModel model) {
            UpdateTitle();
        }


        // FORM / DOCK PANEL ================================================================================
        //--------------------------------------------------------------------------------
        public override void UpdateTitle() {
            DockPanel.Text = " " + (mModel.HasChanged ? "*" : "") + mModel.Name;
        }


        // MODEL ================================================================================
        //--------------------------------------------------------------------------------
        public PDFScrapeModel Model { get { return mModel; } }
        public override string DocumentName { get { return mModel.Name; } }
        public override bool HasChanged { get { return mModel.HasChanged; } }


        // PDF ================================================================================
        //--------------------------------------------------------------------------------
        private void txtTemplatePDFPath_TextChanged(object sender, EventArgs e) {
            // Dispose
            if (mPDFReader != null) {
                mPDFReader.Dispose();
                mPDFReader = null;
            }

            // Read
            if (!string.IsNullOrEmpty(txtTemplatePDFPath.Text)) {
                Form.ShowWaitOverlay(DockPanel);
                try { mPDFReader = new PDFReader(txtTemplatePDFPath.Text, PDF_DPI, Color.FromArgb(255, 0, 0), Color.FromArgb(100, 100, 255)); }
                catch (Exception ex) {
                    XtraMessageBox.Show("Failed to read template PDF: " + ex.Message, "PDF Scraper", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                Form.HideWaitOverlay();

                // Extractors
                RefreshExtractorsGrid();
            }
        }
        
        //--------------------------------------------------------------------------------
        private void btnSelectTemplatePDF_Click(object sender, EventArgs e) {
            if (dlgOpenPDF.ShowDialog() == DialogResult.OK)
                txtTemplatePDFPath.Text = dlgOpenPDF.FileName;
        }


        // EXTRACTORS ================================================================================
        //--------------------------------------------------------------------------------
        private void RefreshExtractorsGrid() {


            // Refresh
            grdExtractors.DataSource = mModel.Extractors.Select(e => new { Extractor = e.Name, Region = e.Region.ToString() }).ToList();



            /*// Selection
            PDFExtractor selectedExtractor = null;
            if (dgvExtractors.SelectedRows.Count > 0) {
                int selectedIndex = dgvExtractors.SelectedRows[0].Index;
                if (selectedIndex < mModel.ExtractorCount)
                    selectedExtractor = mModel.Extractor(selectedIndex);
            }

            // Refresh
            dgvExtractors.DataSource = mModel.Extractors.Select(x => new { Extractor = x.Name, Type = x.TypeString, Region = x.Region.ToString() }).ToList();

            // Restore selection
            if (selectedExtractor != null) {
                for (int i = 0; i < mModel.ExtractorCount; ++i) {
                    if (mModel.Extractor(i) == selectedExtractor)
                        dgvExtractors.Rows[i].Selected = true;
                }
            }*/
        }


        // SAVING ================================================================================
        //--------------------------------------------------------------------------------
        public override void Save(string path) {
            mModel.SaveToJSON(path);
        }
    }

}

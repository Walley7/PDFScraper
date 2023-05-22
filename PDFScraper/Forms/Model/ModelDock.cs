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



namespace PDFScraper.Forms.Model {

    public partial class ModelDock : DockContent {
        //================================================================================
        public const int                        PDF_DPI = 96;


        //================================================================================
        private ModelForm                       mForm;

        private PDFScrapeModel                  mModel;
        private string                          mPath = "";
        private PDFScrapeModel.EventDelegate    mModelChangedDelegate = null;

        private PDFReader                       mPDFReader = null;

        private Label                           mLoadingLabel = null;

        private bool                            mFloatPaneExists = false;


        //================================================================================
        //--------------------------------------------------------------------------------
        public ModelDock(ModelForm form, PDFScrapeModel model, string path = "") {
            // Initialise
            InitializeComponent();

            // Form
            mForm = form;
            
            // Model
            mModel = model;
            mPath = path;
            if (string.IsNullOrEmpty(mPath)) 
                mModel.IncrementChangeCount(); // Mark new model as changed

            // Title
            UpdateTitle();

            // Fields
            txtTemplatePDFPath.DataBindings.Add("Text", mModel, "TemplatePDFPath", false, DataSourceUpdateMode.OnPropertyChanged);
        }
        

        // FORM ================================================================================
        //--------------------------------------------------------------------------------
        private void ModelDock_Shown(object sender, EventArgs e) {
            // Model events
            mModelChangedDelegate = (m) => OnModelChanged(m);
            mModel.Changed += mModelChangedDelegate;

            // Extractors
            RefreshExtractorsGrid();
            UpdateExtractReview();
        }
        
        //--------------------------------------------------------------------------------
        private void ModelDock_FormClosing(object sender, FormClosingEventArgs e) {
            // Save changes
            if (e.CloseReason == CloseReason.UserClosing) {
                if (mModel.HasChanged) {
                    DialogResult result = MessageBox.Show("Save changes to following models?\n  " + mModel.Name, "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                        mForm.SaveModel(this);
                    else if (result == DialogResult.Cancel) {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        //--------------------------------------------------------------------------------
        private void ModelDock_FormClosed(object sender, FormClosedEventArgs e) {
            // Model events
            mModel.Changed -= mModelChangedDelegate;
            mModelChangedDelegate = null;

            // Dispose
            if (mPDFReader != null)
                mPDFReader.Dispose();
        }
        
        //--------------------------------------------------------------------------------
        private void ModelDock_DockStateChanged(object sender, EventArgs e) {
            if (!mFloatPaneExists && !(FloatPane is null)) {
                mFloatPaneExists = true;
                FloatPane.FloatWindow.Width = (int)((double)Width * 0.75);
                FloatPane.FloatWindow.Height = (int)((double)Height * 0.75);
            }
        }


        // EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        private void OnModelChanged(PDFScrapeModel model) {
            UpdateTitle();
        }


        // TITLE ================================================================================
        //--------------------------------------------------------------------------------
        public void UpdateTitle() {
            Text = (mModel.HasChanged ? "*" : "") + mModel.Name;
        }
        

        // MODEL ================================================================================
        //--------------------------------------------------------------------------------
        public PDFScrapeModel Model { get { return mModel; } }
        public bool HasChanged { get { return mModel.HasChanged; } }

        //--------------------------------------------------------------------------------
        public string Path {
            set { mPath = value; }
            get { return mPath; }
        }

        //--------------------------------------------------------------------------------
        public bool HasNoPath { get { return string.IsNullOrWhiteSpace(mPath); } }
        

        // PDF ================================================================================
        //--------------------------------------------------------------------------------
        private void txtTemplatePDFPath_TextChanged(object sender, EventArgs e) {
            // Dispose
            if (mPDFReader != null)
                mPDFReader.Dispose();

            // Read
            if (!string.IsNullOrEmpty(txtTemplatePDFPath.Text)) {
                ShowLoadingLabel("Loading PDF...");
                mPDFReader = new PDFReader(txtTemplatePDFPath.Text, PDF_DPI, Color.FromArgb(255, 0, 0), Color.FromArgb(100, 100, 255));
                HideLoadingLabel();

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
            // Selection
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
            }
        }
        
        //--------------------------------------------------------------------------------
        private void dgvExtractors_DoubleClick(object sender, EventArgs e) {
            btnEditExtractor.PerformClick();
        }

        //--------------------------------------------------------------------------------
        private void btnAddExtractor_MouseDown(object sender, MouseEventArgs e) {
            mnuAddExtractor.Show(btnAddExtractor, e.X - (mnuAddExtractor.Width / 2), e.Y - 13);
            mnuAddExtractor.Items[0].Select(); // Workaround for the item not highlighting when it opens under the cursor
        }
        
        //--------------------------------------------------------------------------------
        private void mnuAddExtractor_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            // Checks
            if (mPDFReader == null) {
                MessageBox.Show("Select a template PDF first.", "Select Template PDF");
                return;
            }

            // Create extractor
            PDFExtractor extractor = null;
            if (e.ClickedItem.Text.Equals("Table Extractor"))
                extractor = mModel.AddExtractor(PDFScrapeModel.ExtractorType.TABLE);
            else
                return;
            //else if (e.ClickedItem.Text.Equals("Text Extractor"))
            //    extractor = mModel.AddExtractor(PDFScrapeModel.ExtractorType.TEXT);

            // Edit extractor
            ExtractorForm form = new ExtractorForm(extractor, true, mPDFReader);
            form.ShowDialog();
            RefreshExtractorsGrid();
            UpdateExtractReview();
        }
        
        //--------------------------------------------------------------------------------
        private void btnEditExtractor_Click(object sender, EventArgs e) {
            // Checks
            if (dgvExtractors.SelectedRows.Count == 0)
                return;

            // Extractor
            int index = dgvExtractors.SelectedRows[0].Index;
            PDFExtractor extractor = mModel.Extractor(index);

            // Edit
            ExtractorForm form = new ExtractorForm(extractor, false, mPDFReader);
            form.ShowDialog();
            RefreshExtractorsGrid();
            UpdateExtractReview();
        }
        
        //--------------------------------------------------------------------------------
        private void btnDeleteExtractor_Click(object sender, EventArgs e) {
            // Checks
            if (dgvExtractors.SelectedRows.Count == 0)
                return;

            // Remove
            int index = dgvExtractors.SelectedRows[0].Index;
            mModel.RemoveExtractor(index);
            RefreshExtractorsGrid();
            UpdateExtractReview();
        }


        // EXTRACT REVIEW ================================================================================
        //--------------------------------------------------------------------------------
        private void UpdateExtractReview() {
            // Extract
            ScrapeSet extractionSet = mModel.Extract(mPDFReader);

            // Column count
            int columnCount = 0;
            for (int i = 0; i < extractionSet.TableCount; ++i) {
                if (extractionSet.Table(i).ColumnCount > columnCount)
                    columnCount = extractionSet.Table(i).ColumnCount;
            }

            // Columns
            dgvExtractReview.Columns.Clear();
            for (int i = 0; i < columnCount; ++i) {
                dgvExtractReview.Columns.Add(dgvExtractors.Name + "Column" + i, i.ToString());
            }

            // Styles
            DataGridViewCellStyle extractorStyle = new DataGridViewCellStyle();
            extractorStyle.BackColor = Color.FromArgb(160, 160, 160);
            extractorStyle.Font = new Font(dgvExtractReview.DefaultCellStyle.Font, FontStyle.Bold);
            DataGridViewCellStyle headersStyle = new DataGridViewCellStyle();
            headersStyle.BackColor = Color.FromArgb(200, 200, 200);
            headersStyle.Font = new Font(dgvExtractReview.DefaultCellStyle.Font, FontStyle.Bold);

            // Rows
            dgvExtractReview.Rows.Clear();
            
            for (int i = 0; i < extractionSet.TableCount; ++i) {
                ScrapeTable table = extractionSet.Table(i);
                if (i > 0)
                    dgvExtractReview.Rows.Add();
                dgvExtractReview.Rows.Add((i + 1) + ". " + mModel.Extractor(i).Name);
                dgvExtractReview.Rows.Add(table.GridHeaders);

                for (int j = 0; j < dgvExtractReview.ColumnCount; ++j) {
                    dgvExtractReview.Rows[dgvExtractReview.Rows.Count - 2].Cells[j].Style = extractorStyle;
                    dgvExtractReview.Rows[dgvExtractReview.Rows.Count - 1].Cells[j].Style = headersStyle;
                }
                
                for (int j = 0; j < table.RowCount; ++j) {
                    dgvExtractReview.Rows.Add(table.Row(j).Values);
                }
            }
        }

        //--------------------------------------------------------------------------------
        private void dgvExtractReview_SelectionChanged(object sender, EventArgs e) {
            dgvExtractReview.ClearSelection();  
        }


        // LOADING LABEL ================================================================================
        //--------------------------------------------------------------------------------
        public void ShowLoadingLabel(string text, int height = 150) {
            // Hide
            HideLoadingLabel();

            // Create
            mLoadingLabel = new Label() { Text = text, BorderStyle = BorderStyle.FixedSingle, AutoSize = false, TextAlign = ContentAlignment.MiddleCenter, Height = height };
            mLoadingLabel.Font = new Font(mLoadingLabel.Font.FontFamily, 48, FontStyle.Bold);
            mLoadingLabel.Top = (Height / 2) - (mLoadingLabel.Height / 2);
            mLoadingLabel.Width = Width;

            // Show
            Controls.Add(mLoadingLabel);
            mLoadingLabel.BringToFront();
            mLoadingLabel.Refresh();
        }
        
        //--------------------------------------------------------------------------------
        public void HideLoadingLabel() {
            if (mLoadingLabel != null) {
                Controls.Remove(mLoadingLabel);
                mLoadingLabel.Dispose();
                mLoadingLabel = null;
            }
        }
    }

}

using PDFScrape.PDF;
using PDFScraper.Forms.Macros;
using PDFScraper.Forms.Scraper;
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

    public partial class ModelForm : Form {
        //================================================================================
        private LaunchForm                      mLaunchForm;
        private bool                            mReturnToLaunchForm = true;

        private Label                           mLoadingLabel = null;


        //================================================================================
        //--------------------------------------------------------------------------------
        public ModelForm(LaunchForm launchForm) {
            // Initialise
            InitializeComponent();

            // Launch form
            mLaunchForm = launchForm;

            // Theme
            dckMain.Theme = new VS2015DarkTheme();

            // Model
            //NewModel();
        }
        

        // FORM ================================================================================
        //--------------------------------------------------------------------------------
        private void ModelForm_FormClosing(object sender, FormClosingEventArgs e) {
            // Unsaved docks
            List<ModelDock> unsavedDocks = new List<ModelDock>();
            foreach (IDockContent d in dckMain.Documents) {
                ModelDock dock = (ModelDock)d;
                if (dock.Model.HasChanged)
                    unsavedDocks.Add(dock);
            }

            // Save changes
            if (unsavedDocks.Count > 0) {
                string saveChangesText = "Save changes to following models?";
                unsavedDocks.ForEach(d => saveChangesText += "\n  " + d.Model.Name);

                DialogResult result = MessageBox.Show(saveChangesText, "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes) {
                    // Save
                    foreach (ModelDock d in unsavedDocks) {
                        SaveModel(d);
                    }
                }
                else if (result == DialogResult.Cancel) {
                    // Cancel
                    e.Cancel = true;
                    mReturnToLaunchForm = true;
                    return;
                }
            }
        }

        //--------------------------------------------------------------------------------
        private void ModelForm_FormClosed(object sender, FormClosedEventArgs e) {
            // Launch form
            if (mReturnToLaunchForm) {
                mLaunchForm.MoveToCentre();
                mLaunchForm.Show();
            }
        }
        

        // NAVIGATION BUTTONS ================================================================================
        //--------------------------------------------------------------------------------
        private void btnScraper_Click(object sender, EventArgs e) {
            // Close
            mReturnToLaunchForm = false;
            Close();

            // Scraper
            if (IsDisposed) {
                ScraperForm form = new ScraperForm(mLaunchForm);
                form.Show();
            }
        }
        
        //--------------------------------------------------------------------------------
        private void btnModelEditor_Click(object sender, EventArgs e) {
            // Close
            mReturnToLaunchForm = false;
            Close();

            // Model editor
            if (IsDisposed) {
                ModelForm form = new ModelForm(mLaunchForm);
                form.Show();
            }
        }
        
        //--------------------------------------------------------------------------------
        private void btnMacroEditor_Click(object sender, EventArgs e) {
            // Close
            mReturnToLaunchForm = false;
            Close();

            // Macro editor
            if (IsDisposed) {
                MacroForm form = new MacroForm(mLaunchForm);
                form.Show();
            }
        }

        //--------------------------------------------------------------------------------
        private void btnExit_Click(object sender, EventArgs e) {
            Close();
        }

        
        // DOCUMENT BUTTONS ================================================================================
        //--------------------------------------------------------------------------------
        private void btnNew_Click(object sender, EventArgs e) { NewModel(); }
        private void btnOpen_Click(object sender, EventArgs e) { OpenModel(); }
        private void btnSave_Click(object sender, EventArgs e) { SaveModel(); }
        private void btnSaveAs_Click(object sender, EventArgs e) { SaveModel(true); }

        
        // MODELS ================================================================================
        //--------------------------------------------------------------------------------
        public void NewModel() {
            // Name
            string name = "";
            int i = 1;
            while (true) {
                name = "Untitled" + i;

                bool foundName = false;
                foreach (IDockContent d in dckMain.Documents) {
                    if (((ModelDock)d).Model.Name.Equals(name)) {
                        foundName = true;
                        break;
                    }
                }

                if (!foundName)
                    break;
                ++i;
            }

            // Create / open
            OpenModelDock(new PDFScrapeModel(null, name));
        }

        //--------------------------------------------------------------------------------
        public void OpenModel() {
            // Path
            if (dlgOpenModel.ShowDialog() != DialogResult.OK)
                return;

            // Existing documents
            foreach (DockContent d in dckMain.Contents) {
                ModelDock dock = (ModelDock)d;
                if (dock.Path.Equals(dlgOpenModel.FileName)) {
                    d.Activate();
                    return;
                }
            }

            // Model
            PDFScrapeModel model;
            try { model = new PDFScrapeModel(dlgOpenModel.FileName); }
            catch (Exception e) {
                MessageBox.Show("Failed to open: " + e.Message, "Open", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Dock
            ShowLoadingLabel("Loading Model...");
            OpenModelDock(model, dlgOpenModel.FileName);
            HideLoadingLabel();
        }

        //--------------------------------------------------------------------------------
        public void SaveModel(bool saveAs = false) {
            // Checks
            if (dckMain.ActiveDocument == null || !(dckMain.ActiveDocument is ModelDock))
                return;

            // Save
            ModelDock modelDock = (ModelDock)dckMain.ActiveDocument;
            SaveModel(modelDock, saveAs);
        }

        //--------------------------------------------------------------------------------
        public void SaveModel(ModelDock modelDock, bool saveAs = false) {
            // Save as
            if (saveAs || modelDock.HasNoPath) {
                if (dlgSaveModel.ShowDialog() != DialogResult.OK)
                    return;
                modelDock.Path = dlgSaveModel.FileName;
            }

            // Save
            try { modelDock.Model.SaveToJSON(modelDock.Path); }
            catch (Exception e) { MessageBox.Show("Failed to save: " + e.Message, "Save", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            // Title
            modelDock.UpdateTitle();
        }

        
        // MODEL DOCKS ================================================================================
        //--------------------------------------------------------------------------------
        private void OpenModelDock(PDFScrapeModel model, string path = "") {
            ModelDock dock = new ModelDock(this, model, path);
            dock.Show(dckMain, DockState.Document);
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

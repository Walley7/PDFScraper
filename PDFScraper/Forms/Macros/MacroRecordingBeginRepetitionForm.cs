using PDFScrape.Macros;
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



namespace PDFScraper.Forms.Macros {

    public partial class MacroRecordingBeginRepetitionForm : Form {
        //================================================================================
        private PDFScrapeModel                  mSampleModel;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroRecordingBeginRepetitionForm(PDFScrapeModel sampleModel) {
            // Initialise
            InitializeComponent();

            // Sample model
            mSampleModel = sampleModel;
        }
        

        // FORM ================================================================================
        //--------------------------------------------------------------------------------
        private void MacroRecordingBeginRepetitionForm_Shown(object sender, EventArgs e) {
            // Mode
            cboMode.SelectedIndex = 0;

            // Extractors
            if (mSampleModel != null) {
                // Items
                string[] extractorNames = mSampleModel.ExtractorNames;
                cboExtractor.Items.AddRange(extractorNames);

                // Auto completion
                AutoCompleteStringCollection autoCompleteSource = new AutoCompleteStringCollection();
                autoCompleteSource.AddRange(extractorNames);
                cboExtractor.AutoCompleteCustomSource = autoCompleteSource;
            }
        }
        

        // VALIDATION ================================================================================
        //--------------------------------------------------------------------------------
        private void btnOk_Click(object sender, EventArgs e) {
            // Dialog result
            this.DialogResult = DialogResult.None;

            // Validation
            if (string.IsNullOrEmpty(cboExtractor.Text)) {
                MessageBox.Show("Please enter an extractor name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboExtractor.Focus();
                return;
            }

            // Validated
            this.DialogResult = DialogResult.OK;
        }


        // FIELDS ================================================================================
        //--------------------------------------------------------------------------------
        public MacroRepetitionNode.RepetitionMode Mode {
            get {
                switch (cboMode.Text.ToLower()) {
                    case "per row": return MacroRepetitionNode.RepetitionMode.PER_ROW;
                    default:        return MacroRepetitionNode.RepetitionMode.PER_ROW;
                }
            }
        }

        //--------------------------------------------------------------------------------
        public string ExtractorName { get { return cboExtractor.Text; } }
    }

}

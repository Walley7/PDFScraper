using PDFScrape.Data;
using PDFScrape.Macros;
using PDFScrape.Macros.Events;
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

    public partial class MacroRecordingDataInputForm : Form {
        //================================================================================
        private MacroRecorder                   mRecorder;

        private PDFScrapeModel                  mSampleModel;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroRecordingDataInputForm(MacroRecorder recorder, PDFScrapeModel sampleModel) {
            // Initialise
            InitializeComponent();

            // Recorder
            mRecorder = recorder;

            // Sample model
            mSampleModel = sampleModel;
        }

        
        // FORM ================================================================================
        //--------------------------------------------------------------------------------
        private void MacroRecordingDataInputForm_Shown(object sender, EventArgs e) {
            // Sources
            for (int i = 0; i < mRecorder.RepetitionDepth; ++i) {
                cboSource.Items.Add("Repetition row " + (i + 1));
            }
            for (int i = 0; i < mRecorder.RepetitionDepth; ++i) {
                cboSource.Items.Add("Extractor row at repetition position " + (i + 1));
            }

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
            if (string.IsNullOrEmpty(cboSource.Text)) {
                MessageBox.Show("Please select a source.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboSource.Focus();
                return;
            }

            if (SourceIsExtractor && string.IsNullOrEmpty(cboExtractor.Text)) {
                MessageBox.Show("Please enter an extractor name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboExtractor.Focus();
                return;
            }

            if (chkColumnByHeader.Checked && string.IsNullOrEmpty(cboColumnHeader.Text)) {
                MessageBox.Show("Please enter a column header .", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboColumnHeader.Focus();
                return;
            }
            
            if (!chkColumnByHeader.Checked && string.IsNullOrEmpty(cboColumnRelativeTo.Text)) {
                MessageBox.Show("Please select a column.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboColumnRelativeTo.Focus();
                return;
            }

            // Validated
            this.DialogResult = DialogResult.OK;
        }
        

        // CONTROLS ================================================================================
        //--------------------------------------------------------------------------------
        private void cboSource_SelectedIndexChanged(object sender, EventArgs e) {
            // Extractor
            cboExtractor.Enabled = (SourceType != Macro.SourceType.REPETITION_ROW);
            if (!cboExtractor.Enabled)
                cboExtractor.Text = "";

            // Headers
            UpdateHeaderList();
        }
     
        //--------------------------------------------------------------------------------   
        private void cboExtractor_TextChanged(object sender, EventArgs e) {
            UpdateHeaderList();
        }

        //--------------------------------------------------------------------------------
        private void chkColumnByHeader_CheckedChanged(object sender, EventArgs e) {
            // Header
            lblColumnHeader.Enabled = chkColumnByHeader.Checked;
            cboColumnHeader.Enabled = chkColumnByHeader.Checked;

            // Column
            lblColumnPosition.Enabled = !chkColumnByHeader.Checked;
            cboColumnRelativeTo.Enabled = !chkColumnByHeader.Checked;
            numColumnPosition.Enabled = !chkColumnByHeader.Checked;
        }


        // HEADERS ================================================================================
        //--------------------------------------------------------------------------------
        private void UpdateHeaderList() {
            // Clear
            cboColumnHeader.Items.Clear();

            // Checks
            if (mSampleModel == null)
                return;

            // Extractor name
            string extractorName = "";
            if (SourceIsExtractor)
                extractorName = cboExtractor.Text;
            else if (SourceType == Macro.SourceType.REPETITION_ROW) {
                // Repetition
                MacroRepetitionNode repetitionNode = mRecorder.Repetition(RepetitionIndex);
                if (repetitionNode == null)
                    return;
                extractorName = repetitionNode.ExtractorName;
            }
            
            // Extractor
            PDFExtractor extractor = mSampleModel.Extractor(extractorName);
            if (extractor == null)
                return;

            // Items
            string[] headerPrefixes = extractor.DistinctHeaderPrefixes;
            cboColumnHeader.Items.AddRange(headerPrefixes);

            // Auto completion
            AutoCompleteStringCollection autoCompleteSource = new AutoCompleteStringCollection();
            autoCompleteSource.AddRange(headerPrefixes);
            cboColumnHeader.AutoCompleteCustomSource = autoCompleteSource;
        }


        // FIELDS ================================================================================
        //--------------------------------------------------------------------------------
        public string Source { get { return cboSource.Text; } }

        //--------------------------------------------------------------------------------
        public Macro.SourceType SourceType {
            get {
                if (cboSource.Text.StartsWith("Repetition row"))
                    return Macro.SourceType.REPETITION_ROW;
                else if (cboSource.Text.StartsWith("Extractor row at repetition position"))
                    return Macro.SourceType.EXTRACTOR_ROW_AT_REPETITION_POSITION;
                else
                    return Macro.SourceType.EXTRACTOR;
            }
        }

        //--------------------------------------------------------------------------------
        public bool SourceIsExtractor { get { return (SourceType == Macro.SourceType.EXTRACTOR) || (SourceType == Macro.SourceType.EXTRACTOR_ROW_AT_REPETITION_POSITION); } }
        
        //--------------------------------------------------------------------------------
        public int RepetitionIndex {
            get {
                if (SourceType == Macro.SourceType.EXTRACTOR)
                    return 0;
                else
                    return int.Parse(string.Concat(cboSource.Text.ToArray().Reverse().TakeWhile(char.IsNumber).Reverse())) - 1;
            }
        }

        //--------------------------------------------------------------------------------
        public string Extractor { get { return cboExtractor.Text; } }

        //--------------------------------------------------------------------------------
        public PDFExtractor.ColumnSpecifier ColumnSpecifier {
            get {
                if (chkColumnByHeader.Checked)
                    return new PDFExtractor.ColumnSpecifier(cboColumnHeader.Text);
                else
                    return new PDFExtractor.ColumnSpecifier(ColumnPosition);
            }
        }

        //--------------------------------------------------------------------------------
        public bool ColumnByHeader { get { return chkColumnByHeader.Checked; } }
        public string ColumnHeader { get { return cboColumnHeader.Text; } }
        public ScrapeColumnSpecifier ColumnPosition { get { return new ScrapeColumnSpecifier(ColumnPositionIndex, ColumnPositionRelativeTo); } }

        //--------------------------------------------------------------------------------
        public ScrapeColumnSpecifier.RelativePoint ColumnPositionRelativeTo {
            get {
                switch (cboColumnRelativeTo.Text.ToLower()) {
                    case "start":   return ScrapeColumnSpecifier.RelativePoint.START;
                    case "end":     return ScrapeColumnSpecifier.RelativePoint.END;
                    default:        return ScrapeColumnSpecifier.RelativePoint.START;
                }
            }
        }

        //--------------------------------------------------------------------------------
        public int ColumnPositionIndex { get { return (int)numColumnPosition.Value; } }
    }

}

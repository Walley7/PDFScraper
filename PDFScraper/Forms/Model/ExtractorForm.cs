using PDFScrape.Data;
using PDFScrape.PDF;
using PDFScrape.PDF.Attributes;
using PDFScrape.PDF.Extractor_Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PDFScraper.Forms.Model {

    public partial class ExtractorForm : Form {
        //================================================================================
        private const int                       FILTER_ROW_PADDING = 3;


        //================================================================================
        private PDFExtractor                    mExtractor;
        private bool                            mNew;

        private PDFReader                       mPDFReader;

        private int                             mNewFilterIndex = 0;

        private List<FilterControls>            mFilterControls = new List<FilterControls>();

        private bool                            mResettingHeaderCellValue = false;
        
        private int                             mPanelScroll = 0;


        //================================================================================
        //--------------------------------------------------------------------------------
        public ExtractorForm(PDFExtractor extractor, bool newExtractor, PDFReader pdfReader) {
            // Initialise
            InitializeComponent();

            // Extractor
            mExtractor = extractor;
            mNew = newExtractor;

            // PDF reader
            mPDFReader = pdfReader;

            // Fields
            cboType.Text = mExtractor.TypeString;
            txtName.DataBindings.Add("Text", mExtractor, "Name", false, DataSourceUpdateMode.Never);
            chkSplitLinesIntoRows.DataBindings.Add("Checked", mExtractor, "SplitLinesIntoRows", true, DataSourceUpdateMode.OnPropertyChanged);

            // Region
            UpdateRegion();

            // Filters / extraction
            AddFilterControls();
        }
        

        // FORM ================================================================================
        //--------------------------------------------------------------------------------
        private void ExtractorForm_Shown(object sender, EventArgs e) {
            Extract(); // Has to be done here for sizing to occur properly
        }

        //--------------------------------------------------------------------------------
        private void ExtractorForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (this.DialogResult == DialogResult.OK) {
                // Save
            }
            else {
                // Cancel confirmation
                if (MessageBox.Show("Cancel changes?", "Cancel?", MessageBoxButtons.YesNo) == DialogResult.No) {
                    e.Cancel = true;
                    return;
                }

                // Cancel
                if (mNew)
                    mExtractor.Model.RemoveExtractor(mExtractor);
            }

            // Filters
            RemoveAllFilterControls();

            // Bindings
            txtName.DataBindings.RemoveAt(0);
            chkSplitLinesIntoRows.DataBindings.RemoveAt(0);
        }
        

        // FIELDS ================================================================================
        //--------------------------------------------------------------------------------
        private void txtName_Leave(object sender, EventArgs e) {
            if (!mExtractor.SetName(txtName.Text)) {
                MessageBox.Show("Name already in use.");
                txtName.Text = mExtractor.Name;
            }
        }
        

        // REGION ================================================================================
        //--------------------------------------------------------------------------------
        private void btnSelectRegion_Click(object sender, EventArgs e) {
            // Checks
            if (mPDFReader == null) {
                MessageBox.Show("Select a template PDF first.", "Select Template PDF");
                return;
            }

            // Select
            ExtractorRegionForm form = new ExtractorRegionForm(mExtractor.Region, mPDFReader);
            form.ShowDialog();

            // Update
            UpdateRegion();

            // Extract
            Extract();
        }
        
        //--------------------------------------------------------------------------------
        private void UpdateRegion() {
            txtRegion.Text = mExtractor.Region.ToString();
        }


        // OPTIONS ================================================================================
        //--------------------------------------------------------------------------------
        // We use this instead of CheckedChanged as this fires after the data binding
        // update, whereas CheckedChanged doesn't.
        private void chkSplitLinesIntoRows_CheckStateChanged(object sender, EventArgs e) {
            Extract();
        }


        // EXTRACTION ================================================================================
        //--------------------------------------------------------------------------------
        private void Extract() {
            // Extract
            ScrapeTable extract0 = mExtractor.Extract(mPDFReader, 0);
            extract0.SetGridColumns(dgvExtract0);
            extract0.SetGridRows(dgvExtract0);
            tlpFilters.RowStyles[1].Height = (dgvExtract0.ColumnHeadersHeight * 2) + dgvExtract0.Rows.Cast<DataGridViewRow>().Sum(r => r.Height) +
                                             dgvExtract0.Margin.Top + dgvExtract0.Margin.Bottom;

            // Headers
            mExtractor.SetHeaderGridColumns(dgvExtractHeaders, extract0.ColumnCount);
            mExtractor.SetHeaderGridRows(dgvExtractHeaders, extract0.ColumnCount);

            // Filters
            Extract(0);
        }
        
        //--------------------------------------------------------------------------------
        private void Extract(int filterIndex) {
            for (int i = filterIndex; i < mExtractor.FilterCount; ++i) {
                // Data grid
                DataGridView dataGrid = mFilterControls[i].dataGrid;

                // Extract
                ScrapeTable extract = mExtractor.Extract(mPDFReader, i + 1);
                extract.SetGridColumns(dataGrid);
                extract.SetGridRows(dataGrid);

                // Layout
                int rowIndex = 2 + (i * 2);
                tlpFilters.RowStyles[rowIndex + 1].Height = (dataGrid.ColumnHeadersHeight * 2) + dataGrid.Rows.Cast<DataGridViewRow>().Sum(r => r.Height) +
                                                            dataGrid.Margin.Top + dataGrid.Margin.Bottom + mFilterControls[i].settingsHeight;
            }

            // Layout
            UpdateFilterPanelDimensions();
        }


        // HEADERS ================================================================================
        //--------------------------------------------------------------------------------
        private void dgvExtractHeaders_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            // Checks
            if (mResettingHeaderCellValue)
                return;

            // Update
            if (mExtractor.SetHeader(e.ColumnIndex, (string)dgvExtractHeaders.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                Extract();
            else {
                mResettingHeaderCellValue = true;
                dgvExtractHeaders.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = mExtractor.Header(e.ColumnIndex);
                mResettingHeaderCellValue = false;
            }
        }

        
        // FILTERS ================================================================================
        //--------------------------------------------------------------------------------
        private void AddFilter(int index, PDFExtractorFilter filter) {
            // Suspend layout
            SuspendFilterControlsLayout();

            // Add
            mExtractor.AddFilter(index, filter);
            AddFilterControls(index);
            
            // Resume layout
            ResumeFilterControlsLayout();
            
            // Suspend layout
            SuspendFilterControlsLayout();

            // Extract
            Extract(index);
            
            // Resume layout
            ResumeFilterControlsLayout();
        }

        //--------------------------------------------------------------------------------
        private void RemoveFilter(int index) {
            // Suspend layout
            SuspendFilterControlsLayout();

            // Remove
            PDFExtractorFilter filter = mExtractor.Filter(index);
            mExtractor.RemoveFilter(index);
            RemoveFilterControls(index, filter);
            Extract(index);

            // Resume layout
            ResumeFilterControlsLayout();
        }

        //--------------------------------------------------------------------------------
        private void btnAddFilterEnd_MouseDown(object sender, MouseEventArgs e) {
            mNewFilterIndex = mExtractor.FilterCount;
            mnuAddFilter.Show(btnAddFilterEnd, e.X - (btnAddFilterEnd.Width / 2), e.Y - 13);
            mnuAddFilter.Items[0].Select(); // Workaround for the item not highlighting when it opens under the cursor
        }
        
        //--------------------------------------------------------------------------------
        private void mnuAddFilterColumns_InsertColumns_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFInsertColumns()); }
        private void mnuAddFilterColumns_KeepColumnRange_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFKeepColumnRange()); }
        private void mnuAddFilterColumns_RemoveColumnRange_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFRemoveColumnRange()); }
        private void mnuAddFilterColumns_RemoveEmptyColumns_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFRemoveEmptyColumns()); }
        private void mnuAddFilterColumns_MoveColumn_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFMoveColumn()); }
        private void mnuAddFilterColumns_MergeColumns_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFMergeColumns()); }
        private void mnuAddFilterColumns_SplitColumnAtText_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFSplitColumnAtText()); }
        private void mnuAddFilterColumns_SplitColumnAtWhitespace_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFSplitColumnAtWhitespace()); }
        private void mnuAddFilterColumns_SplitColumnAtLineBreaks_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFSplitColumnAtLineBreaks()); }
        private void mnuAddFilterColumns_SplitColumnAfterNWords_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFSplitColumnAfterNWords()); }
        private void mnuAddFilterColumns_SplitColumnAfterNCharacters_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFSplitColumnAfterNCharacters()); }

        //--------------------------------------------------------------------------------
        private void mnuAddFilterRows_KeepRowRange_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFKeepRowRange()); }
        private void mnuAddFilterRows_KeepRowsBetween_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFKeepRowsBetween()); }
        private void mnuAddFilterRows_KeepRowsWhere_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFKeepRowsWhere()); }
        private void mnuAddFilterRows_RemoveRowRange_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFRemoveRowRange()); }
        private void mnuAddFilterRows_RemoveRowsBetween_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFRemoveRowsBetween()); }
        private void mnuAddFilterRows_RemoveRowsWhere_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFRemoveRowsWhere()); }
        private void mnuAddFilterRows_MergeRows_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFMergeRows()); }

        //--------------------------------------------------------------------------------
        private void mnuAddFilterCells_AddSetText_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFAddSetText()); }
        private void mnuAddFilterCells_CopyText_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFCopyText()); }
        private void mnuAddFilterCells_ReplaceText_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFReplaceText()); }
        private void mnuAddFilterCells_ReformatNumbers_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFReformatNumbers()); }
        private void mnuAddFilterCells_ReformatDates_Click(object sender, EventArgs e) { AddFilter(mNewFilterIndex, new PEFReformatDatesTimes()); }


        // FILTER CONTROLS ================================================================================
        //--------------------------------------------------------------------------------
        private void AddFilterControls(int filterIndex) {
            // Checks
            if (filterIndex < 0 || filterIndex > FilterCount)
                throw new IndexOutOfRangeException();
            
            // Filter
            PDFExtractorFilter filter = mExtractor.Filter(filterIndex);
            PropertyChangedEventHandler propertyChangedDelegate = (sender, e) => Extract(filterIndex);
            filter.PropertyChanged += propertyChangedDelegate;

            // Row index
            int rowIndex = 2 + (filterIndex * 2);

            // Subsequent controls
            foreach (Control c in tlpFilters.Controls) {
                if (tlpFilters.GetRow(c) >= rowIndex)
                    tlpFilters.SetRow(c, tlpFilters.GetRow(c) + 2);
            }

            // Row styles
            tlpFilters.RowStyles.Insert(rowIndex, new RowStyle(SizeType.Absolute, 26.0f));
            tlpFilters.RowStyles.Insert(rowIndex + 1, new RowStyle(SizeType.Absolute, 100.0f));

            // Add / remove buttons
            Button addButton = new Button() { Text = "+", Dock = DockStyle.Fill, Anchor = AnchorStyles.Right };
            addButton.Font = new Font(addButton.Font, FontStyle.Bold);
            addButton.Height = 20;
            tlpFilters.Controls.Add(addButton, 1, rowIndex);
            addButton.MouseDown += new MouseEventHandler((sender, e) => {
                mNewFilterIndex = mExtractor.FilterIndex(filter);
                mnuAddFilter.Show(addButton, e.X - (addButton.Width / 2), e.Y - 13);
                mnuAddFilter.Items[0].Select(); // Workaround for the item not highlighting when it opens under the cursor
            });

            Button removeButton = new Button() { Text = "-", Dock = DockStyle.Right, Anchor = AnchorStyles.Right };
            removeButton.Font = new Font(removeButton.Font, FontStyle.Bold);
            removeButton.Height = 20;
            tlpFilters.Controls.Add(removeButton, 1, rowIndex + 1);
            removeButton.Click += new EventHandler((sender, e) => RemoveFilter(mExtractor.FilterIndex(filter)));

            // Panel
            TableLayoutPanel panel = new TableLayoutPanel() { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 0) };
            int settingsHeight = (int)Math.Round(24.0f + filter.SettingsRows * 27.0f);
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, settingsHeight));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100.0f));
            tlpFilters.Controls.Add(panel, 0, rowIndex + 1);

            // Data grid
            DataGridView dataGrid = new DataGridView() {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
                RowHeadersVisible = false
            };
            dataGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            panel.Controls.Add(dataGrid, 0, 1);

            // Top panel
            TableLayoutPanel topPanel = new TableLayoutPanel() { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 0) };
            topPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 26.0f));
            topPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100.0f));
            panel.Controls.Add(topPanel, 0, 0);

            // Heading
            Label headingLabel = new Label() { Text = (filterIndex + 1) + ". " + filter.TypeName, AutoSize = true, Anchor = AnchorStyles.Bottom | AnchorStyles.Left };
            headingLabel.Font = new Font(headingLabel.Font, FontStyle.Bold);
            topPanel.Controls.Add(headingLabel, 0, 0);

            // Settings panel
            FlowLayoutPanel settingsPanel = new FlowLayoutPanel() { Dock = DockStyle.Fill, Margin = new Padding(10, 0, 0, 0) };
            topPanel.Controls.Add(settingsPanel, 0, 1);

            // Filter setting controls
            AddFilterSettingControls(settingsPanel, filter, filterIndex);

            // Add
            mFilterControls.Insert(filterIndex, new FilterControls(addButton, panel, dataGrid, headingLabel, settingsHeight, propertyChangedDelegate));

            // Headings
            UpdateFilterHeadings();

            // Layout
            tlpFilters.RowCount = tlpFilters.RowStyles.Count;
            UpdateFilterPanelDimensions();

            // Scroll
            ScrollToFilterPanelRow(rowIndex);
        }
        
        //--------------------------------------------------------------------------------
        private void AddFilterSettingControls(FlowLayoutPanel panel, PDFExtractorFilter filter, int filterIndex) {
            // Properties
            PropertyInfo[] filterProperties = filter.GetType().GetProperties();
            foreach (PropertyInfo p in filterProperties) {
                // Filter settings
                APDFFilterSetting[] filterSettings = (APDFFilterSetting[])p.GetCustomAttributes(typeof(APDFFilterSetting), true);
                foreach (APDFFilterSetting s in filterSettings) {
                    // Control
                    Control settingControl = CreateFilterSettingControl(filter, filterIndex, p, s);

                    if (settingControl != null) {
                        // Label default padding
                        int labelDefaultPadding = (panel.Controls.Count != 0 ? 5 : 0);

                        // Add / bind control (front)
                        if (!s.LabelInFront)
                            panel.Controls.Add(settingControl);

                        // Label
                        if (!string.IsNullOrEmpty(s.Display)) {
                            Label settingLabel = new Label() { Text = s.Display, AutoSize = true, Anchor = AnchorStyles.Left,
                                                               Margin = s.LabelInFront ? new Padding(labelDefaultPadding + s.Padding, 3, 0, 0) : new Padding(0, 3, 3, 0) };
                            panel.Controls.Add(settingLabel);
                        }

                        // Add / bind control (back)
                        if (s.LabelInFront)
                            panel.Controls.Add(settingControl);
                    }
                }
            }
        }
        
        //--------------------------------------------------------------------------------
        private Control CreateFilterSettingControl(PDFExtractorFilter filter, int filterIndex, PropertyInfo property, APDFFilterSetting setting) {
            // Margin
            Padding margin = setting.LabelInFront ? new Padding(0, 3, 3, 0) : new Padding(setting.Padding, 3, 0, 0);

            // Control
            switch (setting.Control) {
                case APDFFilterSetting.ControlType.STRING: {
                    TextBox control = new TextBox() { Margin = margin, Width = setting.Width };
                    control.DataBindings.Add("Text", filter, property.Name, true, DataSourceUpdateMode.OnPropertyChanged);
                    return control;
                }

                case APDFFilterSetting.ControlType.BOOLEAN: {
                    CheckBox control = new CheckBox() { Margin = margin, Width = 13 };
                    control.DataBindings.Add("Checked", filter, property.Name, true, DataSourceUpdateMode.OnPropertyChanged);
                    return control;
                }

                case APDFFilterSetting.ControlType.INTEGER: {
                    NumericUpDown control = new NumericUpDown() { Margin = margin, Width = setting.Width, Minimum = -1000, Maximum = 1000 };
                    control.DataBindings.Add("Value", filter, property.Name, true, DataSourceUpdateMode.OnPropertyChanged);
                    return control;
                }

                case APDFFilterSetting.ControlType.POSITIVE_INTEGER: {
                    NumericUpDown control = new NumericUpDown() { Margin = margin, Width = setting.Width, Minimum = 1, Maximum = 1000 };
                    control.DataBindings.Add("Value", filter, property.Name, true, DataSourceUpdateMode.OnPropertyChanged);
                    return control;
                }

                case APDFFilterSetting.ControlType.POSITIVE_ZERO_INTEGER: {
                    NumericUpDown control = new NumericUpDown() { Margin = margin, Width = setting.Width, Minimum = 0, Maximum = 1000 };
                    control.DataBindings.Add("Value", filter, property.Name, true, DataSourceUpdateMode.OnPropertyChanged);
                    return control;
                }

                case APDFFilterSetting.ControlType.LIST: {
                    ComboBox control = new ComboBox() { Margin = margin, Width = setting.Width, DropDownStyle = ComboBoxStyle.DropDownList };
                    foreach (string s in setting.ListValues) {
                        control.Items.Add(s);
                    }
                    control.DataBindings.Add("SelectedIndex", filter, property.Name, true, DataSourceUpdateMode.OnPropertyChanged);
                    return control;
                }

                case APDFFilterSetting.ControlType.COLUMN_RELATIVE_POINT: {
                    ComboBox control = new ComboBox() { Margin = margin, Width = setting.Width, DropDownStyle = ComboBoxStyle.DropDownList };
                    control.Items.Add("Start");
                    control.Items.Add("End");
                    control.DataBindings.Add("SelectedIndex", filter, property.Name, true, DataSourceUpdateMode.OnPropertyChanged);
                    return control;
                }

                case APDFFilterSetting.ControlType.ROW_RELATIVE_POINT: {
                    ComboBox control = new ComboBox() { Margin = margin, Width = setting.Width, DropDownStyle = ComboBoxStyle.DropDownList };
                    control.Items.Add("Start");
                    control.Items.Add("End");
                    control.DataBindings.Add("SelectedIndex", filter, property.Name, true, DataSourceUpdateMode.OnPropertyChanged);
                    return control;
                }

                case APDFFilterSetting.ControlType.CONDITION: {
                    ComboBox control = new ComboBox() { Margin = margin, Width = setting.Width, DropDownStyle = ComboBoxStyle.DropDownList };
                    string[] conditions = ScrapeTable.ConditionStringList();
                    foreach (string c in conditions) {
                        control.Items.Add(c);
                    }
                    control.DataBindings.Add("SelectedIndex", filter, property.Name, true, DataSourceUpdateMode.OnPropertyChanged);
                    return control;
                }

                case APDFFilterSetting.ControlType.MESSAGE_BUTTON: {
                    Button control = new Button() { Margin = new Padding(margin.Left, margin.Top - 1, margin.Right, margin.Bottom), Text = setting.Caption, Width = setting.Width };
                    control.Font = new Font(control.Font, FontStyle.Bold);
                    control.Click += new EventHandler((sender, e) => MessageBox.Show((string)property.GetValue(filter), setting.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information));
                    return control;
                }

                default:
                    return null;
            }
        }

        //--------------------------------------------------------------------------------
        private void AddFilterControls() {
            // Suspend layout
            SuspendFilterControlsLayout();

            // Add
            for (int i = 0; i < mExtractor.FilterCount; ++i) {
                AddFilterControls(i);
            }

            // Resume layout
            ResumeFilterControlsLayout();
        }

        //--------------------------------------------------------------------------------
        private void RemoveFilterControls(int filterIndex, PDFExtractorFilter filter) {
            // Checks
            if (filterIndex < 0 || filterIndex >= FilterCount)
                throw new IndexOutOfRangeException();

            // Row index
            int rowIndex = 2 + (filterIndex * 2);

            // Remove controls
            int i = 0;
            while (i < tlpFilters.Controls.Count) {
                Control control = tlpFilters.Controls[i];
                if ((tlpFilters.GetRow(control) >= rowIndex) && (tlpFilters.GetRow(control) <= rowIndex + 1)) {
                    tlpFilters.Controls.RemoveAt(i);
                    control.Dispose();
                }
                else {
                    // Control on later row - shift up
                    if (tlpFilters.GetRow(control) > rowIndex + 1)
                        tlpFilters.SetRow(control, tlpFilters.GetRow(control) - 2);
                    ++i;
                }
            }

            // Remove row styles
            tlpFilters.RowStyles.RemoveAt(rowIndex + 1);
            tlpFilters.RowStyles.RemoveAt(rowIndex);
            
            // Filter
            filter.PropertyChanged -= mFilterControls[filterIndex].propertyChangedDelegate;

            // Remove
            mFilterControls.RemoveAt(filterIndex);

            // Headings
            UpdateFilterHeadings();

            // Layout
            tlpFilters.RowCount = tlpFilters.RowStyles.Count;
            UpdateFilterPanelDimensions();
        }

        //--------------------------------------------------------------------------------
        private void RemoveAllFilterControls() {
            // Suspend layout
            SuspendFilterControlsLayout();

            // Remove
            while (FilterCount > 0) {
                RemoveFilterControls(FilterCount - 1, mExtractor.Filter(FilterCount - 1));
            }

            // Resume layout
            ResumeFilterControlsLayout();
        }

        //--------------------------------------------------------------------------------
        private int FilterCount { get { return mFilterControls.Count; } }

        //--------------------------------------------------------------------------------
        private void UpdateFilterHeadings() {
            for (int i = 0; i < mFilterControls.Count; ++i) {
                mFilterControls[i].heading.Text = (i + 1) + ". " + mExtractor.Filter(i).TypeName;
            }
        }
        
        //--------------------------------------------------------------------------------
        private void UpdateFilterPanelDimensions() {
            float height = 0.0f;
            for (int i = 0; i < tlpFilters.RowStyles.Count; ++i) {
                height += tlpFilters.RowStyles[i].Height + FILTER_ROW_PADDING;
            }
            tlpFilters.Height = (int)height;
        }

        //--------------------------------------------------------------------------------
        private void ScrollToFilterPanelRow(int rowIndex) {
            /*MessageBox.Show("Y: " + pnlFilters.AutoScrollPosition.Y);
            float rowBottom = 0.0f;
            for (int i = 0; i <= rowIndex; ++i) {
                rowBottom += tlpFilters.RowStyles[i].Height + 1;
            }*/
            //pnlFilters.AutoScrollPosition = new Point(0, -20);

            //pnlFilters.AutoScrollPosition = new Point(pnlFilters.AutoScrollMinSize.Width - pnlFilters.ClientSize.Width,
            //                                          pnlFilters.AutoScrollMinSize.Height - pnlFilters.ClientSize.Height);
            //pnlFilters.PerformLayout();

            // Setting auto scroll position doesn't seem to do anything.
        }
        
        //--------------------------------------------------------------------------------
        private void SuspendFilterControlsLayout() {
            SuspendLayout();
            tlpFilters.SuspendLayout();
        }
        
        //--------------------------------------------------------------------------------
        private void ResumeFilterControlsLayout() {
            tlpFilters.ResumeLayout();
            ResumeLayout();
        }
        
        //--------------------------------------------------------------------------------
        // The panel doesn't re-draw properly during scrolling, hence we have to cause it
        // manually - the downside is performance, hence the distance rule to limit how
        // often it occurs.
        // For future reference, if I want to try to figure out a deeper fix:
        // https://stackoverflow.com/questions/24528533/smooth-drawing-or-painting-of-child-controls-in-panel-while-scrolling
        private void pnlFilters_Scroll(object sender, ScrollEventArgs e) {
            if (Math.Abs(e.NewValue - mPanelScroll) > 40) {
                mPanelScroll = e.NewValue;
                pnlFilters.Update();
            }
        }

        
        //================================================================================
        //********************************************************************************
        private class FilterControls {
            public Button addButton;
            public TableLayoutPanel panel;
            public DataGridView dataGrid;
            public Label heading;
            public int settingsHeight;
            public PropertyChangedEventHandler propertyChangedDelegate;

            public FilterControls(Button addButton, TableLayoutPanel panel, DataGridView dataGrid, Label heading, int settingsHeight, PropertyChangedEventHandler propertyChangedDelegate) {
                this.addButton = addButton;
                this.panel = panel;
                this.dataGrid = dataGrid;
                this.heading = heading;
                this.settingsHeight = settingsHeight;
                this.propertyChangedDelegate = propertyChangedDelegate;
            }
        }
    }

}

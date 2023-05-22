using PDFScrape.PDF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PDFScraper.Forms.Model {

    public partial class ExtractorRegionForm : Form {
        //================================================================================
        private const int                       INVALIDATE_PADDING = 2;


        //================================================================================
        private PDFRegion                       mRegion;
        private PDFRegionManipulator            mRegionManipulator;

        private PDFReader                       mPDFReader;

        private Point                           mMenuClickPoint = new Point();


        //================================================================================
        //--------------------------------------------------------------------------------
        public ExtractorRegionForm(PDFRegion region, PDFReader pdfReader) {
            // Initialise
            InitializeComponent();

            // Region
            mRegion = region;
            mRegionManipulator = new PDFRegionManipulator(mRegion);

            // Controls
            picPDF.ContextMenuStrip = mnuRightClick;

            // PDF reader
            mPDFReader = pdfReader;
            numPDFPage.Maximum = mPDFReader.PageCount;

            // Bindings
            numPDFPage.DataBindings.Add("Value", mRegion, "Page", false, DataSourceUpdateMode.OnPropertyChanged);
            chkWholePage.DataBindings.Add("Checked", mRegion, "WholePage", false, DataSourceUpdateMode.OnPropertyChanged);
        }
        

        // FORM ================================================================================
        //--------------------------------------------------------------------------------
        private void ExtractorRegionForm_Load(object sender, EventArgs e) {
            // PDF
            SetPDFPage((int)numPDFPage.Value);
        }
        
        //--------------------------------------------------------------------------------
        private void ExtractorRegionForm_FormClosing(object sender, FormClosingEventArgs e) {
            // Bindings
            numPDFPage.DataBindings.RemoveAt(0);
            chkWholePage.DataBindings.RemoveAt(0);
        }


        // PDF RENDERING ================================================================================
        //--------------------------------------------------------------------------------
        private void picPDF_Paint(object sender, PaintEventArgs e) {
            // Clipping
            Rectangle clipRectangle = e.ClipRectangle;
            clipRectangle.Inflate(INVALIDATE_PADDING, INVALIDATE_PADDING);

            // Draw
            mRegion.DrawSurrounding(e.Graphics, clipRectangle);
            mRegion.Draw(e.Graphics, clipRectangle);
        }
        

        // PDF PAGES ================================================================================
        //--------------------------------------------------------------------------------
        private void numPDFPage_ValueChanged(object sender, EventArgs e) {
            SetPDFPage((int)numPDFPage.Value);
        }

        //--------------------------------------------------------------------------------
        private void SetPDFPage(int page) {
            picPDF.Image = mPDFReader.PageImage(page);
            mRegionManipulator.SetPageSize(picPDF.Width, picPDF.Height);
            lblRedFiltered.Visible = mPDFReader.GreyscaleFilterApplied;
        }
        

        // REGION ================================================================================
        //--------------------------------------------------------------------------------
        private void chkWholePage_CheckedChanged(object sender, EventArgs e) {
            picPDF.Invalidate();
        }

        // REGION MANIPULATION ================================================================================
        //--------------------------------------------------------------------------------
        private void picPDF_MouseDown(object sender, MouseEventArgs e) {
            // Checks
            if (e.X < 0 || e.X >= picPDF.Size.Width || e.Y < 0 || e.Y >= picPDF.Size.Height)
                return;

            // Manipulator
            if (e.Button == MouseButtons.Left) {
                picPDF.Invalidate(mRegion.InflatedDimensions(INVALIDATE_PADDING, INVALIDATE_PADDING));
                mRegionManipulator.InjectMouseDown(e.X, e.Y);
                picPDF.Invalidate(mRegion.InflatedDimensions(INVALIDATE_PADDING, INVALIDATE_PADDING));
            }

            // Context menu
            if (e.Button == MouseButtons.Right) {
                mMenuClickPoint.X = e.X;
                mMenuClickPoint.Y = e.Y;
            }
        }
        
        //--------------------------------------------------------------------------------
        private void picPDF_MouseUp(object sender, MouseEventArgs e) {
            // Checks
            if (e.Button != MouseButtons.Left)
                return;

            // Manipulator
            picPDF.Invalidate(mRegion.InflatedDimensions(INVALIDATE_PADDING, INVALIDATE_PADDING));
            mRegionManipulator.InjectMouseUp(e.X, e.Y);
            picPDF.Invalidate(mRegion.InflatedDimensions(INVALIDATE_PADDING, INVALIDATE_PADDING));
        }
        
        //--------------------------------------------------------------------------------
        private void picPDF_MouseMove(object sender, MouseEventArgs e) {
            // Cursor
            picPDF.Cursor = mRegionManipulator.GetCursor(e.X, e.Y);
            
            // Manipulator
            picPDF.Invalidate(mRegion.InflatedDimensions(INVALIDATE_PADDING, INVALIDATE_PADDING));
            mRegionManipulator.InjectMouseMove(e.X, e.Y);
            picPDF.Invalidate(mRegion.InflatedDimensions(INVALIDATE_PADDING, INVALIDATE_PADDING));
        }


        // CONTROLS ================================================================================
        //--------------------------------------------------------------------------------
        private void btnClose_Click(object sender, EventArgs e) {
            Close();
        }
        
        //--------------------------------------------------------------------------------
        private void mnuRightClick_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            if (e.ClickedItem == mnuRightClick_AddColumn) {
                // Add column
                if (!mRegionManipulator.AddDivider(mMenuClickPoint.X, mMenuClickPoint.Y)) {
                    mnuRightClick.Hide();
                    MessageBox.Show("No room for a new divider - please expand the region.", "No Room For Divider");
                }
                picPDF.Invalidate(mRegion.InflatedDimensions(INVALIDATE_PADDING, INVALIDATE_PADDING));
            }
            else if (e.ClickedItem == mnuRightClick_RemoveColumn) {
                // Remove column
                mRegionManipulator.RemoveDivider(mMenuClickPoint.X, mMenuClickPoint.Y);
                picPDF.Invalidate(mRegion.InflatedDimensions(INVALIDATE_PADDING, INVALIDATE_PADDING));
            }
        }
    }

}

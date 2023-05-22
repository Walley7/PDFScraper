using PDFScrape;
using PDFScrape.PDF;
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

    public partial class ModelDock : DockContent {
        //================================================================================
        private static readonly Pen             MANDATORY_REGION_BORDER_PEN = new Pen(Color.FromArgb(255, 100, 100));
        private static readonly SolidBrush      MANDATORY_REGION_FILL_BRUSH = new SolidBrush(Color.FromArgb(127, 255, 100, 100));
        private static readonly Pen             OPTIONAL_REGION_BORDER_PEN = new Pen(Color.FromArgb(50, 175, 50));
        private static readonly SolidBrush      OPTIONAL_REGION_FILL_BRUSH = new SolidBrush(Color.FromArgb(127, 50, 175, 50));
        private static readonly Pen             SELECTED_MANDATORY_REGION_BORDER_PEN = new Pen(Color.FromArgb(255, 175, 175), 2.0f);
        private static readonly SolidBrush      SELECTED_MANDATORY_REGION_FILL_BRUSH = new SolidBrush(Color.FromArgb(127, 255, 175, 175));
        private static readonly Pen             SELECTED_OPTIONAL_REGION_BORDER_PEN = new Pen(Color.FromArgb(125, 250, 125), 2.0f);
        private static readonly SolidBrush      SELECTED_OPTIONAL_REGION_FILL_BRUSH = new SolidBrush(Color.FromArgb(127, 125, 250, 125));

        private const int                       INVALIDATE_PADDING = 2;

        private const int                       MINIMUM_REGION_WIDTH = 5;
        private const int                       MINIMUM_REGION_HEIGHT = 5;

        private const int                       RESIZE_MARGIN = 2;

        //--------------------------------------------------------------------------------
        public enum SelectionState {
            NONE,
            INITIAL_SIZING,
            RESIZING_TOP_LEFT,
            RESIZING_TOP,
            RESIZING_TOP_RIGHT,
            RESIZING_LEFT,
            RESIZING_RIGHT,
            RESIZING_BOTTOM_LEFT,
            RESIZING_BOTTOM,
            RESIZING_BOTTOM_RIGHT,
            MOVING
        }

        public enum SelectionArea {
            NONE,
            TOP_LEFT,
            TOP,
            TOP_RIGHT,
            LEFT,
            RIGHT,
            BOTTOM_LEFT,
            BOTTOM,
            BOTTOM_RIGHT,
            INSIDE
        }


        //================================================================================
        private RegionPropertiesDock            mRegionPropertiesDock;

        private PDFScrapeModel2                  mModel;

        private PDFReader2                       mPDFReader = null;

        private bool                            mAddingRegion = false;

        private PDFScrapeRegion2                 mSelectedRegion = null;
        private SelectionState                  mSelectionState = SelectionState.NONE;

        private Point                           mAnchor = new Point(0, 0);
        private Rectangle                       mOriginalDimensions = new Rectangle(0, 0, 0, 0);


        //================================================================================
        //--------------------------------------------------------------------------------
        public ModelDock(RegionPropertiesDock regionPropertiesDock, PDFScrapeModel2 model) {
            // Component
            InitializeComponent();

            // Region properties dock
            mRegionPropertiesDock = regionPropertiesDock;
            mRegionPropertiesDock.ModelDock = this;

            // Model
            mModel = model;
        }
        

        // REGION PROPERTIES DOCK ================================================================================
        //--------------------------------------------------------------------------------
        public RegionPropertiesDock RegionPropertiesDock { get { return mRegionPropertiesDock; } }


        // MODEL ================================================================================
        //--------------------------------------------------------------------------------
        public PDFScrapeModel2 Model { get { return mModel; } }


        // PDF READER ================================================================================
        //--------------------------------------------------------------------------------
        public PDFReader2 PDFReader { get { return mPDFReader; } }
        

        // CONTROLS ================================================================================
        //--------------------------------------------------------------------------------
        private void btnSelectPDF_Click(object sender, EventArgs e) {
            if (dlgOpenPDF.ShowDialog() == DialogResult.OK)
                LoadPDF(dlgOpenPDF.FileName);
        }
        
        //--------------------------------------------------------------------------------
        private void numPDFPage_ValueChanged(object sender, EventArgs e) {
            ShowPDFPage((int)numPDFPage.Value);
        }
        
        //--------------------------------------------------------------------------------
        private void btnAddRegion_Click(object sender, EventArgs e) {
            ToggleAddingRegion();
        }

        //--------------------------------------------------------------------------------
        private void picPDF_Paint(object sender, PaintEventArgs e) {
            DrawRegions(e.Graphics, e.ClipRectangle);
        }

        //--------------------------------------------------------------------------------
        private void picPDF_MouseDown(object sender, MouseEventArgs e) {
            // Checks
            if (mPDFReader == null)
                return;
            if (e.X < 0 || e.X >= picPDF.Size.Width || e.Y < 0 || e.Y >= picPDF.Size.Height)
                return;

            // Anchor
            mAnchor.X = e.X;
            mAnchor.Y = e.Y;

            // Adding
            if (mAddingRegion) {
                SelectRegion(AddRegion(1, e.X, e.Y, MINIMUM_REGION_WIDTH, MINIMUM_REGION_HEIGHT));
                SetSelectionState(SelectionState.INITIAL_SIZING);
                return;
            }

            // Existing selection
            if (SelectedRegion != null) {
                // Original dimensions
                mOriginalDimensions = mSelectedRegion.Dimensions; // Struct, hence a copy

                // Action
                switch (SelectedRegionArea(e.X, e.Y)) {
                    case SelectionArea.INSIDE:          SetSelectionState(SelectionState.MOVING); return;
                    case SelectionArea.TOP_LEFT:        SetSelectionState(SelectionState.RESIZING_TOP_LEFT); return;
                    case SelectionArea.TOP:             SetSelectionState(SelectionState.RESIZING_TOP); return;
                    case SelectionArea.TOP_RIGHT:       SetSelectionState(SelectionState.RESIZING_TOP_RIGHT); return;
                    case SelectionArea.LEFT:            SetSelectionState(SelectionState.RESIZING_LEFT); return;
                    case SelectionArea.RIGHT:           SetSelectionState(SelectionState.RESIZING_RIGHT); return;
                    case SelectionArea.BOTTOM_LEFT:     SetSelectionState(SelectionState.RESIZING_BOTTOM_LEFT); return;
                    case SelectionArea.BOTTOM:          SetSelectionState(SelectionState.RESIZING_BOTTOM); return;
                    case SelectionArea.BOTTOM_RIGHT:    SetSelectionState(SelectionState.RESIZING_BOTTOM_RIGHT); return;
                }
            }

            // New selection
            SelectRegionAtPoint(e.X, e.Y);
        }

        //--------------------------------------------------------------------------------
        private void picPDF_MouseUp(object sender, MouseEventArgs e) {
            SetSelectionState(SelectionState.NONE);
        }
        
        //--------------------------------------------------------------------------------
        private void picPDF_MouseMove(object sender, MouseEventArgs e) {
            // Cursor
            UpdateCursor(e.X, e.Y);

            // Selection
            if (SelectedRegion != null) {
                // Invalidate drawing area - before
                if (mSelectionState != SelectionState.NONE)
                    InvalidateRegionDrawArea(SelectedRegion);

                // States
                if (mSelectionState == SelectionState.INITIAL_SIZING) {
                    // Initial sizing
                    if (e.X > SelectedRegion.X)
                        SelectedRegion.Width = (e.X - SelectedRegion.X);
                    if (SelectedRegion.Width < MINIMUM_REGION_WIDTH)
                        SelectedRegion.Width = MINIMUM_REGION_WIDTH;

                    if (e.Y >= SelectedRegion.Y)
                        SelectedRegion.Height = (e.Y - SelectedRegion.Y);
                    if (SelectedRegion.Height < MINIMUM_REGION_HEIGHT)
                        SelectedRegion.Height = MINIMUM_REGION_HEIGHT;
                }
                else if (mSelectionState == SelectionState.MOVING) {
                    // Moving
                    SelectedRegion.X = Math.Min(Math.Max(mOriginalDimensions.X + (e.X - mAnchor.X), 0), picPDF.Width - 1 - SelectedRegion.Width);
                    SelectedRegion.Y = Math.Min(Math.Max(mOriginalDimensions.Y + (e.Y - mAnchor.Y), 0), picPDF.Height - 1 - SelectedRegion.Height);
                }
                else {
                    if ((mSelectionState == SelectionState.RESIZING_TOP_LEFT) || (mSelectionState == SelectionState.RESIZING_LEFT) || (mSelectionState == SelectionState.RESIZING_BOTTOM_LEFT)) {
                        // Resizing - leftward
                        int xLimit = Math.Min(picPDF.Width - 1, mOriginalDimensions.X + mOriginalDimensions.Width) - MINIMUM_REGION_WIDTH;
                        SelectedRegion.X = Math.Min(Math.Max(mOriginalDimensions.X + (e.X - mAnchor.X), 0), xLimit);
                        SelectedRegion.Width = (mOriginalDimensions.X + mOriginalDimensions.Width) - SelectedRegion.X;
                    }
                    if ((mSelectionState == SelectionState.RESIZING_TOP_LEFT) || (mSelectionState == SelectionState.RESIZING_TOP) || (mSelectionState == SelectionState.RESIZING_TOP_RIGHT)) {
                        // Resizing - upward
                        int yLimit = Math.Min(picPDF.Height - 1, mOriginalDimensions.Y + mOriginalDimensions.Height) - MINIMUM_REGION_HEIGHT;
                        SelectedRegion.Y = Math.Min(Math.Max(mOriginalDimensions.Y + (e.Y - mAnchor.Y), 0), yLimit);
                        SelectedRegion.Height = (mOriginalDimensions.Y + mOriginalDimensions.Height) - SelectedRegion.Y;
                    }
                    if ((mSelectionState == SelectionState.RESIZING_TOP_RIGHT) || (mSelectionState == SelectionState.RESIZING_RIGHT) || (mSelectionState == SelectionState.RESIZING_BOTTOM_RIGHT)) {
                        // Resizing - rightward
                        SelectedRegion.Width = Math.Min(Math.Max(mOriginalDimensions.Width + (e.X - mAnchor.X), MINIMUM_REGION_WIDTH), picPDF.Width - 1 - mOriginalDimensions.X);
                    }
                    if ((mSelectionState == SelectionState.RESIZING_BOTTOM_LEFT) || (mSelectionState == SelectionState.RESIZING_BOTTOM) || (mSelectionState == SelectionState.RESIZING_BOTTOM_RIGHT)) {
                        // Resizing - downward
                        SelectedRegion.Height = Math.Min(Math.Max(mOriginalDimensions.Height + (e.Y - mAnchor.Y), MINIMUM_REGION_HEIGHT), picPDF.Height - 1 - mOriginalDimensions.Y);
                    }
                }

                // Invalidate drawing area - after resize
                if (mSelectionState != SelectionState.NONE)
                    InvalidateRegionDrawArea(SelectedRegion);
            }
        }


        // PDF ================================================================================
        //--------------------------------------------------------------------------------
        private void LoadPDF(string filePath) {
            // PDF
            if (mPDFReader != null)
                mPDFReader.Dispose();
            mPDFReader = new PDFReader2(filePath, 72);

            // Page
            numPDFPage.Maximum = mPDFReader.PageImages.Length;
            numPDFPage.Value = 1;

            // Show
            ShowPDFPage(1);
        }

        //--------------------------------------------------------------------------------
        private void ShowPDFPage(int page) {
            if (mPDFReader != null)
                picPDF.Image = mPDFReader.PageImages[page - 1];
        }

        
        // REGIONS - DRAWING ================================================================================
        //--------------------------------------------------------------------------------
        public void DrawRegions(Graphics graphics, Rectangle clipRectangle) {
            // Clipping rectangle (fixes the edge of regions not redrawing when another region is dragged off them)
            Rectangle clippingRectangle = clipRectangle;
            clippingRectangle.Inflate(INVALIDATE_PADDING, INVALIDATE_PADDING);

            // Draw
            foreach (PDFScrapeRegion2 r in Model.Regions) {
                if ((r.Page == numPDFPage.Value) && clippingRectangle.IntersectsWith(r.Dimensions)) {
                    // Dimensions
                    Rectangle dimensions = (SelectedRegion == r) ? new Rectangle(r.Dimensions.X + 1, r.Dimensions.Y + 1, r.Dimensions.Width - 1, r.Dimensions.Height - 1) : r.Dimensions;

                    // Colouring
                    SolidBrush fillBrush;
                    Pen borderPen;
                    if (SelectedRegion == r) {
                        if (r.Mandatory) {
                            fillBrush = SELECTED_MANDATORY_REGION_FILL_BRUSH;
                            borderPen = SELECTED_MANDATORY_REGION_BORDER_PEN;
                        }
                        else {
                            fillBrush = SELECTED_OPTIONAL_REGION_FILL_BRUSH;
                            borderPen = SELECTED_OPTIONAL_REGION_BORDER_PEN;
                        }
                    }
                    else {
                        if (r.Mandatory) {
                            fillBrush = MANDATORY_REGION_FILL_BRUSH;
                            borderPen = MANDATORY_REGION_BORDER_PEN;
                        }
                        else {
                            fillBrush = OPTIONAL_REGION_FILL_BRUSH;
                            borderPen = OPTIONAL_REGION_BORDER_PEN;
                        }
                    }

                    // Draw
                    graphics.FillRectangle(fillBrush, dimensions);
                    graphics.DrawRectangle(borderPen, dimensions);
                }
            }
        }

        //--------------------------------------------------------------------------------
        public void InvalidateRegionDrawArea(PDFScrapeRegion2 region) {
            Rectangle dimensions = region.Dimensions;
            dimensions.Inflate(INVALIDATE_PADDING, INVALIDATE_PADDING);
            picPDF.Invalidate(dimensions);
        }


        // REGIONS ================================================================================
        //--------------------------------------------------------------------------------
        private void StartAddingRegion() {
            mAddingRegion = true;
            btnAddRegion.Font = new Font(btnAddRegion.Font, FontStyle.Bold);
        }

        //--------------------------------------------------------------------------------
        private void StopAddingRegion() {
            mAddingRegion = false;
            btnAddRegion.Font = new Font(btnAddRegion.Font, FontStyle.Regular);
        }
        
        //--------------------------------------------------------------------------------
        private void ToggleAddingRegion() {
            if (!mAddingRegion)
                StartAddingRegion();
            else
                StopAddingRegion();
        }

        //--------------------------------------------------------------------------------
        private PDFScrapeRegion2 AddRegion(int page, int x, int y, int width, int height) {
            StopAddingRegion();
            PDFScrapeRegion2 region = Model.AddRegion(page, x, y, width, height);
            InvalidateRegionDrawArea(region);
            return region;
        }

        
        // SELECTION ================================================================================
        //--------------------------------------------------------------------------------
        private void SelectRegion(PDFScrapeRegion2 region) {
            // Invalidate previous draw area
            if (mSelectedRegion != null)
                InvalidateRegionDrawArea(mSelectedRegion);

            // Select
            mSelectedRegion = region;
            mRegionPropertiesDock.SetRegion(region);

            // Invalidate new draw area
            if (mSelectedRegion != null)
                InvalidateRegionDrawArea(mSelectedRegion);
        }
        
        //--------------------------------------------------------------------------------
        private void SelectRegionAtPoint(int x, int y) {
            for (int i = Model.Regions.Count - 1; i >= 0; --i) {
                if ((x >= Model.Regions[i].X) && (x <= Model.Regions[i].X + Model.Regions[i].Width) &&
                    (y >= Model.Regions[i].Y) && (y <= Model.Regions[i].Y + Model.Regions[i].Height))
                {
                    SetSelectionState(SelectionState.NONE);
                    SelectRegion(Model.Regions[i]);
                    return;
                }
            }
        }

        //--------------------------------------------------------------------------------
        private PDFScrapeRegion2 SelectedRegion { get { return mSelectedRegion; } }
        
        //--------------------------------------------------------------------------------
        private SelectionArea SelectedRegionArea(int x, int y) {
            // Checks
            if (x < (SelectedRegion.X - RESIZE_MARGIN) || x > (SelectedRegion.X + SelectedRegion.Width + RESIZE_MARGIN) ||
                y < (SelectedRegion.Y - RESIZE_MARGIN) || y > (SelectedRegion.Y + SelectedRegion.Height + RESIZE_MARGIN))
            {
                return SelectionArea.NONE;
            }

            // Distances
            int fromLeft = (x - (SelectedRegion.X - RESIZE_MARGIN));
            int fromRight = ((SelectedRegion.X + SelectedRegion.Width + RESIZE_MARGIN) - x);
            int fromTop = (y - (SelectedRegion.Y - RESIZE_MARGIN));
            int fromBottom = ((SelectedRegion.Y + SelectedRegion.Height + RESIZE_MARGIN) - y);

            // Area
            if (fromLeft >= 0 && fromLeft <= 5 && fromTop >= 0 && fromTop <= 5)
                return SelectionArea.TOP_LEFT;
            else if (fromRight >= 0 && fromRight <= 5 && fromTop >= 0 && fromTop <= 5)
                return SelectionArea.TOP_RIGHT;
            else if (fromLeft >= 0 && fromLeft <= 5 && fromBottom >= 0 && fromBottom <= 5)
                return SelectionArea.BOTTOM_LEFT;
            else if (fromRight >= 0 && fromRight <= 5 && fromBottom >= 0 && fromBottom <= 5)
                return SelectionArea.BOTTOM_RIGHT;
            else if (fromLeft >= 0 && fromLeft <= 5)
                return SelectionArea.LEFT;
            else if (fromTop >= 0 && fromTop <= 5)
                return SelectionArea.TOP;
            else if (fromRight >= 0 && fromRight <= 5)
                return SelectionArea.RIGHT;
            else if (fromBottom >= 0 && fromBottom <= 5)
                return SelectionArea.BOTTOM;
            else
                return SelectionArea.INSIDE;
        }
        

        // SELECTION STATE ================================================================================
        //--------------------------------------------------------------------------------
        private void SetSelectionState(SelectionState state) {
            // Checks
            if (mSelectionState == state)
                return;

            // Disable

            // Set
            mSelectionState = state;

            // Enable
        }
        

        // CURSOR ================================================================================
        //--------------------------------------------------------------------------------
        private Cursor SelectedRegionAreaCursor(int x, int y) {
            // Area
            SelectionArea area = SelectedRegionArea(x, y);

            // Cursor
            switch (area) {
                case SelectionArea.TOP_LEFT:        return Cursors.SizeNWSE;
                case SelectionArea.TOP:             return Cursors.SizeNS;
                case SelectionArea.TOP_RIGHT:       return Cursors.SizeNESW;
                case SelectionArea.LEFT:            return Cursors.SizeWE;
                case SelectionArea.RIGHT:           return Cursors.SizeWE;
                case SelectionArea.BOTTOM_LEFT:     return Cursors.SizeNESW;
                case SelectionArea.BOTTOM:          return Cursors.SizeNS;
                case SelectionArea.BOTTOM_RIGHT:    return Cursors.SizeNWSE;
                case SelectionArea.INSIDE:          return Cursors.SizeAll;
                default:                            return Cursors.Default;
            }
        }

        //--------------------------------------------------------------------------------
        private void UpdateCursor(int x, int y) {
            // Selection
            if (SelectedRegion != null) {
                switch (mSelectionState) {
                    case SelectionState.NONE:                   picPDF.Cursor = SelectedRegionAreaCursor(x, y); break;
                    case SelectionState.MOVING:                 picPDF.Cursor = Cursors.SizeAll; break;
                    case SelectionState.RESIZING_TOP_LEFT:      picPDF.Cursor = Cursors.SizeNWSE; break;
                    case SelectionState.RESIZING_TOP:           picPDF.Cursor = Cursors.SizeNS; break;
                    case SelectionState.RESIZING_TOP_RIGHT:     picPDF.Cursor = Cursors.SizeNESW; break;
                    case SelectionState.RESIZING_LEFT:          picPDF.Cursor = Cursors.SizeWE; break;
                    case SelectionState.RESIZING_RIGHT:         picPDF.Cursor = Cursors.SizeWE; break;
                    case SelectionState.RESIZING_BOTTOM_LEFT:   picPDF.Cursor = Cursors.SizeNESW; break;
                    case SelectionState.RESIZING_BOTTOM:        picPDF.Cursor = Cursors.SizeNS; break;
                    case SelectionState.RESIZING_BOTTOM_RIGHT:  picPDF.Cursor = Cursors.SizeNWSE; break;
                    default:                                    picPDF.Cursor = Cursors.Default; break;
                }
            }
            else
                picPDF.Cursor = Cursors.Default;
        }
    }

}

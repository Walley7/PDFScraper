using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDFScrape.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.PDF {

    public class PDFRegion : Bindable {
        //================================================================================
        protected static readonly SolidBrush    SURROUNDING_BRUSH = new SolidBrush(Color.FromArgb(127, 0, 0, 0));
        protected static readonly SolidBrush    BACKGROUND_BRUSH = new SolidBrush(Color.FromArgb(15, 255, 255, 255));
        protected static readonly Pen           BORDER_PEN = new Pen(Color.FromArgb(127, 255, 63, 63), 2.0f);
        //protected static readonly Pen           BORDER_BACKGROUND_PEN = new Pen(Color.FromArgb(127, 255, 255, 255), 2.0f);
        //protected static readonly Pen           BORDER_FOREGROUND_PEN = new Pen(Color.FromArgb(127, 255, 63, 63), 1.0f);
        protected static readonly SolidBrush    DIVIDER_BRUSH = new SolidBrush(Color.FromArgb(127, 255, 63, 63));
        protected static readonly SolidBrush    POINTS_BRUSH = new SolidBrush(Color.FromArgb(255, 63, 63));
        protected const int                     POINTS_PADDING = 1;
        

        //================================================================================
        private int                             mPage;
        private bool                            mWholePage;

        private int                             mX;
        private int                             mY;
        private int                             mWidth;
        private int                             mHeight;

        private List<int>                       mDividers = new List<int>();
        
        //--------------------------------------------------------------------------------        
        public event EventDelegate              Changed = delegate { };


        //================================================================================
        //--------------------------------------------------------------------------------
        public PDFRegion(int page = 1, bool wholePage = false, int x = 0, int y = 0, int width = 0, int height = 0) {
            mPage = page;
            mWholePage = wholePage;
            mX = x;
            mY = y;
            mWidth = width;
            mHeight = height;

            //BORDER_FOREGROUND_PEN.DashPattern = new float[] { 1.0f, 1.0f };
        }

        //--------------------------------------------------------------------------------
        public PDFRegion(int page, int x, int y, int width, int height) : this(page, false, x, y, width, height) { }


        // RENDERING ================================================================================
        //--------------------------------------------------------------------------------
        public virtual void Draw(Graphics graphics, Rectangle clipRectangle) {
            // Checks
            if (WholePage || (Width <= 0) || (Height <= 0) || !clipRectangle.IntersectsWith(Dimensions))
                return;
            
            // Background
            Rectangle backgroundDimensions = Dimensions;
            backgroundDimensions.Inflate(-2, -2);
            graphics.FillRectangle(BACKGROUND_BRUSH, backgroundDimensions);

            // Border - background
            Rectangle borderDimensions = Dimensions;
            borderDimensions.Inflate(-1, -1);
            graphics.DrawRectangle(BORDER_PEN, borderDimensions);

            /*// Border - background
            Rectangle borderDimensions = Dimensions;
            borderDimensions.Inflate(-1, -1);
            graphics.DrawRectangle(BORDER_BACKGROUND_PEN, borderDimensions);

            // Border - foreground
            borderDimensions = Dimensions;
            borderDimensions.Width -= 1;
            borderDimensions.Height -= 1;
            graphics.DrawRectangle(BORDER_FOREGROUND_PEN, borderDimensions);
            borderDimensions.Inflate(-1, -1);
            graphics.DrawRectangle(BORDER_FOREGROUND_PEN, borderDimensions);*/

            // Points
            graphics.FillRectangle(POINTS_BRUSH, new Rectangle(Dimensions.Left - POINTS_PADDING, Dimensions.Top - POINTS_PADDING, 2 + (POINTS_PADDING * 2), 2 + (POINTS_PADDING * 2)));
            graphics.FillRectangle(POINTS_BRUSH, new Rectangle(Dimensions.Right - 2 - POINTS_PADDING, Dimensions.Top - POINTS_PADDING, 2 + (POINTS_PADDING * 2), 2 + (POINTS_PADDING * 2)));
            graphics.FillRectangle(POINTS_BRUSH, new Rectangle(Dimensions.Left - POINTS_PADDING, Dimensions.Bottom - 2 - POINTS_PADDING, 2 + (POINTS_PADDING * 2), 2 + (POINTS_PADDING * 2)));
            graphics.FillRectangle(POINTS_BRUSH, new Rectangle(Dimensions.Right - 2 - POINTS_PADDING, Dimensions.Bottom - 2 - POINTS_PADDING, 2 + (POINTS_PADDING * 2), 2 + (POINTS_PADDING * 2)));

            // Dividers
            Rectangle dividerDimensions = Dimensions;
            dividerDimensions.Width = 2;

            foreach (int d in mDividers) {
                // Line
                dividerDimensions.X = Dimensions.X + d - 1;
                graphics.FillRectangle(DIVIDER_BRUSH, dividerDimensions);

                // Points
                graphics.FillRectangle(POINTS_BRUSH, new Rectangle(dividerDimensions.X - POINTS_PADDING, Dimensions.Top - POINTS_PADDING, 2 + (POINTS_PADDING * 2), 2 + (POINTS_PADDING * 2)));
                graphics.FillRectangle(POINTS_BRUSH, new Rectangle(dividerDimensions.X - POINTS_PADDING, Dimensions.Bottom - 2 - POINTS_PADDING, 2 + (POINTS_PADDING * 2), 2 + (POINTS_PADDING * 2)));
            }
        }

        //--------------------------------------------------------------------------------
        public void DrawSurrounding(Graphics graphics, Rectangle clipRectangle) {
            // Checks
            if (mWholePage)
                return;

            // Surrounding - dimensions
            int backgroundTopY1 = clipRectangle.Top;
            int backgroundTopY2 = Math.Min(clipRectangle.Bottom, Dimensions.Top);
            int backgroundBottomY1 = Math.Min(clipRectangle.Bottom, Dimensions.Bottom);
            int backgroundBottomY2 = clipRectangle.Bottom;

            int backgroundLeftX1 = clipRectangle.Left;
            int backgroundLeftX2 = Math.Min(clipRectangle.Right, Dimensions.Left);
            int backgroundRightX1 = Math.Min(clipRectangle.Right, Dimensions.Right);
            int backgroundRightX2 = clipRectangle.Right;

            // Surrounding - draw
            graphics.FillRectangle(SURROUNDING_BRUSH, new Rectangle(backgroundLeftX1, backgroundTopY1, clipRectangle.Width, backgroundTopY2 - backgroundTopY1));
            graphics.FillRectangle(SURROUNDING_BRUSH, new Rectangle(backgroundLeftX1, backgroundBottomY1, clipRectangle.Width, backgroundBottomY2 - backgroundBottomY1));
            graphics.FillRectangle(SURROUNDING_BRUSH, new Rectangle(backgroundLeftX1, backgroundTopY2, backgroundLeftX2 - backgroundLeftX1, backgroundBottomY1 - backgroundTopY2));
            graphics.FillRectangle(SURROUNDING_BRUSH, new Rectangle(backgroundRightX1, backgroundTopY2, backgroundRightX2 - backgroundRightX1, backgroundBottomY1 - backgroundTopY2));
        }


        // PAGE ================================================================================
        //--------------------------------------------------------------------------------
        public int Page {
            set {
                SetProperty("Page", ref mPage, value);
                Changed(this);
            }
            get { return mPage; }
        }

        //--------------------------------------------------------------------------------
        public bool WholePage {
            set {
                SetProperty("WholePage", ref mWholePage, value);
                Changed(this);
            }
            get { return mWholePage; }
        }


        // DIMENSIONS ================================================================================
        //--------------------------------------------------------------------------------
        public int X {
            set {
                SetProperty("X", ref mX, value);
                Changed(this);
            }
            get { return mX; }
        }

        //--------------------------------------------------------------------------------
        public int Y {
            set {
                SetProperty("Y", ref mY, value);
                Changed(this);
            }
            get { return mY; }
        }

        //--------------------------------------------------------------------------------
        public int Width {
            set {
                SetProperty("Width", ref mWidth, value);
                Changed(this);
            }
            get { return mWidth; }
        }

        //--------------------------------------------------------------------------------
        public int Height {
            set {
                SetProperty("Height", ref mHeight, value);
                Changed(this);
            }
            get { return mHeight; }
        }

        //--------------------------------------------------------------------------------
        public int X2 { get { return X + Width; } }
        public int Y2 { get { return Y + Height; } }

        //--------------------------------------------------------------------------------
        public Rectangle Dimensions { get { return new Rectangle(mX, mY, mWidth, mHeight); } }

        //--------------------------------------------------------------------------------
        public Rectangle InflatedDimensions(int width, int height) {
            Rectangle dimensions = Dimensions;
            dimensions.Inflate(width, height);
            return dimensions;
        }
        
        //--------------------------------------------------------------------------------
        public iText.Kernel.Geom.Rectangle ScrapeDimensions(PDFReader reader) {
            return new iText.Kernel.Geom.Rectangle((float)((X * reader.XScale) + reader.XTranslate),
                                                   (float)(reader.CropBox.GetHeight() - ((Y + Height) * reader.YScale) + reader.YTranslate),
                                                   (float)(Width * reader.XScale), (float)(Height * reader.YScale));
        }


        // COLUMNS ================================================================================
        //--------------------------------------------------------------------------------
        public void AddDivider(int position) {
            mDividers.Add(Math.Max(1, Math.Min(Width - 1, position)));
            mDividers.Sort();
            Changed(this);
        }

        //--------------------------------------------------------------------------------
        public void AddDivider() {
            if (LastDivider > -1)
                AddDivider(LastDivider + ((Width - 1) - LastDivider) / 2); // Halfway between last divider and end
            else
                AddDivider(Width / 2);
        }

        //--------------------------------------------------------------------------------
        public void AddDivider(int position, int minimumColumnWidth) {
            // Add
            AddDivider(position);

            // Spacing
            ApplyMinimumColumnWidth(minimumColumnWidth);
        }
        
        //--------------------------------------------------------------------------------
        public void RemoveDivider(int index) {
            mDividers.RemoveAt(index);
            Changed(this);
        }
        
        //--------------------------------------------------------------------------------
        public void RemoveDivider() {
            if (DividerCount > 0)
                RemoveDivider(DividerCount - 1);
        }

        //--------------------------------------------------------------------------------
        public void SetDivider(int index, int position) {
            if (index < 0 || index >= DividerCount)
                throw new IndexOutOfRangeException();
            mDividers[index] = position;
            mDividers.Sort();
            Changed(this);
        }
        
        //--------------------------------------------------------------------------------
        public int Divider(int index) {
            if (index < 0 || index >= DividerCount)
                throw new IndexOutOfRangeException();
            return mDividers[index];
        }

        //--------------------------------------------------------------------------------
        public int FirstDivider { get { return (DividerCount >= 1 ? Divider(0) : -1); } }
        public int LastDivider { get { return (DividerCount >= 1 ? Divider(DividerCount - 1) : -1); } }

        //--------------------------------------------------------------------------------
        public int PreviousDivider(int index) {
            // Checks
            if (index < 0 || index >= DividerCount)
                throw new IndexOutOfRangeException();

            // Previous
            int previousIndex = index - 1;
            return ((previousIndex >= 0 && previousIndex < DividerCount) ? Divider(previousIndex) : -1);
        }
        
        //--------------------------------------------------------------------------------
        public int NextDivider(int index) {
            // Checks
            if (index < 0 || index >= DividerCount)
                throw new IndexOutOfRangeException();

            // Next
            int nextIndex = index + 1;
            return ((nextIndex >= 0 && nextIndex < DividerCount) ? Divider(nextIndex) : -1);
        }

        //--------------------------------------------------------------------------------
        public int DividerCount { get { return mDividers.Count; } }

        //--------------------------------------------------------------------------------
        public void ShiftDividers(int shiftAmount) {
            // Shift
            for (int i = 0; i < mDividers.Count; ++i) {
                mDividers[i] += shiftAmount;
            }

            // Change count
            Changed(this);
        }

        //--------------------------------------------------------------------------------
        public void ApplyMinimumColumnWidth(int minimumColumnWidth, bool leftThenRight = false) {
            if (leftThenRight) {
                ApplyMinimumColumnWidthFromLeft(minimumColumnWidth);
                ApplyMinimumColumnWidthFromRight(minimumColumnWidth);
            }
            else {
                ApplyMinimumColumnWidthFromRight(minimumColumnWidth);
                ApplyMinimumColumnWidthFromLeft(minimumColumnWidth);
            }
        }

        //--------------------------------------------------------------------------------
        private void ApplyMinimumColumnWidthFromLeft(int minimumColumnWidth) {
            // Left to right
            int previous = 0;
            for (int i = 0; i < mDividers.Count; ++i) {
                if (mDividers[i] < previous + minimumColumnWidth)
                    mDividers[i] = previous + minimumColumnWidth;
                if (mDividers[i] > Width - minimumColumnWidth)
                    mDividers[i] = Width - minimumColumnWidth;
                previous = mDividers[i];
            }

            // Change count
            Changed(this);
        }

        //--------------------------------------------------------------------------------
        private void ApplyMinimumColumnWidthFromRight(int minimumColumnWidth) {
            // Right to left
            int previous = Width;
            for (int i = mDividers.Count - 1; i >= 0; --i) {
                if (mDividers[i] > previous - minimumColumnWidth)
                    mDividers[i] = previous - minimumColumnWidth;
                if (mDividers[i] < minimumColumnWidth)
                    mDividers[i] = minimumColumnWidth;
                previous = mDividers[i];
            }

            // Change count
            Changed(this);
        }

        //--------------------------------------------------------------------------------
        public int MinimumTotalWidth(int minimumColumnWidth) { return (DividerCount + 1) * minimumColumnWidth; }
        public int ExcessColumnSpace(int minimumColumnWidth) { return Width - MinimumTotalWidth(minimumColumnWidth); }


        // AREAS ================================================================================
        //--------------------------------------------------------------------------------
        public Rectangle Area(int index) {
            // Checks
            if (index < 0 || index >= AreaCount)
                throw new IndexOutOfRangeException();

            // Area
            if (WholePage)
                return new Rectangle(0, 0, -1, -1);
            else if (DividerCount == 0)
                return Dimensions;
            else {
                // Left
                int left = X;
                if (index > 0)
                    left = X + Divider(index - 1);

                // Right
                int right = X + Width;
                if (index < DividerCount)
                    right = X + Divider(index);

                // Area
                return new Rectangle(left, Y, right - left, Height);
            }
        }
        
        //--------------------------------------------------------------------------------
        public int AreaCount { get { return DividerCount > 0 ? DividerCount + 1 : 1; } }
        
        //--------------------------------------------------------------------------------
        public iText.Kernel.Geom.Rectangle[] ScrapeAreas(PDFReader reader) {
            // Checks
            if (WholePage)
                return null;

            // Areas
            List<iText.Kernel.Geom.Rectangle> scrapeAreas = new List<iText.Kernel.Geom.Rectangle>();

            for (int i = 0; i < AreaCount; ++i) {
                Rectangle area = Area(i);
                scrapeAreas.Add(new iText.Kernel.Geom.Rectangle((float)(area.X * reader.XScale + reader.XTranslate),
                                                                (float)(reader.CropBox.GetHeight() - ((area.Y + area.Height) * reader.YScale + reader.YTranslate)),
                                                                (float)(area.Width * reader.XScale), (float)(area.Height * reader.YScale)));
            }

            return scrapeAreas.ToArray();
        }


        // TO STRING ================================================================================
        //--------------------------------------------------------------------------------
        public override string ToString() {
            return "Page " + mPage + ", " + (mWholePage ? "Whole Page" : "(" + mX + ", " + mY + ") - (" + mWidth + ", " + mHeight + ")");
        }


        // SAVING / LOADING ================================================================================
        //--------------------------------------------------------------------------------
        public void WriteJSON(JsonTextWriter writer) {
            // Start
            writer.WriteStartObject();

            // Region
            writer.WritePropertyName("page");
            writer.WriteValue(Page);
            writer.WritePropertyName("whole_page");
            writer.WriteValue(WholePage);
            
            writer.WritePropertyName("x");
            writer.WriteValue(X);
            writer.WritePropertyName("y");
            writer.WriteValue(Y);
            writer.WritePropertyName("width");
            writer.WriteValue(Width);
            writer.WritePropertyName("height");
            writer.WriteValue(Height);

            // Dividers
            writer.WritePropertyName("dividers");
            writer.WriteStartArray();
            foreach (int d in mDividers) {
                writer.WriteValue(d);
            }
            writer.WriteEndArray();

            // End
            writer.WriteEndObject();
        }

        //--------------------------------------------------------------------------------
        public void ReadJSON(JToken token) {
            // Reset
            mPage = 1;
            mWholePage = false;
            mX = 0;
            mY = 0;
            mWidth = 0;
            mHeight = 0;
            mDividers.Clear();

            // Region
            mPage = (int)token.SelectToken("page");
            mWholePage = (bool)token.SelectToken("whole_page");
            mX = (int)token.SelectToken("x");
            mY = (int)token.SelectToken("y");
            mWidth = (int)token.SelectToken("width");
            mHeight = (int)token.SelectToken("height");

            // Dividers
            JArray dividers = (JArray)token.SelectToken("dividers");
            if (dividers != null) {
                foreach(JToken d in dividers) {
                    mDividers.Add((int)d);
                }
            }
        }


        //================================================================================
        //********************************************************************************
        public delegate void EventDelegate(PDFRegion region);
    }

}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFScraper.Forms.Overlay {

    public class OverlayMacroCursorText {
        //================================================================================
        private Graphics                        mGraphics;

        private int                             mRenderPriority = 0;

        private long                            mTimeRemaining;
        private long                            mPreviousTime;
        private int                             mX;
        private int                             mY;

        private bool                            mCursorVisible = false;
        private bool                            mHideCursorWhenNotMostRecent = false;
        private int                             mMinimumCursorSize;
        private int                             mMaximumCursorSize;
        private double                          mCursorResizeRate; // Base amount to resize cursor by per update (in either direction) 
        private double                          mCursorResizeDecayFactor = 1.0;
        private double                          mCursorSize; // Current size of cursor
        private double                          mCursorResizeFactor; // Amount to resize cursor on next update

        private bool                            mTextVisible = false;
        private string                          mText = "";
        private Font                            mTextFont = new Font("Microsoft Sans Serif", 12);
        private int                             mTextMargin = 20;
        private SizeF                           mTextDimensions = new SizeF(0.0f, 0.0f);

        private double                          mColourChangeRate = 3.0 / 256.0;
        private Color                           mColour1 = Color.FromArgb(255, 127, 255, 127);
        private Color                           mColour2 = Color.FromArgb(255, 0, 127, 0);
        private bool                            mOneWayColourChange = false;

        private int                             mColourDimAmount = 20;
        private int                             mColourMaximumDim = 60;
        private bool                            mColourDimming = false;

        private double                          mColourRatio = 0.0;
        private Color                           mColour = Color.White;
        private Color                           mBorderColour = Color.White;


        //================================================================================
        //--------------------------------------------------------------------------------
        public OverlayMacroCursorText(Graphics graphics, int renderPriority, int x, int y, long duration) {
            mGraphics = graphics;
            mRenderPriority = renderPriority;
            mTimeRemaining = duration;
            mPreviousTime = DateTime.Now.Ticks;
            mX = x;
            mY = y;
        }

        
        // UPDATING ================================================================================
        //--------------------------------------------------------------------------------
        public bool Update(OverlayMacroForm form) {
            // Time
            long currentTime = DateTime.Now.Ticks;
            long timeElapsed = currentTime - mPreviousTime;
            mTimeRemaining -= timeElapsed;
            mPreviousTime = currentTime;

            // Cursor hiding
            if ((form.CursorTextReversePosition(this) > 0) && mCursorVisible && mHideCursorWhenNotMostRecent) {
                Console.Out.WriteLine(form.CursorTextReversePosition(this));
                mCursorVisible = false;
            }

            // Cursor size
            if (mCursorVisible) {
                // Size
                if (mCursorSize <= mMinimumCursorSize)
                    mCursorResizeFactor = mCursorResizeRate;
                else if (mCursorSize >= mMaximumCursorSize)
                    mCursorResizeFactor = -mCursorResizeRate;
                mCursorSize += mCursorResizeFactor;

                // Decay
                mCursorResizeRate *= mCursorResizeDecayFactor;
                mCursorResizeFactor *= mCursorResizeDecayFactor;
            }

            // Colour
            if (mCursorVisible || mTextVisible) {
                // Colour - rainbow
                /*mCursorHue += CURSOR_COLOUR_CHANGE_RATE;
                mCursorHue %= 241;
                mCursorColor = Color.FromArgb(255, Color.FromArgb(UGeneral.ColorHLSToRGB(mCursorHue, 120, 240)));
                mCursorBorderColor = Color.FromArgb(255, Color.FromArgb(UGeneral.ColorHLSToRGB(mCursorHue, 60, 240)));*/

                // Colour - update
                mColourRatio += mColourChangeRate;
                if (mOneWayColourChange)
                    mColourRatio = Math.Min(mColourRatio, 1.0);
                mColourRatio %= 2.0;

                // Colour - apply
                if (mColourRatio <= 1.0) {
                    mColour = Color.FromArgb((int)Math.Round(mColour1.A + mColourRatio * (mColour2.A - mColour1.A)),
                                             (int)Math.Round(mColour1.R + mColourRatio * (mColour2.R - mColour1.R)),
                                             (int)Math.Round(mColour1.G + mColourRatio * (mColour2.G - mColour1.G)),
                                             (int)Math.Round(mColour1.B + mColourRatio * (mColour2.B - mColour1.B)));
                }
                else {
                    mColour = Color.FromArgb((int)Math.Round(mColour2.A + (mColourRatio - 1.0) * (mColour1.A - mColour2.A)),
                                             (int)Math.Round(mColour2.R + (mColourRatio - 1.0) * (mColour1.R - mColour2.R)),
                                             (int)Math.Round(mColour2.G + (mColourRatio - 1.0) * (mColour1.G - mColour2.G)),
                                             (int)Math.Round(mColour2.B + (mColourRatio - 1.0) * (mColour1.B - mColour2.B)));
                }

                mBorderColour = Color.FromArgb(255, mColour.R / 3, mColour.G / 3, mColour.B / 3);

                // Dimming
                if (mColourDimming) {
                    int dimAmount = Math.Min(form.CursorTextReversePosition(this) * ColourDimAmount, ColourMaximumDim);
                    mColour = Color.FromArgb((byte)Math.Max((int)mColour.A, 0),
                                             (byte)Math.Max((int)mColour.R - dimAmount, 0),
                                             (byte)Math.Max((int)mColour.G - dimAmount, 0),
                                             (byte)Math.Max((int)mColour.B - dimAmount, 0));
                    mBorderColour = Color.FromArgb((byte)Math.Max((int)mBorderColour.A, 0),
                                                   (byte)Math.Max((int)mBorderColour.R - dimAmount, 0),
                                                   (byte)Math.Max((int)mBorderColour.G - dimAmount, 0),
                                                   (byte)Math.Max((int)mBorderColour.B - dimAmount, 0));
                }
            }

            // Invalidation
            if (mCursorVisible || mTextVisible) {
                int halfSize = mMaximumCursorSize / 2;
                form.Invalidate(new Rectangle(mX - halfSize - 1, mY - halfSize - 1 - (int)Math.Round(mTextDimensions.Height * 0.5f),
                                mMaximumCursorSize + 2 + mTextMargin + (int)Math.Round(mTextDimensions.Width),
                                mMaximumCursorSize + 2 + (int)Math.Round(mTextDimensions.Height)));
            }

            // Return
            return (mTimeRemaining > 0);
        }

        
        // RENDERING ================================================================================
        //--------------------------------------------------------------------------------
        public void Draw(Graphics graphics) {
            // Cursor
            if (mCursorVisible) {
                int cursorSize = Math.Min(Math.Max((int)Math.Round(mCursorSize), mMinimumCursorSize), mMaximumCursorSize);
                DrawCursor(graphics, mX, mY, mBorderColour, cursorSize, 5);
                DrawCursor(graphics, mX, mY, mColour, cursorSize - 2, 3);
            }

            // Text
            if (mTextVisible) {
                int textYOffset = (int)Math.Round(mTextDimensions.Height * 0.45);
                DrawText(graphics, mText, mX + mTextMargin - 1, mY - textYOffset, mBorderColour, mTextFont);
                DrawText(graphics, mText, mX + mTextMargin + 1, mY - textYOffset, mBorderColour, mTextFont);
                DrawText(graphics, mText, mX + mTextMargin, mY - textYOffset + 1, mBorderColour, mTextFont);
                DrawText(graphics, mText, mX + mTextMargin, mY - textYOffset - 1, mBorderColour, mTextFont);
                DrawText(graphics, mText, mX + mTextMargin, mY - textYOffset, mColour, mTextFont);
            }
        }

        //--------------------------------------------------------------------------------
        public static void DrawCursor(Graphics graphics, int x, int y, Color colour, int size, int arrowSize = 5) {
            // Measurements
            int halfSize = size / 2;

            // Outline
            //Pen outlinePen = new Pen(Color.FromArgb(127, 0, 0), 1.0f);
            //graphics.DrawRectangle(outlinePen, new Rectangle(x - halfSize, y - halfSize, size - 1, size - 1));

            // Arrows
            SolidBrush arrowsBrush = new SolidBrush(colour);

            // Left / right
            graphics.FillPolygon(arrowsBrush, new PointF[] { new PointF(x - halfSize, y - arrowSize),
                                                             new PointF(x - halfSize + arrowSize, y),
                                                             new PointF(x - halfSize, y + arrowSize) });
            graphics.FillPolygon(arrowsBrush, new PointF[] { new PointF(x + halfSize + 1, y - arrowSize),
                                                             new PointF(x + halfSize + 1 - arrowSize, y),
                                                             new PointF(x + halfSize + 1, y + arrowSize) });

            // Top / bottom
            graphics.FillPolygon(arrowsBrush, new PointF[] { new PointF(x - arrowSize + 1, y - halfSize),
                                                             new PointF(x, y - halfSize + arrowSize),
                                                             new PointF(x + arrowSize, y - halfSize) });
            graphics.FillPolygon(arrowsBrush, new PointF[] { new PointF(x - arrowSize, y + halfSize + 1),
                                                             new PointF(x, y + halfSize - arrowSize),
                                                             new PointF(x + arrowSize, y + halfSize + 1) });
        }

        //--------------------------------------------------------------------------------
        public static void DrawText(Graphics graphics, string text, int x, int y, Color colour, Font font) {
            // Outline
            /*Pen outlinePen = new Pen(mBorderColour);

            GraphicsPath path = new GraphicsPath();
            path.AddString(mOverlayText, FontFamily.GenericSansSerif, (int)FontStyle.Regular, e.Graphics.DpiY * 12 / 72, new Point(mCursorX + 40, mCursorY - 6), new StringFormat());
            path.AddString(mOverlayText, FontFamily.GenericSansSerif, (int)FontStyle.Regular, e.Graphics.DpiY * 12 / 72, new Point(mCursorX + 39, mCursorY - 6), new StringFormat());
            path.AddString(mOverlayText, FontFamily.GenericSansSerif, (int)FontStyle.Regular, e.Graphics.DpiY * 12 / 72, new Point(mCursorX + 41, mCursorY - 6), new StringFormat());
            path.AddString(mOverlayText, FontFamily.GenericSansSerif, (int)FontStyle.Regular, e.Graphics.DpiY * 12 / 72, new Point(mCursorX + 40, mCursorY - 5), new StringFormat());
            path.AddString(mOverlayText, FontFamily.GenericSansSerif, (int)FontStyle.Regular, e.Graphics.DpiY * 12 / 72, new Point(mCursorX + 40, mCursorY - 7), new StringFormat());
            e.Graphics.DrawPath(outlinePen, path);*/

            // Brushes
            SolidBrush brush = new SolidBrush(colour);
            graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;

            // Text
            graphics.DrawString(text, font, brush, new PointF(x, y));
        }


        // RENDER PRIORITY ================================================================================
        //--------------------------------------------------------------------------------
        public int RenderPriority { get { return mRenderPriority; } }


        // DURATION ================================================================================
        //--------------------------------------------------------------------------------
        public long TimeRemaining { get { return mTimeRemaining; } }


        // POSITION ================================================================================
        //--------------------------------------------------------------------------------
        public int X { set { mX = value; } get { return mX; } }
        public int Y { set { mY = value; } get { return mY; } }
        
        
        // CURSOR ================================================================================
        //--------------------------------------------------------------------------------
        public void ShowCursor(int minimumSize, int maximumSize, double resizeRate) {
            mMinimumCursorSize = minimumSize;
            mMaximumCursorSize = maximumSize;
            mCursorResizeRate = resizeRate;

            mCursorSize = mMinimumCursorSize;
            mCursorResizeFactor = mCursorResizeRate;
            mCursorVisible = true;
        }

        //--------------------------------------------------------------------------------
        public bool CursorVisible { set { mCursorVisible = value; } get { return mCursorVisible; } }
        public bool HideCursorWhenNotMostRecent { set { mHideCursorWhenNotMostRecent = value; } get { return mHideCursorWhenNotMostRecent; } }
        public int MinimumCursorSize { set { mMinimumCursorSize = value; } get { return mMinimumCursorSize; } }
        public int MaximumCursorSize { set { mMaximumCursorSize = value; } get { return mMaximumCursorSize; } }
        public double CursorResizeRate { set { mCursorResizeRate = value; } get { return mCursorResizeRate; } }
        public double CursorResizeDecayFactor { set { mCursorResizeDecayFactor = value; } get { return mCursorResizeDecayFactor; } }
        public double CursorSize { set { mCursorSize = value; } get { return mCursorSize; } }
        public double CursorResizeFactor { set { mCursorResizeFactor = value; } get { return mCursorResizeFactor; } }

        
        // TEXT ================================================================================
        //--------------------------------------------------------------------------------
        public void ShowText(string text, Font font = null, int margin = 20) {
            if (font != null)
                mTextFont = font;
            Text = text;
            mTextMargin = margin;
            mTextVisible = true;
        }

        //--------------------------------------------------------------------------------
        public bool TextVisible { set { mTextVisible = value; } get { return mTextVisible; } }

        //--------------------------------------------------------------------------------
        public string Text {
            set {
                // Text
                mText = value;

                // Dimensions
                mTextDimensions = mGraphics.MeasureString(mText, mTextFont);
            }
            get { return mText; }
        }

        //--------------------------------------------------------------------------------
        public Font TextFont { set { mTextFont = value; } get { return mTextFont; } }
        public int TextMargin { set { mTextMargin = value; } get { return mTextMargin; } }
        public SizeF TextDimensions { set { mTextDimensions = value; } get { return mTextDimensions; } }

        
        // COLOURS ================================================================================
        //--------------------------------------------------------------------------------
        public double ColourChangeRate { set { mColourChangeRate = value; } get { return mColourChangeRate; } }
        public Color Colour1 { set { mColour1 = value; } get { return mColour1; } }
        public Color Colour2 { set { mColour2 = value; } get { return mColour2; } }
        public bool OneWayColourChange { set { mOneWayColourChange = value; } get { return mOneWayColourChange; } }
        
        //--------------------------------------------------------------------------------
        public int ColourDimAmount { set { mColourDimAmount = value; } get { return mColourDimAmount; } }
        public int ColourMaximumDim { set { mColourMaximumDim = value; } get { return mColourMaximumDim; } }
        public bool ColourDimming { set { mColourDimming = value; } get { return mColourDimming; } }

        //--------------------------------------------------------------------------------
        public double ColourRatio { set { mColourRatio = value; } get { return mColourRatio; } }
        public Color Colour { set { mColour = value; } get { return mColour; } }
        public Color BorderColour { set { mBorderColour = value; } get { return mBorderColour; } }
    }

}

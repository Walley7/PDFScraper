using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Filter;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using PDFScrape.Data;
using PDFScrape.Exceptions;
using PDFScrape.IText;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFScrape.PDF {

    public class PDFReader {
        //================================================================================
        private string                          mFilePath;

        private PdfReader                       mReader;
        private PdfDocument                     mDocument;

        private int                             mDPI;

        private Image[]                         mPageImages = null;
        
        private Color?                          mGreyscaleFilterTone = null;
        private Color                           mGreyscaleFilterTolerance = Color.Black;
        private bool                            mGreyscaleFilterApplied = false;


        //================================================================================
        //--------------------------------------------------------------------------------
        public PDFReader(string filePath, int dpi, Color? greyscaleFilterTone, Color greyscaleFilterTolerance, bool readAllPageImages = false) {
            // File path
            mFilePath = filePath;

            // Reader / document
            mReader = new PdfReader(filePath);
            mDocument = new PdfDocument(mReader);
            
            // DPI
            mDPI = dpi;

            // Greyscale filter
            SetGreyscaleFilter(greyscaleFilterTone, greyscaleFilterTolerance);

            // Page images
            mPageImages = Enumerable.Repeat<Image>(null, mDocument.GetNumberOfPages()).ToArray();
            ReadPageImages(1, readAllPageImages ? int.MaxValue : 1);
        }

        //--------------------------------------------------------------------------------
        public PDFReader(string filePath, int dpi, bool readAllPageImages = false) : this(filePath, dpi, null, Color.Black, readAllPageImages) { }

        //--------------------------------------------------------------------------------
        public void Dispose() {
            // Page images
            DisposePageImages();
            mPageImages = null;

            // Reader / document
            mDocument.Close();
            mReader.Close();
        }


        // PATH ================================================================================
        //--------------------------------------------------------------------------------
        public string FilePath { get { return mFilePath; } }


        // DOCUMENT ================================================================================
        //--------------------------------------------------------------------------------
        public PdfDocument Document { get { return mDocument; } }
        public iText.Kernel.Geom.Rectangle PageBox { get { return mDocument.GetFirstPage().GetMediaBox(); } }
        public iText.Kernel.Geom.Rectangle CropBox { get { return mDocument.GetFirstPage().GetCropBox(); } }
        public int PageCount { get { return mDocument.GetNumberOfPages(); } }


        // DIMENSIONS ================================================================================
        //--------------------------------------------------------------------------------
        // XScale and YScale are used to scale from the dimensions of the image used for
        // regions we define, to the dimensions of the pdf's crop box.
        public double XScale { get { return (double)CropBox.GetWidth() / (double)PageImage(1).Width; } }
        public double YScale { get { return (double)CropBox.GetHeight() / (double)PageImage(1).Height; } }

        //--------------------------------------------------------------------------------
        // XTranslate and YTranslate translate from the top left margin of the media box
        // to the top left margin of the crop box.
        public double XTranslate { get { return ((double)PageBox.GetWidth() - (double)CropBox.GetWidth()) / 2.0; } }
        public double YTranslate { get { return ((double)PageBox.GetHeight() - (double)CropBox.GetHeight()) / 2.0;} }



        // DPI ================================================================================
        //--------------------------------------------------------------------------------
        public int DPI { get { return mDPI; } }


        // PAGE IMAGES ================================================================================
        //--------------------------------------------------------------------------------
        public void ReadPageImages(int startPage = 1, int endPage = int.MaxValue) {
            // Checks
            if (startPage > endPage)
                throw new ArgumentException();
            if (startPage < 1)
                throw new IndexOutOfRangeException();

            // Page count
            int pageCount = PageCount;
            if (mPageImages.Length != pageCount)
                throw new ConsistencyException("PDF page count changed since reader opened");

            // End page
            if (endPage > pageCount)
                endPage = pageCount;

            // Already loaded?
            bool imagesLoaded = true;
            for (int i = startPage; i <= endPage; ++i) {
                if (mPageImages[i - 1] == null) {
                    imagesLoaded = false;
                    break;
                }
            }

            if (imagesLoaded)
                return;

            // Variables
            GhostscriptVersionInfo ghostscriptVersionInfo;
            GhostscriptRasterizer rasteriser = null;

            // Read
            try {
                // Ghostscript
                string dllPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "gsdll32.dll");
                ghostscriptVersionInfo = new GhostscriptVersionInfo(dllPath);
                //ghostscriptVersionInfo = new GhostscriptVersionInfo("gsdll32.dll");
            
                // Rasteriser
                rasteriser = new GhostscriptRasterizer();
                rasteriser.Open(FilePath, ghostscriptVersionInfo, false);

                // Page count
                if (mPageImages.Length != rasteriser.PageCount)
                    throw new ConsistencyException("PDF page count changed since reader opened");

                // Images
                for (int i = startPage; i <= endPage; ++i) {
                    if (mPageImages[i - 1] == null) {
                        mPageImages[i - 1] = rasteriser.GetPage(DPI, DPI, i);
                        ApplyGreyscaleFilter(mPageImages[i - 1]);
                    }
                }
            }
            finally {
                // Close rasteriser
                if (rasteriser != null) {
                    rasteriser.Close();
                    rasteriser.Dispose();
                }
            }
        }
        
        //--------------------------------------------------------------------------------
        public void DisposePageImages() {
            // Images
            if (mPageImages != null) {
                for (int i = 0; i < mPageImages.Length; ++i) {
                    if (mPageImages[i] != null) {
                        mPageImages[i].Dispose();
                        mPageImages[i] = null;
                    }
                }
            }

            // Greyscale filter
            mGreyscaleFilterApplied = false;
        }

        //--------------------------------------------------------------------------------
        public Image PageImage(int page) {
            // Checks
            if (page < 1)
                throw new IndexOutOfRangeException();
            if (page > mPageImages.Length)
                throw new IndexOutOfRangeException();

            // Page image
            if (mPageImages[page - 1] == null)
                ReadPageImages(page, page);
            return mPageImages[page - 1];
        }


        // GREYSCALE FILTER ================================================================================
        //--------------------------------------------------------------------------------
        public void SetGreyscaleFilter(Color? tone, Color tolerance) {
            mGreyscaleFilterTone = tone;
            mGreyscaleFilterTolerance = tolerance;
            DisposePageImages();
        }
        
        //--------------------------------------------------------------------------------
        private void ApplyGreyscaleFilter(Image image) {
            // Checks
            if (mGreyscaleFilterTone == null)
                return;
            if (!(image is Bitmap))
                throw new TypeAccessException();

            // Greyscale filter
            Bitmap bitmap = (Bitmap)image;
            for (int x = 0; x < bitmap.Width; ++x) {
                for (int y = 0; y < bitmap.Height; ++y) {
                    Color colour = bitmap.GetPixel(x, y);
                    if ((Math.Abs(mGreyscaleFilterTone.Value.R - colour.R) <= mGreyscaleFilterTolerance.R) &&
                        (Math.Abs(mGreyscaleFilterTone.Value.G - colour.G) <= mGreyscaleFilterTolerance.G) &&
                        (Math.Abs(mGreyscaleFilterTone.Value.B - colour.B) <= mGreyscaleFilterTolerance.B))
                    {
                        int greyscale = (int)(colour.R * 0.3 + colour.G * 0.59 + colour.B * 0.11);
                        bitmap.SetPixel(x, y, Color.FromArgb(colour.A, greyscale, greyscale, greyscale));
                        mGreyscaleFilterApplied = true;
                    }
                }
            }
        }

        //--------------------------------------------------------------------------------
        public bool GreyscaleFilterApplied { get { return mGreyscaleFilterApplied; } }


        // SCRAPING ================================================================================
        //--------------------------------------------------------------------------------
        public string[] ScrapeText(PDFRegion region) {
            if (!region.WholePage) {
                // Strategy
                TextRegionEventFilter regionFilter = new TextRegionEventFilter(region.ScrapeDimensions(this));
                TableTextExtractionStrategy tableStrategy = new TableTextExtractionStrategy(region.ScrapeAreas(this));
                ITextExtractionStrategy strategy = new FilteredTextEventListener(tableStrategy, regionFilter);

                // Scrape
                return tableStrategy.SplitTextByColumns(PdfTextExtractor.GetTextFromPage(Document.GetPage(region.Page), strategy));
            }
            else {
                // Whole page
                ITextExtractionStrategy strategy = new FilteredTextEventListener(new LocationTextExtractionStrategy());
                return new string[] { PdfTextExtractor.GetTextFromPage(Document.GetPage(region.Page), strategy) };
            }

            // OLD METHOD 1
            /*//try {
                //if (region.WholePage) {
                //    // Whole page
                //    //ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy(); // Disabled as it doesn't get text in line by line order
                //    ITextExtractionStrategy strategy = new FilteredTextEventListener(new LocationTextExtractionStrategy());
                //    return new string[] { PdfTextExtractor.GetTextFromPage(Document.GetPage(region.Page), strategy) };
                //}
                //else {
                    // Variables
                    List<string> text = new List<string>();
                    ITextExtractionStrategy strategy;

                    // Scrape
                    for (int i = 0; i < region.AreaCount; ++i) {
                        Rectangle area = region.Area(i);

                        if (area.Width == -1)
                            strategy = new FilteredTextEventListener(new LocationTextExtractionStrategy()); // Whole page
                        else {
                            // Region
                            TextRegionEventFilter regionFilter = new TextRegionEventFilter(
                                new iText.Kernel.Geom.Rectangle((float)(area.X * XScale + XTranslate), (float)(CropBox.GetHeight() - ((area.Y + area.Height) * YScale + YTranslate)),
                                                                (float)(area.Width * XScale), (float)(area.Height * YScale))                            
                            );
                            strategy = new FilteredTextEventListener(new LocationTextExtractionStrategy(), regionFilter);
                        }

                        text.Add(PdfTextExtractor.GetTextFromPage(Document.GetPage(region.Page), strategy));
                    }

                    return text.ToArray();
                //}
            //}
            //catch (Exception) { return null; }*/
            
            // OLD METHOD 2
            /*// Strategy
            ITextExtractionStrategy strategy;
            if (region.WholePage)
                strategy = new SimpleTextExtractionStrategy();
            else {
                // Region
                //var pageRotatedBox = pdfDocument.GetFirstPage().GetPageSizeWithRotation(); - may be relevant at some point
                TextRegionEventFilter regionFilter = new TextRegionEventFilter(
                    new iText.Kernel.Geom.Rectangle((float)((region.X * XScale) + XTranslate), (float)(CropBox.GetHeight() - ((region.Y + region.Height) * YScale) + YTranslate),
                                                    (float)(region.Width * XScale), (float)(region.Height * YScale))
                );
                strategy = new FilteredTextEventListener(new LocationTextExtractionStrategy(), regionFilter);
            }

            // Scrape
            try { return PdfTextExtractor.GetTextFromPage(Document.GetPage(region.Page), strategy); }
            catch (Exception) {  }*/
        }

        //--------------------------------------------------------------------------------
        public ScrapeTable Scrape(PDFRegion region, string tableName, bool splitRowsByNewLines = true) {
            // Scrape
            string[] text = ScrapeText(region);

            // Table
            ScrapeTable table = new ScrapeTable(tableName, text.Length, new string[][] { text });

            // Split by new lines
            if (splitRowsByNewLines)
                table.SplitRows("\n");

            // Return
            return table;
        }

        //--------------------------------------------------------------------------------
        public ScrapeTable Scrape(PDFRegion region, bool splitRowsByNewLines = true) { return Scrape(region, null, splitRowsByNewLines); }
            
    }

}

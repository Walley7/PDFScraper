using iText.IO.Util;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using PDFScrape.Exceptions;
using PDFScrape.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.IText {

    public class TableTextExtractionStrategy : ITextExtractionStrategy {
        //================================================================================
        private static bool                             DEBUG_OUTPUT = false;


        //================================================================================
        private readonly ITextChunkLocationStrategy     mChunkStrategy;

        private Rectangle[]                             mColumns;
        private Tuple<int, int>[]                       mColumnTextPositions;

        private bool                                    mRightToLeftRunDirection = false;
        
        private bool                                    mUseActualText = false;
        
        private readonly IList<TextChunk>               mChunks = new List<TextChunk>();

        private TextRenderInfo                          mLastTextRenderInfo = null;


        //================================================================================
        //--------------------------------------------------------------------------------
        public TableTextExtractionStrategy(ITextChunkLocationStrategy chunkStrategy, params Rectangle[] columns) {
            // Chunk strategy
            mChunkStrategy = chunkStrategy;

            // Columns
            mColumns = columns;
            mColumnTextPositions = new Tuple<int, int>[mColumns.Length];
            for (int i = 0; i < mColumns.Length; ++i) {
                mColumnTextPositions[i] = new Tuple<int, int>(0, 0);
            }
        }

        //--------------------------------------------------------------------------------
        public TableTextExtractionStrategy(params Rectangle[] columns) : this(new TextChunkLocationStrategy(), columns) { }


        // EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        public virtual void EventOccurred(IEventData data, EventType type) {
            if (type.Equals(EventType.RENDER_TEXT)) {
                // Render info / segment
                TextRenderInfo renderInfo = (TextRenderInfo)data;
                LineSegment segment = renderInfo.GetBaseline();
                //Console.Out.WriteLine("EventOccurred['" + renderInfo.GetText() + "']");

                // Remove rise (from the baseline - done because the )
                if (renderInfo.GetRise() != 0) {
                    Matrix riseOffsetTransform = new Matrix(0, -renderInfo.GetRise());
                    segment = segment.TransformBy(riseOffsetTransform);
                }

                // Chunks
                if (!UsingActualText) {
                    TextChunk chunk = new TextChunk(renderInfo.GetText(), mChunkStrategy.CreateLocation(renderInfo, segment));
                    mChunks.Add(chunk);
                }
                else {
                    // Actual text
                    CanvasTag lastTagWithActualText = (mLastTextRenderInfo != null ? FindLastTagWithActualText(mLastTextRenderInfo.GetCanvasTagHierarchy()) : null);

                    if ((lastTagWithActualText != null) && (lastTagWithActualText == FindLastTagWithActualText(renderInfo.GetCanvasTagHierarchy()))) {
                        // Merge two text pieces - assume they're on the same line
                        TextChunk lastTextChunk = mChunks[mChunks.Count - 1];

                        Vector mergedStart = new Vector(Math.Min(lastTextChunk.GetLocation().GetStartLocation().Get(0), segment.GetStartPoint().Get(0)),
                                                        Math.Min(lastTextChunk.GetLocation().GetStartLocation().Get(1), segment.GetStartPoint().Get(1)),
                                                        Math.Min(lastTextChunk.GetLocation().GetStartLocation().Get(2), segment.GetStartPoint().Get(2)));
                        Vector mergedEnd = new Vector(Math.Max(lastTextChunk.GetLocation().GetEndLocation().Get(0), segment.GetEndPoint().Get(0)),
                                                      Math.Max(lastTextChunk.GetLocation().GetEndLocation().Get(1), segment.GetEndPoint().Get(1)),
                                                      Math.Max(lastTextChunk.GetLocation().GetEndLocation().Get(2), segment.GetEndPoint().Get(2)));
                        TextChunk merged = new TextChunk(lastTextChunk.GetText(), mChunkStrategy.CreateLocation(renderInfo, new LineSegment(mergedStart, mergedEnd)));

                        mChunks[mChunks.Count - 1] = merged;
                    }
                    else {
                        String actualText = renderInfo.GetActualText();
                        TextChunk chunk = new TextChunk(actualText != null ? actualText : renderInfo.GetText(), mChunkStrategy.CreateLocation(renderInfo, segment));
                        mChunks.Add(chunk);
                    }
                }

                // Last render info
                mLastTextRenderInfo = renderInfo;
            }
        }

        //--------------------------------------------------------------------------------
        public virtual ICollection<EventType> GetSupportedEvents() { return null; }


        // TEXT ================================================================================
        //--------------------------------------------------------------------------------
        public virtual String GetResultantText() {
            // Debug output
            if (DEBUG_OUTPUT)
                PrintDebugOutput();

            // Sort chunks
            IList<TextChunk> chunks = new List<TextChunk>(mChunks);
            SortChunks(chunks);

            // Builders / previous chunks
            StringBuilder[] builders = new StringBuilder[mColumns.Length];
            TextChunk[] previousChunks = new TextChunk[mColumns.Length];
            for (int i = 0; i < mColumns.Length; ++i) {
                builders[i] = new StringBuilder();
                previousChunks[i] = null;
            }

            // Previous chunk
            TextChunk previousChunk = null;

            // Extract text
            foreach (TextChunk c in chunks) {
                /*Console.Write("[");
                for (int i = 0; i < mColumns.Length; ++i) {
                    if (i != 0)
                        Console.Write(", ");
                    Console.Write("'" + builders[i].ToString() + "'");
                }
                Console.WriteLine("]");*/

                // Column
                int column = ChunkColumn(c);
                if (column == -1)
                    throw new InvalidStateException("Chunk not in column.");
                //Console.Write(column + " : ");

                // Lines / words
                if (previousChunk != null) {
                    // New lines
                    if (!c.GetLocation().SameLine(previousChunk.GetLocation())) {
                        //Console.Write("newline :");
                        for (int i = 0; i < mColumns.Length; ++i) {
                            builders[i].Append('\n');
                        }
                    }
                }
                
                if (previousChunks[column] != null) {
                    // Spaces
                    if (c.GetLocation().SameLine(previousChunks[column].GetLocation()) && ChunkAtWordBoundary(c, previousChunks[column]) &&
                        !StartsWithSpace(c.GetText()) && !EndsWithSpace(previousChunks[column].GetText()))
                    {
                        //Console.Write("space : ");
                        builders[column].Append(' ');
                    }
                }

                // Text
                //Console.WriteLine("'" + c.GetText() + "'");
                builders[column].Append(c.GetText());
                previousChunks[column] = c;
                previousChunk = c;
            }

            // Strings
            StringBuilder builder = new StringBuilder();
            int offset = 0;

            for (int i = 0; i < mColumns.Length; ++i) {
                builder.Append(builders[i].ToString());
                mColumnTextPositions[i] = new Tuple<int, int>(offset, builders[i].Length);
                offset = offset + builders[i].Length;
            }

            return builder.ToString();
        }
        
        //--------------------------------------------------------------------------------
        protected int ChunkColumn(TextChunk chunk) {
            // Variables
            float largestIntersectionAmount = 0.0f;
            int column = -1;

            // Find column
            for (int i = 0; i < mColumns.Length; ++i) {
                if (ChunkIntersects(chunk, mColumns[i])) {
                    float intersectionAmount = ChunkIntersectAmount(chunk, mColumns[i]);
                    if (intersectionAmount > largestIntersectionAmount) {
                        largestIntersectionAmount = intersectionAmount;
                        column = i;
                    }
                }
            }

            // Result
            return column;
        }
        
        //--------------------------------------------------------------------------------
        protected bool ChunkIntersects(TextChunk chunk, Rectangle rectangle) {
            float x1 = chunk.GetLocation().GetStartLocation().Get(Vector.I1);
            float y1 = chunk.GetLocation().GetStartLocation().Get(Vector.I2);
            float x2 = chunk.GetLocation().GetEndLocation().Get(Vector.I1);
            float y2 = chunk.GetLocation().GetEndLocation().Get(Vector.I2);
            return rectangle.IntersectsLine(x1, y1, x2, y2);
        }

        //--------------------------------------------------------------------------------
        protected float ChunkIntersectAmount(TextChunk chunk, Rectangle rectangle) {
            float x1 = chunk.GetLocation().GetStartLocation().Get(Vector.I1);
            float y1 = chunk.GetLocation().GetStartLocation().Get(Vector.I2);
            float x2 = chunk.GetLocation().GetEndLocation().Get(Vector.I1);
            float y2 = chunk.GetLocation().GetEndLocation().Get(Vector.I2);
            return rectangle.IntersectLineAmount(x1, y1, x2, y2);
        }


        // COLUMNS ================================================================================
        //--------------------------------------------------------------------------------
        public string ColumnText(string text, int columnIndex) {
            return text.Substring(mColumnTextPositions[columnIndex].Item1, mColumnTextPositions[columnIndex].Item2);
        }

        //--------------------------------------------------------------------------------
        public string[] SplitTextByColumns(string text) {
            string[] textByColumns = new string[ColumnCount];
            for (int i = 0; i < ColumnCount; ++i) {
                textByColumns[i] = ColumnText(text, i);
            }
            return textByColumns;
        }

        //--------------------------------------------------------------------------------
        public int ColumnCount { get { return mColumns.Length; } }
        

        // CHUNKS ================================================================================
        //--------------------------------------------------------------------------------
        private void SortChunks(IList<TextChunk> chunks) {
            // Variables
            IDictionary<TextChunk, TextChunkMarks> marks = new Dictionary<TextChunk, TextChunkMarks>();
            IList<TextChunk> chunksToSort = new List<TextChunk>();

            // Chunks to sort
            for (int i = 0; i < chunks.Count; ++i) {
                ITextChunkLocation location = chunks[i].GetLocation();

                if (location.GetStartLocation().Equals(location.GetEndLocation())) {
                    // Find base
                    bool foundBase = false;
                    for (int j = 0; j < chunks.Count; ++j) {
                        if (i != j) {
                            ITextChunkLocation baseLocation = chunks[j].GetLocation();
                            if (!baseLocation.GetStartLocation().Equals(baseLocation.GetEndLocation()) && TextChunkLocation.ContainsMark(baseLocation, location)) {
                                TextChunkMarks currentMarks = marks.Get(chunks[j]);
                                if (currentMarks == null) {
                                    currentMarks = new TextChunkMarks();
                                    marks.Put(chunks[j], currentMarks);
                                }

                                if (i < j)
                                    currentMarks.preceding.Add(chunks[i]);
                                else
                                    currentMarks.succeeding.Add(chunks[i]);

                                foundBase = true;
                                break;
                            }
                        }
                    }

                    // Not found
                    if (!foundBase)
                        chunksToSort.Add(chunks[i]);
                }
                else
                    chunksToSort.Add(chunks[i]);
            }

            // Sort
            JavaCollectionsUtil.Sort(chunksToSort, new TextChunkComparer(new TextChunkLocation.DefaultComparer(!mRightToLeftRunDirection)));

            // Re-add
            chunks.Clear();

            foreach (TextChunk c in chunksToSort) {
                TextChunkMarks currentMarks = marks.Get(c);
                if (currentMarks != null) {
                    if (!mRightToLeftRunDirection) {
                        for (int i = 0; i < currentMarks.preceding.Count; ++i) {
                            chunks.Add(currentMarks.preceding[i]);
                        }
                    }
                    else {
                        for (int i = currentMarks.succeeding.Count - 1; i >= 0; --i) {
                            chunks.Add(currentMarks.succeeding[i]);
                        }
                    }
                }

                chunks.Add(c);

                if (currentMarks != null) {
                    if (!mRightToLeftRunDirection) {
                        for (int i = 0; i < currentMarks.succeeding.Count; ++i) {
                            chunks.Add(currentMarks.succeeding[i]);
                        }
                    }
                    else {
                        for (int i = currentMarks.preceding.Count - 1; i >= 0; --i) {
                            chunks.Add(currentMarks.preceding[i]);
                        }
                    }
                }
            }
        }

        //--------------------------------------------------------------------------------
        protected internal virtual bool ChunkAtWordBoundary(TextChunk chunk, TextChunk previousChunk) {
            return chunk.GetLocation().IsAtWordBoundary(previousChunk.GetLocation());
        }


        // RIGHT TO LEFT RUN DIRECTION ================================================================================
        //--------------------------------------------------------------------------------
        // Sets if text flows from left to right or from right to left. Call this method
        // with true argument for extracting Arabic, Hebrew or other text with
        // right-to-left writing direction.
        public virtual TableTextExtractionStrategy SetRightToLeftRunDirection(bool rightToLeftRunDirection) {
            mRightToLeftRunDirection = rightToLeftRunDirection;
            return this;
        }


        // ACTUAL TEXT ================================================================================
        //--------------------------------------------------------------------------------
        // Changes the behavior of text extraction so that if the parameter is set to
        // true, marked content property will be used instead of raw decoded bytes.
        public virtual TableTextExtractionStrategy SetUseActualText(bool useActualText) {
            mUseActualText = useActualText;
            return this;
        }

        //--------------------------------------------------------------------------------
        public virtual bool UsingActualText { get { return mUseActualText; } }
        
        //--------------------------------------------------------------------------------
        private CanvasTag FindLastTagWithActualText(IList<CanvasTag> canvasTagHierarchy) {
            CanvasTag lastActualText = null;
            foreach (CanvasTag tag in canvasTagHierarchy) {
                if (tag.GetActualText() != null) {
                    lastActualText = tag;
                    break;
                }
            }
            return lastActualText;
        }


        // DEBUGGING ================================================================================
        //--------------------------------------------------------------------------------
        private void PrintDebugOutput() {
            foreach (TextChunk chunk in mChunks) {
                System.Console.Out.WriteLine("Text (@" + chunk.GetLocation().GetStartLocation() + " -> " + chunk.GetLocation().GetEndLocation() + "): " + chunk.GetText());
                System.Console.Out.WriteLine("orientationMagnitude: " + chunk.GetLocation().OrientationMagnitude());
                System.Console.Out.WriteLine("distPerpendicular: " + chunk.GetLocation().DistPerpendicular());
                System.Console.Out.WriteLine("distParallel: " + chunk.GetLocation().DistParallelStart());
                System.Console.Out.WriteLine();
            }
        }
        

        // STRINGS ================================================================================
        //--------------------------------------------------------------------------------
        private bool StartsWithSpace(String str) {
            return (str.Length != 0 && str[0] == ' ');
        }
        
        //--------------------------------------------------------------------------------
        private bool EndsWithSpace(String str) {
            return (str.Length != 0 && str[str.Length - 1] == ' ');
        }


        //================================================================================
        //********************************************************************************
        public interface ITextChunkLocationStrategy {
            ITextChunkLocation CreateLocation(TextRenderInfo renderInfo, LineSegment baseline);
        }

        //********************************************************************************
        private sealed class TextChunkLocationStrategy : ITextChunkLocationStrategy {
            public TextChunkLocationStrategy() { }

            public ITextChunkLocation CreateLocation(TextRenderInfo renderInfo, LineSegment baseline) {
                return new TextChunkLocation(baseline.GetStartPoint(), baseline.GetEndPoint(), renderInfo.GetSingleSpaceWidth());
            }
        }
        
        //********************************************************************************
        private class TextChunkMarks {
            internal IList<TextChunk> preceding = new List<TextChunk>();
            internal IList<TextChunk> succeeding = new List<TextChunk>();
        }
    }
    
}

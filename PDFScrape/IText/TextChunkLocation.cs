using iText.IO.Util;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.IText {

    public class TextChunkLocation : ITextChunkLocation {
        //================================================================================
        private const float                     DIACRITICAL_MARKS_ALLOWED_VERTICAL_DEVIATION = 2;


        //================================================================================
        private readonly Vector                 mStartLocation; // The starting location of the chunk
        private readonly Vector                 mEndLocation; // The ending location of the chunk
        
        private readonly Vector                 mOrientationVector; // Unit vector in the orientation of the chunk
        private readonly int                    mOrientationMagnitude; // The orientation as a scalar for quick sorting

        private readonly int                    mDistPerpendicular; // Perpendicular distance to the orientation unit vector (i.e. the Y position in an unrotated coordinate system)
                                                                    // We round to the nearest integer to handle the fuzziness of comparing floats
        private readonly float                  mDistParallelStart; // Distance of the start of the chunk parallel to the orientation unit vector (i.e. the X position in an unrotated coordinate system)
        private readonly float                  mDistParallelEnd; // Distance of the end of the chunk parallel to the orientation unit vector (i.e. the X position in an unrotated coordinate system)

        private readonly float                  mCharSpaceWidth; // The width of a single space character in the font of the chunk


        //================================================================================
        //--------------------------------------------------------------------------------
        public TextChunkLocation(Vector startLocation, Vector endLocation, float charSpaceWidth) {
            // Location / char space width
            mStartLocation = startLocation;
            mEndLocation = endLocation;
            mCharSpaceWidth = charSpaceWidth;

            // Orientation
            Vector oVector = endLocation.Subtract(startLocation);
            if (oVector.Length() == 0)
                oVector = new Vector(1, 0, 0);
            mOrientationVector = oVector.Normalize();
            mOrientationMagnitude = (int)(Math.Atan2(mOrientationVector.Get(Vector.I2), mOrientationVector.Get(Vector.I1)) * 1000);

            // Distances
            // See http://mathworld.wolfram.com/Point-LineDistance2-Dimensional.html
            // The two vectors we are crossing are in the same plane, so the result will be purely
            // in the z-axis (out of plane) direction, so we just take the I3 component of the result
            Vector origin = new Vector(0, 0, 1);
            mDistPerpendicular = (int)(startLocation.Subtract(origin)).Cross(mOrientationVector).Get(Vector.I3);
            mDistParallelStart = mOrientationVector.Dot(startLocation);
            mDistParallelEnd = mOrientationVector.Dot(endLocation);
        }

        
        // FIELDS ================================================================================
        //--------------------------------------------------------------------------------
        public virtual int OrientationMagnitude() { return mOrientationMagnitude; }
        public virtual int DistPerpendicular() { return mDistPerpendicular; }
        public virtual float DistParallelStart() { return mDistParallelStart; }
        public virtual float DistParallelEnd() { return mDistParallelEnd; }
        public virtual Vector GetStartLocation() { return mStartLocation; }
        public virtual Vector GetEndLocation() { return mEndLocation; }
        public virtual float GetCharSpaceWidth() { return mCharSpaceWidth; }
        
        
        // LOCATION ================================================================================
        //--------------------------------------------------------------------------------
        public virtual bool SameLine(ITextChunkLocation other) {
            // Magnitude
            if (OrientationMagnitude() != other.OrientationMagnitude())
                return false;

            // Perpendicular difference
            float distPerpendicularDiff = DistPerpendicular() - other.DistPerpendicular();
            if (distPerpendicularDiff == 0)
                return true;
            
            // Segments
            LineSegment segment = new LineSegment(mStartLocation, mEndLocation);
            LineSegment otherSegment = new LineSegment(other.GetStartLocation(), other.GetEndLocation());

            // Same line (commented out code from iText had weird zero length checks, which made this pretty much always false)
            return (Math.Abs(distPerpendicularDiff) <= DIACRITICAL_MARKS_ALLOWED_VERTICAL_DEVIATION);
            //return ((Math.Abs(distPerpendicularDiff) <= DIACRITICAL_MARKS_ALLOWED_VERTICAL_DEVIATION) &&
            //        ((segment.GetLength() == 0) || (otherSegment.GetLength() == 0)));
        }
        
        //--------------------------------------------------------------------------------
        // Computes the distance between the end of 'other' and the beginning of this chunk
        // in the direction of this chunk's orientation vector.  Note that it's a bad idea
        // to call this for chunks that aren't on the same line and orientation, but we don't
        // explicitly check for that condition for performance reasons.
        public virtual float DistanceFromEndOf(ITextChunkLocation other) {  return DistParallelStart() - other.DistParallelEnd(); }
        
        //--------------------------------------------------------------------------------
        public virtual bool IsAtWordBoundary(ITextChunkLocation previous) {
            // Here we handle a very specific case which in PDF may look like:
            // -.232 Tc [( P)-226.2(r)-231.8(e)-230.8(f)-238(a)-238.9(c)-228.9(e)]TJ
            // The font's charSpace width is 0.232 and it's compensated with charSpacing of 0.232.
            // And a resultant TextChunk.charSpaceWidth comes to TextChunk constructor as 0.
            // In this case every chunk is considered as a word boundary and space is added.
            // We should consider charSpaceWidth equal (or close) to zero as a no-space.
            if (GetCharSpaceWidth() < 0.1f)
                return false;

            // In case a text chunk is of zero length, this probably means this is a mark character,
            // and we do not actually want to insert a space in such case
            if (mStartLocation.Equals(mEndLocation) || previous.GetEndLocation().Equals(previous.GetStartLocation()))
                return false;

            float dist = DistanceFromEndOf(previous);
            return ((dist < -GetCharSpaceWidth()) || (dist > GetCharSpaceWidth() / 2.0f));
        }
        
        //--------------------------------------------------------------------------------
        public static bool ContainsMark(ITextChunkLocation baseLocation, ITextChunkLocation markLocation) {
            return ((baseLocation.GetStartLocation().Get(Vector.I1) <= markLocation.GetStartLocation().Get(Vector.I1)) &&
                    (baseLocation.GetEndLocation().Get(Vector.I1) >= markLocation.GetEndLocation().Get(Vector.I1)) &&
                    (Math.Abs(baseLocation.DistPerpendicular() - markLocation.DistPerpendicular()) <= DIACRITICAL_MARKS_ALLOWED_VERTICAL_DEVIATION));
        }


        //================================================================================
        //********************************************************************************
        public class DefaultComparer : IComparer<ITextChunkLocation> {
            public bool leftToRight = true;
            
            public DefaultComparer(bool leftToRight) {
                this.leftToRight = leftToRight;
            }

            public DefaultComparer() : this(true) { }

            public virtual int Compare(ITextChunkLocation first, ITextChunkLocation second) {
                // Checks
                if (first == second)
                    return 0;

                // Magnitude
                int difference = JavaUtil.IntegerCompare(first.OrientationMagnitude(), second.OrientationMagnitude());
                if (difference != 0)
                    return difference;

                // Distance - perpendicular
                int differencePerpendicularDistance = first.DistPerpendicular() - second.DistPerpendicular();
                if (differencePerpendicularDistance != 0)
                    return differencePerpendicularDistance;

                // Distance - parallel
                return (this.leftToRight ? JavaUtil.FloatCompare(first.DistParallelStart(), second.DistParallelStart()) : -JavaUtil.FloatCompare(first.DistParallelEnd(), second.DistParallelEnd()));
            }
        }
    }

}

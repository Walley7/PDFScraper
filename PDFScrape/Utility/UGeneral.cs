using iText.Kernel.Geom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Utility {

    public static class UGeneral {
        //================================================================================
        enum CSOutCode {
            INSIDE = 0,
            LEFT = 1,
            RIGHT = 2,
            BOTTOM = 4,
            TOP = 8
        }


        // VARIABLES ================================================================================
        //--------------------------------------------------------------------------------
        public static void Swap<T>(ref T first, ref T second) {
            T temporary;
            temporary = first;
            first = second;
            second = temporary;
        }


        // COMPARISON ================================================================================
        //--------------------------------------------------------------------------------
        public static bool Equal(object first, object second) {
            if ((first == null) && (second == null))
                return true;
            else if (((first == null) && (second != null)) || ((first != null) && (second == null)))
                return false;
            else
                return first.Equals(second);
        }


        // LINES ================================================================================
        //--------------------------------------------------------------------------------
        public static double LineLength(double x1, double y1, double x2, double y2) {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        //--------------------------------------------------------------------------------
        public static Tuple<Point, Point> ClipLineSegment(Rectangle rectangle, Point point1, Point point2) {
            // End point out codes
            CSOutCode outCodePoint1 = ComputeCSOutCode(rectangle, point1);
            CSOutCode outCodePoint2 = ComputeCSOutCode(rectangle, point2);
            bool withinRectangle = false;

            // Clip
            while (true) {
                // Case 1 - both end points within the rectangle
                if ((outCodePoint1 | outCodePoint2) == CSOutCode.INSIDE) {
                    withinRectangle = true;
                    break;
                }

                // Case 2 - both end points in same excluded region
                if ((outCodePoint1 & outCodePoint2) != 0)
                    break;

                // Case 3 - end points in different regions, check for intersection
                CSOutCode outCode = (outCodePoint1 != CSOutCode.INSIDE ? outCodePoint1 : outCodePoint2);
                Point point = CalculateCSIntersection(rectangle, point1, point2, outCode);

                if (outCode == outCodePoint1) {
                    point1 = point;
                    outCodePoint1 = ComputeCSOutCode(rectangle, point1);
                }
                else {
                    point2 = point;
                    outCodePoint2 = ComputeCSOutCode(rectangle, point2);
                }
            }

            // Clip
            if (withinRectangle)
                return new Tuple<Point, Point>(point1, point2);
            else
                return null;
        }
        
        //--------------------------------------------------------------------------------
        private static Point CalculateCSIntersection(Rectangle rectangle, Point point1, Point point2, CSOutCode clipToOutCode) {
            // Rectangle
            double left = rectangle.GetX();
            double right = rectangle.GetX() + rectangle.GetWidth();
            double top = rectangle.GetY();
            double bottom = rectangle.GetY() + rectangle.GetHeight();

            // Deltas / slopes
            double dX = point2.x - point1.x;
            double dY = point2.y - point1.y;

            double slopeY = dX / dY;
            double slopeX = dY / dX;

            // Top
            if (clipToOutCode.HasFlag(CSOutCode.TOP))
                return new Point(point1.x + slopeY * (top - point1.y), top);

            // Bottom
            if (clipToOutCode.HasFlag(CSOutCode.BOTTOM))
                return new Point(point1.x + slopeY * (bottom - point1.y), bottom);

            // Right
            if (clipToOutCode.HasFlag(CSOutCode.RIGHT))
                return new Point(right, point1.y + slopeX * (right - point1.x));

            // Left
            if (clipToOutCode.HasFlag(CSOutCode.LEFT))
                return new Point(left, point1.y + slopeX * (left - point1.x));

            // Out of range
            throw new ArgumentOutOfRangeException("ClipTo = " + clipToOutCode);
        }
        
        //--------------------------------------------------------------------------------
        private static CSOutCode ComputeCSOutCode(Rectangle rectangle, Point point) {
            // Rectangle
            double left = rectangle.GetX();
            double right = rectangle.GetX() + rectangle.GetWidth();
            double top = rectangle.GetY();
            double bottom = rectangle.GetY() + rectangle.GetHeight();

            // Code
            CSOutCode code = CSOutCode.INSIDE;
            if (point.x < left)
                code |= CSOutCode.LEFT; 
            if (point.x > right)
                code |= CSOutCode.RIGHT; 
            if (point.y < top)
                code |= CSOutCode.TOP; 
            if (point.y > bottom)
                code |= CSOutCode.BOTTOM; 
            return code;
        }


        // COLOURS ================================================================================
        //--------------------------------------------------------------------------------
        [DllImport("shlwapi.dll")] public static extern int ColorHLSToRGB(int H, int L, int S);
    }

}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PDFScrape.PDF {

    public class PDFRegionManipulator {
        //================================================================================     
        public const int                        AREA_NONE = 0;
        public const int                        AREA_TOP_LEFT = 1;
        public const int                        AREA_TOP = 2;
        public const int                        AREA_TOP_RIGHT = 3;
        public const int                        AREA_LEFT = 4;
        public const int                        AREA_RIGHT = 5;
        public const int                        AREA_BOTTOM_LEFT = 6;
        public const int                        AREA_BOTTOM = 7;
        public const int                        AREA_BOTTOM_RIGHT = 8;
        public const int                        AREA_INSIDE = 9;
        public const int                        AREA_DIVIDER = 10;

        public const int                        STATE_NONE = 0;
        public const int                        STATE_REPOSITION = 1;
        public const int                        STATE_MOVING = 2;
        public const int                        STATE_SIZING_TOP_LEFT = 3;
        public const int                        STATE_SIZING_TOP = 4;
        public const int                        STATE_SIZING_TOP_RIGHT = 5;
        public const int                        STATE_SIZING_LEFT = 6;
        public const int                        STATE_SIZING_RIGHT = 7;
        public const int                        STATE_SIZING_BOTTOM_LEFT = 8;
        public const int                        STATE_SIZING_BOTTOM = 9;
        public const int                        STATE_SIZING_BOTTOM_RIGHT = 10;
        public const int                        STATE_MOVING_DIVIDER = 11;

        private const int                       RESIZE_MARGIN = 2;

        private const int                       MINIMUM_WIDTH = 10;
        private const int                       MINIMUM_HEIGHT = 10;
        private const int                       MINIMUM_COLUMN_WIDTH = 10;


        //================================================================================
        private PDFRegion                       mRegion;

        private int                             mState = STATE_NONE;

        private Size                            mPageSize = new Size();

        private Point                           mAnchor = new Point(0, 0);
        private Rectangle                       mOriginalDimensions = new Rectangle(0, 0, 0, 0);
        private int                             mDivider;
        private int                             mOriginalDividerPosition;


        //================================================================================
        //--------------------------------------------------------------------------------
        public PDFRegionManipulator(PDFRegion region) {
            mRegion = region;
        }
        

        // INPUT ================================================================================
        //--------------------------------------------------------------------------------
        public virtual void InjectMouseDown(int x, int y) {
            // Checks
            if (mRegion.WholePage)
                return;

            // Anchor
            mAnchor.X = x;
            mAnchor.Y = y;

            // Original dimensions
            mOriginalDimensions = mRegion.Dimensions; // Struct, hence a copy

            // Area
            Tuple<int, int> area = AreaAt(x, y);

            // State
            switch (area.Item1) {
                case AREA_NONE:         SetState(STATE_REPOSITION); break;
                case AREA_INSIDE:       SetState(STATE_MOVING); break;
                case AREA_TOP_LEFT:     SetState(STATE_SIZING_TOP_LEFT); break;
                case AREA_TOP:          SetState(STATE_SIZING_TOP); break;
                case AREA_TOP_RIGHT:    SetState(STATE_SIZING_TOP_RIGHT); break;
                case AREA_LEFT:         SetState(STATE_SIZING_LEFT); break;
                case AREA_RIGHT:        SetState(STATE_SIZING_RIGHT); break;
                case AREA_BOTTOM_LEFT:  SetState(STATE_SIZING_BOTTOM_LEFT); break;
                case AREA_BOTTOM:       SetState(STATE_SIZING_BOTTOM); break;
                case AREA_BOTTOM_RIGHT: SetState(STATE_SIZING_BOTTOM_RIGHT); break;
                case AREA_DIVIDER:      SetState(STATE_MOVING_DIVIDER, area.Item2); break;
            }
        }

        //--------------------------------------------------------------------------------
        public virtual void InjectMouseUp(int x, int y) {
            SetState(STATE_NONE);
        }

        //--------------------------------------------------------------------------------
        public virtual void InjectMouseMove(int x, int y) {
            // States
            if (mState == STATE_MOVING) {
                // Moving
                mRegion.X = Math.Min(Math.Max(mOriginalDimensions.X + (x - mAnchor.X), 0), mPageSize.Width - 1 - mRegion.Width);
                mRegion.Y = Math.Min(Math.Max(mOriginalDimensions.Y + (y - mAnchor.Y), 0), mPageSize.Height - 1 - mRegion.Height);
            }
            else if (mState == STATE_MOVING_DIVIDER) {
                // Moving divider
                int minimumDividerPosition = mRegion.PreviousDivider(mDivider);
                minimumDividerPosition = (minimumDividerPosition != -1 ? minimumDividerPosition + MINIMUM_COLUMN_WIDTH : MINIMUM_COLUMN_WIDTH);
                int maximumDividerPosition = mRegion.NextDivider(mDivider);
                maximumDividerPosition = (maximumDividerPosition != -1 ? maximumDividerPosition - MINIMUM_COLUMN_WIDTH : mRegion.Width - MINIMUM_COLUMN_WIDTH);

                // Move
                int dividerPosition = Math.Min(Math.Max(mOriginalDividerPosition + (x - mAnchor.X), minimumDividerPosition), maximumDividerPosition);
                mRegion.SetDivider(mDivider, dividerPosition);
            }
            else {
                // Previous position
                int regionX = mRegion.X;

                // Resizing
                if ((mState == STATE_SIZING_TOP_LEFT) || (mState == STATE_SIZING_LEFT) || (mState == STATE_SIZING_BOTTOM_LEFT)) {
                    // Leftward
                    int xLimit = Math.Min(mPageSize.Width - 1, mOriginalDimensions.X + mOriginalDimensions.Width) - MinimumWidth;
                    mRegion.X = Math.Min(Math.Max(mOriginalDimensions.X + (x - mAnchor.X), 0), xLimit);
                    mRegion.Width = (mOriginalDimensions.X + mOriginalDimensions.Width) - mRegion.X;
                }
                if ((mState == STATE_SIZING_TOP_LEFT) || (mState == STATE_SIZING_TOP) || (mState == STATE_SIZING_TOP_RIGHT)) {
                    // Upward
                    int yLimit = Math.Min(mPageSize.Height - 1, mOriginalDimensions.Y + mOriginalDimensions.Height) - MINIMUM_HEIGHT;
                    mRegion.Y = Math.Min(Math.Max(mOriginalDimensions.Y + (y - mAnchor.Y), 0), yLimit);
                    mRegion.Height = (mOriginalDimensions.Y + mOriginalDimensions.Height) - mRegion.Y;
                }
                if ((mState == STATE_SIZING_TOP_RIGHT) || (mState == STATE_SIZING_RIGHT) || (mState == STATE_SIZING_BOTTOM_RIGHT)) {
                    // Rightward
                    mRegion.Width = Math.Min(Math.Max(mOriginalDimensions.Width + (x - mAnchor.X), MinimumWidth), mPageSize.Width - 1 - mOriginalDimensions.X);
                }
                if ((mState == STATE_SIZING_BOTTOM_LEFT) || (mState == STATE_SIZING_BOTTOM) || (mState == STATE_SIZING_BOTTOM_RIGHT)) {
                    // Downward
                    mRegion.Height = Math.Min(Math.Max(mOriginalDimensions.Height + (y - mAnchor.Y), MINIMUM_HEIGHT), mPageSize.Height - 1 - mOriginalDimensions.Y);
                }

                // Minimum column width
                if ((mState == STATE_SIZING_TOP_LEFT) || (mState == STATE_SIZING_LEFT) || (mState == STATE_SIZING_BOTTOM_LEFT)) {
                    mRegion.ShiftDividers(regionX - mRegion.X);
                    mRegion.ApplyMinimumColumnWidth(MINIMUM_COLUMN_WIDTH, true);
                }
                else if ((mState == STATE_SIZING_TOP_RIGHT) || (mState == STATE_SIZING_RIGHT) || (mState == STATE_SIZING_BOTTOM_RIGHT))
                    mRegion.ApplyMinimumColumnWidth(MINIMUM_COLUMN_WIDTH, false);
            }
        }
        

        // REGION ================================================================================
        //--------------------------------------------------------------------------------
        public PDFRegion Region { get { return mRegion; } }
        public int MinimumWidth { get { return Math.Max(MINIMUM_WIDTH, mRegion.MinimumTotalWidth(MINIMUM_COLUMN_WIDTH)); } }
        

        // REGION - DIVIDERS ================================================================================
        //--------------------------------------------------------------------------------
        public bool AddDivider(int x, int y) {
            // Checks
            if (mRegion.ExcessColumnSpace(MINIMUM_COLUMN_WIDTH) < MINIMUM_COLUMN_WIDTH)
                return false;

            // Add
            int position = x - mRegion.X;
            mRegion.AddDivider(position, MINIMUM_COLUMN_WIDTH);
            return true;
        }

        //--------------------------------------------------------------------------------
        public void RemoveDivider(int x, int y) {
            // Checks
            if (mRegion.DividerCount == 0)
                return;

            // Nearest divider
            int nearestIndex = -1;
            int nearestDistance = mRegion.Width * 2;
            for (int i = 0; i < mRegion.DividerCount; ++i) {
                int distance = Math.Abs((x - mRegion.X) - mRegion.Divider(i));
                if (distance < nearestDistance) {
                    nearestIndex = i;
                    nearestDistance = distance;
                }
            }

            // Remove
            mRegion.RemoveDivider(nearestIndex);
        }


        // STATE ================================================================================
        //--------------------------------------------------------------------------------
        private void SetState(int state, int divider = 0) {
            // Checks
            if (mState == state)
                return;

            // Disable
            OnStateDisabled(mState);

            // Divider
            mDivider = divider;

            // Set
            mState = state;

            // Enable
            OnStateEnabled(mState);
        }

        //--------------------------------------------------------------------------------
        protected virtual void OnStateEnabled(int state) {
            if (state == STATE_REPOSITION) {
                // Reposition
                mRegion.Width = MinimumWidth;
                mRegion.Height = MINIMUM_HEIGHT;
                mRegion.X = Math.Min(mAnchor.X, mPageSize.Width - mRegion.Width);
                mRegion.Y = Math.Min(mAnchor.Y, mPageSize.Height - mRegion.Height);

                // Original dimensions
                mOriginalDimensions = mRegion.Dimensions; // Update so that sizing works correctly
                mOriginalDimensions.Width = 0;
                mOriginalDimensions.Height = 0;

                // Next state
                SetState(STATE_SIZING_BOTTOM_RIGHT);
            }
            else if (state == STATE_MOVING_DIVIDER) {
                // Divider
                mOriginalDividerPosition = mRegion.Divider(mDivider);
            }
        }

        //--------------------------------------------------------------------------------
        protected virtual void OnStateDisabled(int state) { }


        // PAGE ================================================================================
        //--------------------------------------------------------------------------------
        public void SetPageSize(int width, int height) {
            mPageSize.Width = width;
            mPageSize.Height = height;
        }


        // AREAS ================================================================================
        //--------------------------------------------------------------------------------
        public virtual Tuple<int, int> AreaAt(int x, int y) {
            // None
            if (Region.WholePage || x < (Region.X - RESIZE_MARGIN) || x >= (Region.X + Region.Width + RESIZE_MARGIN) ||
                y < (Region.Y - RESIZE_MARGIN) || y >= (Region.Y + Region.Height + RESIZE_MARGIN))
            {
                return new Tuple<int, int>(AREA_NONE, 0);
            }

            // Sides
            int fromLeft = x - (Region.X - RESIZE_MARGIN);
            int fromRight = (Region.X2 - 1 + RESIZE_MARGIN) - x;
            int fromTop = y - (Region.Y - RESIZE_MARGIN);
            int fromBottom = (Region.Y2 - 1 + RESIZE_MARGIN) - y;

            bool onLeft = (fromLeft >= 0) && (fromLeft < 2 + RESIZE_MARGIN * 2);
            bool onRight = (fromRight >= 0) && (fromRight < 2 + RESIZE_MARGIN * 2);
            bool onTop = (fromTop >= 0) && (fromTop < 2 + RESIZE_MARGIN * 2);
            bool onBottom = (fromBottom >= 0) && (fromBottom < 2 + RESIZE_MARGIN * 2);

            // Area
            if (onTop && onLeft)
                return new Tuple<int, int>(AREA_TOP_LEFT, 0);
            else if (onTop && onRight)
                return new Tuple<int, int>(AREA_TOP_RIGHT, 0);
            else if (onBottom && onLeft)
                return new Tuple<int, int>(AREA_BOTTOM_LEFT, 0);
            else if (onBottom && onRight)
                return new Tuple<int, int>(AREA_BOTTOM_RIGHT, 0);
            else if (onLeft)
                return new Tuple<int, int>(AREA_LEFT, 0);
            else if (onRight)
                return new Tuple<int, int>(AREA_RIGHT, 0);
            else if (onTop)
                return new Tuple<int, int>(AREA_TOP, 0);
            else if (onBottom)
                return new Tuple<int, int>(AREA_BOTTOM, 0);
            else {
                // Dividers
                for (int i = 0; i < Region.DividerCount; ++i) {
                    int fromDivider = x - (Region.X + Region.Divider(i) - 1 - RESIZE_MARGIN);
                    if ((fromDivider >= 0) && (fromDivider < 2 + RESIZE_MARGIN * 2))
                        return new Tuple<int, int>(AREA_DIVIDER, i);
                }

                // Inside
                return new Tuple<int, int>(AREA_INSIDE, 0);
            }
        }
        

        // CURSOR ================================================================================
        //--------------------------------------------------------------------------------
        public virtual Cursor GetCursor(int x, int y) {
            switch (mState) {
                case STATE_NONE:
                    switch (AreaAt(x, y).Item1) {
                        case AREA_TOP_LEFT:     return Cursors.SizeNWSE;
                        case AREA_TOP:          return Cursors.SizeNS;
                        case AREA_TOP_RIGHT:    return Cursors.SizeNESW;
                        case AREA_LEFT:         return Cursors.SizeWE;
                        case AREA_RIGHT:        return Cursors.SizeWE;
                        case AREA_BOTTOM_LEFT:  return Cursors.SizeNESW;
                        case AREA_BOTTOM:       return Cursors.SizeNS;
                        case AREA_BOTTOM_RIGHT: return Cursors.SizeNWSE;
                        case AREA_INSIDE:       return Cursors.SizeAll;
                        case AREA_DIVIDER:      return Cursors.SizeWE;
                        default:                return Cursors.Default;
                    }
                case STATE_MOVING:              return Cursors.SizeAll;
                case STATE_SIZING_TOP_LEFT:     return Cursors.SizeNWSE;
                case STATE_SIZING_TOP:          return Cursors.SizeNS;
                case STATE_SIZING_TOP_RIGHT:    return Cursors.SizeNESW;
                case STATE_SIZING_LEFT:         return Cursors.SizeWE;
                case STATE_SIZING_RIGHT:        return Cursors.SizeWE;
                case STATE_SIZING_BOTTOM_LEFT:  return Cursors.SizeNESW;
                case STATE_SIZING_BOTTOM:       return Cursors.SizeNS;
                case STATE_SIZING_BOTTOM_RIGHT: return Cursors.SizeNWSE;
                case STATE_MOVING_DIVIDER:      return Cursors.SizeWE;
                default:                        return Cursors.Default;
            }
        }

    }

}

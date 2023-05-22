using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;



namespace PDFScrape.Macros.Events {

    public class MacroMouseEvent2 : MacroEvent2 {
        //================================================================================
        public enum EventType {
            MOVE,
            UP,
            DOWN,
            WHEEL
        }


        //================================================================================
        private EventType                       mType;

        private Point                           mPosition;
        private MouseButton                     mButton;
        private int                             mScrollAmount;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroMouseEvent2(EventType type, Point position, MouseButton button = MouseButton.Left, int scrollAmount = 0) {
            mType = type;
            mPosition = position;
            mButton = button;
            mScrollAmount = scrollAmount;
        }

        
        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public EventType Type { get { return mType; } }
        public bool IsMoveEvent { get { return (mType == EventType.MOVE); } }
        public bool IsUpEvent { get { return (mType == EventType.UP); } }
        public bool IsDownEvent { get { return (mType == EventType.DOWN); } }
        public bool IsWheelEvent { get { return (mType == EventType.WHEEL); } }
        
        
        // PROPERTIES ================================================================================
        //--------------------------------------------------------------------------------
        public Point Position { get { return mPosition; } }
        
        //--------------------------------------------------------------------------------
        private Point AbsolutePosition {
            get {
                return new Point((Convert.ToDouble(65535) * (mPosition.X + 0.5)) / Convert.ToDouble(Screen.PrimaryScreen.Bounds.Width),
                                 (Convert.ToDouble(65535) * (mPosition.Y + 0.5)) / Convert.ToDouble(Screen.PrimaryScreen.Bounds.Height));
            }
        }
        
        //--------------------------------------------------------------------------------
        public MouseButton Button { get { return mButton; } }
        public int ScrollAmount { get { return mScrollAmount; } }


        // PLAYING ================================================================================
        //--------------------------------------------------------------------------------
        public override void Play(MacroPlayer2 player) {
            // Absolute position
            Point absolutePosition = AbsolutePosition;

            // Play
            switch (mType) {
                case EventType.MOVE:
                    player.MouseSimulator.MoveMouseTo(absolutePosition.X, absolutePosition.Y);
                    break;

                case EventType.UP:
                    player.MouseSimulator.MoveMouseTo(absolutePosition.X, absolutePosition.Y);
                    if (mButton == MouseButton.Left)
                        player.MouseSimulator.LeftButtonUp();
                    else if (mButton == MouseButton.Right)
                        player.MouseSimulator.RightButtonUp();
                    break;

                case EventType.DOWN:
                    player.MouseSimulator.MoveMouseTo(absolutePosition.X, absolutePosition.Y);
                    if (mButton == MouseButton.Left)
                        player.MouseSimulator.LeftButtonDown();
                    else if (mButton == MouseButton.Right)
                        player.MouseSimulator.RightButtonDown();
                    break;

                case EventType.WHEEL:
                    player.MouseSimulator.MoveMouseTo(absolutePosition.X, absolutePosition.Y);
                    player.MouseSimulator.VerticalScroll(mScrollAmount / 120);
                    break;
            }
        }
    }

}

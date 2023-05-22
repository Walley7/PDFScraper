using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;



namespace PDFScrape.Macros.Events {

    public class MacroMouseEvent : MacroEvent {
        //================================================================================
        public enum ActionType {
            MOVE,
            UP,
            DOWN,
            WHEEL
        }


        //================================================================================
        private ActionType                      mAction;

        private Point                           mPosition;
        private MouseButton                     mButton;
        private int                             mScrollAmount;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroMouseEvent() : this(ActionType.MOVE, new Point(), MouseButton.Left, 0) { }

        //--------------------------------------------------------------------------------
        public MacroMouseEvent(ActionType action, Point position, MouseButton button = MouseButton.Left, int scrollAmount = 0) {
            mAction = action;
            mPosition = position;
            mButton = button;
            mScrollAmount = scrollAmount;
        }


        // PLAYING ================================================================================
        //--------------------------------------------------------------------------------
        public override bool Play(MacroPlayer player) {
            // Notify
            player.NotifyPlayingElement(this);

            // Absolute position
            Point absolutePosition = AbsolutePosition;

            // Play
            switch (mAction) {
                case ActionType.MOVE:
                    player.MouseSimulator.MoveMouseTo(absolutePosition.X, absolutePosition.Y);
                    break;

                case ActionType.UP:
                    player.MouseSimulator.MoveMouseTo(absolutePosition.X, absolutePosition.Y);
                    if (mButton == MouseButton.Left)
                        player.MouseSimulator.LeftButtonUp();
                    else if (mButton == MouseButton.Right)
                        player.MouseSimulator.RightButtonUp();
                    break;

                case ActionType.DOWN:
                    player.MouseSimulator.MoveMouseTo(absolutePosition.X, absolutePosition.Y);
                    if (mButton == MouseButton.Left)
                        player.MouseSimulator.LeftButtonDown();
                    else if (mButton == MouseButton.Right)
                        player.MouseSimulator.RightButtonDown();
                    break;

                case ActionType.WHEEL:
                    player.MouseSimulator.MoveMouseTo(absolutePosition.X, absolutePosition.Y);
                    player.MouseSimulator.VerticalScroll(mScrollAmount / 120);
                    break;
            }

            // Return
            return true;
        }

        
        // ACTION ================================================================================
        //--------------------------------------------------------------------------------
        public ActionType Action { get { return mAction; } }
        public bool IsMoveAction { get { return (mAction == ActionType.MOVE); } }
        public bool IsUpAction { get { return (mAction == ActionType.UP); } }
        public bool IsDownAction { get { return (mAction == ActionType.DOWN); } }
        public bool IsWheelAction { get { return (mAction == ActionType.WHEEL); } }
        
        
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


        // PRESENTATION ================================================================================
        //--------------------------------------------------------------------------------
        public override System.Drawing.Color Colour { get { return (!IsMoveAction ? System.Drawing.Color.FromArgb(127, 191, 255) : System.Drawing.Color.FromArgb(191, 191, 191)); } }
        public override OverlayType Overlay { get { return (IsDownAction || IsWheelAction ? OverlayType.POSITIONED : OverlayType.NONE); } }

        //--------------------------------------------------------------------------------
        public override string OverlayMessage {
            get {
                // Variables
                string message = "";

                // Move
                if (Action == ActionType.MOVE)
                    message = "Move";

                // Up / down
                if (Action == ActionType.UP || Action == ActionType.DOWN) {
                    // Action
                    /*switch (Action) {
                        case ActionType.UP:     message += "- "; break;
                        case ActionType.DOWN:   message += "+ "; break;
                    }*/

                    // Button
                    switch (Button) {
                        case MouseButton.Left:      message += "Left"; break;
                        case MouseButton.Middle:    message += "Middle"; break;
                        case MouseButton.Right:     message += "Right"; break;
                        case MouseButton.XButton1:  message += "Side 1"; break;
                        case MouseButton.XButton2:  message += "Side 2"; break;
                    }
                }

                // Wheel
                if (Action == ActionType.WHEEL)
                    message += "Wheel " + (mScrollAmount >= 0 ? "up" : "down");

                // Return
                return message;
            }
        }

        //--------------------------------------------------------------------------------
        public override System.Drawing.Color OverlayColour1 { get { return System.Drawing.Color.FromArgb(127, 191, 255); } }
        public override System.Drawing.Color OverlayColour2 { get { return System.Drawing.Color.FromArgb(0, 63, 127); } }
        public override int? OverlayX { get { return (int)Math.Round(Position.X); } }
        public override int? OverlayY { get { return (int)Math.Round(Position.Y); } }
        public override long OverlayDuration { get { return 1 * TimeSpan.TicksPerSecond; } }
        
        
        // INFORMATION ================================================================================
        //--------------------------------------------------------------------------------
        public override string[] Information {
            get {
                // Type
                string type = "Mouse ";

                // Action
                switch (Action) {
                    case ActionType.MOVE:   type += "Move"; break;
                    case ActionType.UP:     type += "Up"; break;
                    case ActionType.DOWN:   type += "Down"; break;
                    case ActionType.WHEEL:  type += "Wheel"; break;
                }

                // Button
                if (Action == ActionType.UP || Action == ActionType.DOWN) {
                    switch (Button) {
                        case MouseButton.Left:      type += " (Left Button)"; break;
                        case MouseButton.Middle:    type += " (Middle Button)"; break;
                        case MouseButton.Right:     type += " (Right Button)"; break;
                        case MouseButton.XButton1:  type += " (Side Button 1)"; break;
                        case MouseButton.XButton2:  type += " (Side Button 2)"; break;
                    }
                }

                // Parameters
                string parameters = "x: " + Position.X + ", y: " + Position.Y;

                // Scroll amount
                if (Action == ActionType.WHEEL)
                    parameters += ", scroll: " + ScrollAmount;

                // Return
                return new string[] { type, parameters };
            }
        }

        
        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public override void WriteJSON(JsonTextWriter writer) {
            writer.WritePropertyName("action"); writer.WriteValue((int)mAction);
            writer.WritePropertyName("position_x"); writer.WriteValue(mPosition.X);
            writer.WritePropertyName("position_y"); writer.WriteValue(mPosition.Y);
            writer.WritePropertyName("button"); writer.WriteValue((int)mButton);
            writer.WritePropertyName("scroll_amount"); writer.WriteValue(mScrollAmount);
        }

        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            mAction = (ActionType)((int)token.SelectToken("action"));
            mPosition.X = (double)token.SelectToken("position_x");
            mPosition.Y = (double)token.SelectToken("position_y");
            mButton = (MouseButton)((int)token.SelectToken("button"));
            mScrollAmount = (int)token.SelectToken("scroll_amount");
        }
    }

}

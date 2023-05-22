using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Silence.Simulation.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Macros.Events {

    public class MacroKeyEvent : MacroEvent {
        //================================================================================
        public enum ActionType {
            UP,
            DOWN
        }


        //================================================================================
        private ActionType                      mAction;

        private int                             mKey;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroKeyEvent() : this(ActionType.DOWN, 0) { }

        //--------------------------------------------------------------------------------
        public MacroKeyEvent(ActionType action, int key) {
            mAction = action;
            mKey = key;
        }


        // PLAYING ================================================================================
        //--------------------------------------------------------------------------------
        public override bool Play(MacroPlayer player) {
            // Notify
            player.NotifyPlayingElement(this);

            // Play
            switch (mAction) {
                case ActionType.UP:     player.KeyboardSimulator.KeyUp((VirtualKeyCode)mKey); break;
                case ActionType.DOWN:   player.KeyboardSimulator.KeyDown((VirtualKeyCode)mKey); break;
            }

            // Return
            return true;
        }


        // ACTION ================================================================================
        //--------------------------------------------------------------------------------
        public ActionType Action { get { return mAction; } }
        public bool IsUpAction { get { return (mAction == ActionType.UP); } }
        public bool IsDownAction { get { return (mAction == ActionType.DOWN); } }


        // PROPERTIES ================================================================================
        //--------------------------------------------------------------------------------
        public int Key { get { return mKey; } }
        public string KeyName { get { return VirtualKeyCodeName(mKey); } }


        // PRESENTATION ================================================================================
        //--------------------------------------------------------------------------------
        public override Color Colour { get { return Color.FromArgb(127, 239, 127); } }
        public override OverlayType Overlay { get { return (IsDownAction ? OverlayType.CENTRAL : OverlayType.NONE); } }

        //--------------------------------------------------------------------------------
        public override string OverlayMessage {
            get {
                string message = "";
                /*switch (Action) {
                    case ActionType.UP:     message += "- "; break;
                    case ActionType.DOWN:   message += "+ "; break;
                }*/
                switch (Action) {
                    case ActionType.UP:     message += "Released "; break;
                    case ActionType.DOWN:   message += "Pressed "; break;
                }

                message += "'" + KeyName + "' key ";
                return message;
            }
        }

        //--------------------------------------------------------------------------------
        public override Color OverlayColour1 { get { return Color.FromArgb(127, 255, 127); } }
        public override Color OverlayColour2 { get { return Color.FromArgb(0, 127, 0); } }


        // INFORMATION ================================================================================
        //--------------------------------------------------------------------------------
        public override string[] Information {
            get {
                // Type
                string type = "Key ";

                // Action
                switch (Action) {
                    case ActionType.UP:     type += "Up"; break;
                    case ActionType.DOWN:   type += "Down"; break;
                }

                // Key
                type += " (" + KeyName + ")";

                // Return
                return new string[] { type, "" };
            }
        }

        
        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public override void WriteJSON(JsonTextWriter writer) {
            writer.WritePropertyName("action"); writer.WriteValue((int)mAction);
            writer.WritePropertyName("key"); writer.WriteValue(mKey);
        }

        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            mAction = (ActionType)((int)token.SelectToken("action"));
            mKey = (int)token.SelectToken("key");
        }       


        // KEY NAMES ================================================================================
        //--------------------------------------------------------------------------------
        public string VirtualKeyCodeName(int key) {
            switch ((VirtualKeyCode)key) {
                case VirtualKeyCode.LBUTTON:                return "Left Mouse Button";
                case VirtualKeyCode.RBUTTON:                return "Right Mouse Button";
                case VirtualKeyCode.CANCEL:                 return "Control-Break";
                case VirtualKeyCode.MBUTTON:                return "Middle Mouse Button";
                case VirtualKeyCode.XBUTTON1:               return "X1 Mouse Button";
                case VirtualKeyCode.XBUTTON2:               return "X2 Mouse Button";
                case VirtualKeyCode.BACK:                   return "Backspace";
                case VirtualKeyCode.TAB:                    return "Tab";
                case VirtualKeyCode.CLEAR:                  return "Clear";
                case VirtualKeyCode.RETURN:                 return "Enter";
                case VirtualKeyCode.SHIFT:                  return "Shift";
                case VirtualKeyCode.CONTROL:                return "Control";
                case VirtualKeyCode.MENU:                   return "Menu";
                case VirtualKeyCode.PAUSE:                  return "Pause";
                case VirtualKeyCode.CAPITAL:                return "Caps Lock";
                case VirtualKeyCode.KANA:                   return "IME Kana / Hangeul / Hangul Mode";
                case VirtualKeyCode.JUNJA:                  return "IME Junja Mode";
                case VirtualKeyCode.FINAL:                  return "IME Final Mode";
                case VirtualKeyCode.HANJA:                  return "IME Hanja / Kanji Mode";
                case VirtualKeyCode.ESCAPE:                 return "Escape";
                case VirtualKeyCode.CONVERT:                return "IME Convert";
                case VirtualKeyCode.NONCONVERT:             return "IME Nonconvert";
                case VirtualKeyCode.ACCEPT:                 return "IME Accept";
                case VirtualKeyCode.MODECHANGE:             return "IME Mode Change";
                case VirtualKeyCode.SPACE:                  return "Spacebar";
                case VirtualKeyCode.PRIOR:                  return "Page Up";
                case VirtualKeyCode.NEXT:                   return "Page Down";
                case VirtualKeyCode.END:                    return "End";
                case VirtualKeyCode.HOME:                   return "Home";
                case VirtualKeyCode.LEFT:                   return "Left Arrow";
                case VirtualKeyCode.UP:                     return "Up Arrow";
                case VirtualKeyCode.RIGHT:                  return "Right Arrow";
                case VirtualKeyCode.DOWN:                   return "Down Arrow";
                case VirtualKeyCode.SELECT:                 return "Select";
                case VirtualKeyCode.PRINT:                  return "Print";
                case VirtualKeyCode.EXECUTE:                return "Execute";
                case VirtualKeyCode.SNAPSHOT:               return "Print Screen";
                case VirtualKeyCode.INSERT:                 return "Insert";
                case VirtualKeyCode.DELETE:                 return "Delete";
                case VirtualKeyCode.HELP:                   return "Help";
                case VirtualKeyCode.VK_0:                   return "0";
                case VirtualKeyCode.VK_1:                   return "1";
                case VirtualKeyCode.VK_2:                   return "2";
                case VirtualKeyCode.VK_3:                   return "3";
                case VirtualKeyCode.VK_4:                   return "4";
                case VirtualKeyCode.VK_5:                   return "5";
                case VirtualKeyCode.VK_6:                   return "6";
                case VirtualKeyCode.VK_7:                   return "7";
                case VirtualKeyCode.VK_8:                   return "8";
                case VirtualKeyCode.VK_9:                   return "9";
                case VirtualKeyCode.VK_A:                   return "A";
                case VirtualKeyCode.VK_B:                   return "B";
                case VirtualKeyCode.VK_C:                   return "C";
                case VirtualKeyCode.VK_D:                   return "D";
                case VirtualKeyCode.VK_E:                   return "E";
                case VirtualKeyCode.VK_F:                   return "F";
                case VirtualKeyCode.VK_G:                   return "G";
                case VirtualKeyCode.VK_H:                   return "H";
                case VirtualKeyCode.VK_I:                   return "I";
                case VirtualKeyCode.VK_J:                   return "J";
                case VirtualKeyCode.VK_K:                   return "K";
                case VirtualKeyCode.VK_L:                   return "L";
                case VirtualKeyCode.VK_M:                   return "M";
                case VirtualKeyCode.VK_N:                   return "N";
                case VirtualKeyCode.VK_O:                   return "O";
                case VirtualKeyCode.VK_P:                   return "P";
                case VirtualKeyCode.VK_Q:                   return "Q";
                case VirtualKeyCode.VK_R:                   return "R";
                case VirtualKeyCode.VK_S:                   return "S";
                case VirtualKeyCode.VK_T:                   return "T";
                case VirtualKeyCode.VK_U:                   return "U";
                case VirtualKeyCode.VK_V:                   return "V";
                case VirtualKeyCode.VK_W:                   return "W";
                case VirtualKeyCode.VK_X:                   return "X";
                case VirtualKeyCode.VK_Y:                   return "Y";
                case VirtualKeyCode.VK_Z:                   return "Z";
                case VirtualKeyCode.LWIN:                   return "Left Windows";
                case VirtualKeyCode.RWIN:                   return "Right Windows";
                case VirtualKeyCode.APPS:                   return "Applications";
                case VirtualKeyCode.SLEEP:                  return "Sleep";
                case VirtualKeyCode.NUMPAD0:                return "Numpad 0";
                case VirtualKeyCode.NUMPAD1:                return "Numpad 1";
                case VirtualKeyCode.NUMPAD2:                return "Numpad 2";
                case VirtualKeyCode.NUMPAD3:                return "Numpad 3";
                case VirtualKeyCode.NUMPAD4:                return "Numpad 4";
                case VirtualKeyCode.NUMPAD5:                return "Numpad 5";
                case VirtualKeyCode.NUMPAD6:                return "Numpad 6";
                case VirtualKeyCode.NUMPAD7:                return "Numpad 7";
                case VirtualKeyCode.NUMPAD8:                return "Numpad 8";
                case VirtualKeyCode.NUMPAD9:                return "Numpad 9";
                case VirtualKeyCode.MULTIPLY:               return "Numpad *";
                case VirtualKeyCode.ADD:                    return "Numpad +";
                case VirtualKeyCode.SEPARATOR:              return "Separator";
                case VirtualKeyCode.SUBTRACT:               return "Numpad -";
                case VirtualKeyCode.DECIMAL:                return "Numpad .";
                case VirtualKeyCode.DIVIDE:                 return "Numpad /";
                case VirtualKeyCode.F1:                     return "F1";
                case VirtualKeyCode.F2:                     return "F2";
                case VirtualKeyCode.F3:                     return "F3";
                case VirtualKeyCode.F4:                     return "F4";
                case VirtualKeyCode.F5:                     return "F5";
                case VirtualKeyCode.F6:                     return "F6";
                case VirtualKeyCode.F7:                     return "F7";
                case VirtualKeyCode.F8:                     return "F8";
                case VirtualKeyCode.F9:                     return "F9";
                case VirtualKeyCode.F10:                    return "F10";
                case VirtualKeyCode.F11:                    return "F11";
                case VirtualKeyCode.F12:                    return "F12";
                case VirtualKeyCode.F13:                    return "F13";
                case VirtualKeyCode.F14:                    return "F14";
                case VirtualKeyCode.F15:                    return "F15";
                case VirtualKeyCode.F16:                    return "F16";
                case VirtualKeyCode.F17:                    return "F17";
                case VirtualKeyCode.F18:                    return "F18";
                case VirtualKeyCode.F19:                    return "F19";
                case VirtualKeyCode.F20:                    return "F20";
                case VirtualKeyCode.F21:                    return "F21";
                case VirtualKeyCode.F22:                    return "F22";
                case VirtualKeyCode.F23:                    return "F23";
                case VirtualKeyCode.F24:                    return "F24";
                case VirtualKeyCode.NUMLOCK:                return "Num Lock";
                case VirtualKeyCode.SCROLL:                 return "Scroll Lock";
                case VirtualKeyCode.LSHIFT:                 return "Left Shift";
                case VirtualKeyCode.RSHIFT:                 return "Right Shift";
                case VirtualKeyCode.LCONTROL:               return "Left Control";
                case VirtualKeyCode.RCONTROL:               return "Right Control";
                case VirtualKeyCode.LMENU:                  return "Left Alt";
                case VirtualKeyCode.RMENU:                  return "Right Alt";
                case VirtualKeyCode.BROWSER_BACK:           return "Browser Back";
                case VirtualKeyCode.BROWSER_FORWARD:        return "Browser Forward";
                case VirtualKeyCode.BROWSER_REFRESH:        return "Browser Refresh";
                case VirtualKeyCode.BROWSER_STOP:           return "Browser Stop";
                case VirtualKeyCode.BROWSER_SEARCH:         return "Browser Search";
                case VirtualKeyCode.BROWSER_FAVORITES:      return "Browser Favorites";
                case VirtualKeyCode.BROWSER_HOME:           return "Browser Home";
                case VirtualKeyCode.VOLUME_MUTE:            return "Volume Mute";
                case VirtualKeyCode.VOLUME_DOWN:            return "Volume Down";
                case VirtualKeyCode.VOLUME_UP:              return "Volume Up";
                case VirtualKeyCode.MEDIA_NEXT_TRACK:       return "Media Next Track";
                case VirtualKeyCode.MEDIA_PREV_TRACK:       return "Media Previous Track";
                case VirtualKeyCode.MEDIA_STOP:             return "Media Stop";
                case VirtualKeyCode.MEDIA_PLAY_PAUSE:       return "Media Play / Pause";
                case VirtualKeyCode.LAUNCH_MAIL:            return "Launch Mail";
                case VirtualKeyCode.LAUNCH_MEDIA_SELECT:    return "Launch Media";
                case VirtualKeyCode.LAUNCH_APP1:            return "Launch Application 1";
                case VirtualKeyCode.LAUNCH_APP2:            return "Launch Application 2";
                case VirtualKeyCode.OEM_1:                  return ";";
                case VirtualKeyCode.OEM_PLUS:               return "+";
                case VirtualKeyCode.OEM_COMMA:              return ",";
                case VirtualKeyCode.OEM_MINUS:              return "-";
                case VirtualKeyCode.OEM_PERIOD:             return ".";
                case VirtualKeyCode.OEM_2:                  return "/";
                case VirtualKeyCode.OEM_3:                  return "`";
                case VirtualKeyCode.OEM_4:                  return "[";
                case VirtualKeyCode.OEM_5:                  return "\\";
                case VirtualKeyCode.OEM_6:                  return "]";
                case VirtualKeyCode.OEM_7:                  return "'";
                case VirtualKeyCode.OEM_8:                  return "OEM 8";
                case VirtualKeyCode.OEM_102:                return "OEM 102";
                case VirtualKeyCode.PROCESSKEY:             return "IME Process";
                case VirtualKeyCode.PACKET:                 return "Packet";
                case VirtualKeyCode.ATTN:                   return "Attn";
                case VirtualKeyCode.CRSEL:                  return "Crsel";
                case VirtualKeyCode.EXSEL:                  return "Exsel";
                case VirtualKeyCode.EREOF:                  return "Erase EOF";
                case VirtualKeyCode.PLAY:                   return "Play";
                case VirtualKeyCode.ZOOM:                   return "Zoom";
                case VirtualKeyCode.NONAME:                 return "NO NAME";
                case VirtualKeyCode.PA1:                    return "PA1";
                case VirtualKeyCode.OEM_CLEAR:              return "Clear";
                default:                                    return "UNKNOWN";
            }
        }
    }

}

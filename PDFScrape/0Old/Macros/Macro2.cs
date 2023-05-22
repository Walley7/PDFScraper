using PDFScrape.Macros.Events;
using Silence.Simulation.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Macros {

    public class Macro2 {
        //================================================================================
        private List<MacroEvent2>               mEvents = new List<MacroEvent2>();
        

        //================================================================================
        //--------------------------------------------------------------------------------
        public Macro2() {

        }
        

        // EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        public void AddEvent(MacroEvent2 macroEvent) {
            mEvents.Add(macroEvent);
        }
        
        //--------------------------------------------------------------------------------
        public void RemoveAllEvents() {
            mEvents.Clear();
        }
        
        //--------------------------------------------------------------------------------
        public void Clear() { RemoveAllEvents(); }
        public MacroEvent2[] Events { get { return (mEvents != null ? mEvents.ToArray() : null); } }
        
        //--------------------------------------------------------------------------------
        /*public void RemoveKeyUntilFirstRelease(VirtualKeyCode key) {
            // Keys
            List<VirtualKeyCode> keys = new List<VirtualKeyCode>();
            keys.Add(key);

            // Key groups
            if (key == VirtualKeyCode.CONTROL) {
                keys.Add(VirtualKeyCode.LCONTROL);
                keys.Add(VirtualKeyCode.RCONTROL);
            }
            else if (key == VirtualKeyCode.SHIFT) {
                keys.Add(VirtualKeyCode.LSHIFT);
                keys.Add(VirtualKeyCode.RSHIFT);
            }
            else if (key == VirtualKeyCode.MENU) {
                keys.Add(VirtualKeyCode.LMENU);
                keys.Add(VirtualKeyCode.RMENU);
            }

            // Mark
            for (int i = 0; i < mEvents.Count; ++i) {
                if (mEvents[i] is MacroKeyEvent) {
                    MacroKeyEvent keyEvent = (MacroKeyEvent)mEvents[i];
                    if (keys.Contains((VirtualKeyCode)keyEvent.Key)) {
                        // Group culling (remove other keys in group)
                        if (keys.Count > 1)
                            keys.RemoveAll(k => k != (VirtualKeyCode)keyEvent.Key);

                        // Mark
                        mEvents[i] = null;
                        if (keyEvent.IsUpEvent)
                            break;
                    }
                }
            }

            // Remove
        }*/
        
        //--------------------------------------------------------------------------------
        public void AddKeyUpsForUnreleasedKeys() {
            // Map
            bool[] keyDown = Enumerable.Repeat(false, 256).ToArray();

            // Events
            for (int i = 0; i < mEvents.Count; ++i) {
                if (mEvents[i] is MacroKeyEvent2) {
                    MacroKeyEvent2 keyEvent = (MacroKeyEvent2)mEvents[i];
                    if (keyEvent.IsDownEvent)
                        keyDown[keyEvent.Key] = true;
                    else if (keyEvent.IsUpEvent)
                        keyDown[keyEvent.Key] = false;
                }
            }

            // Key ups
            for (int i = 0; i < keyDown.Length; ++i) {
                if (keyDown[i])
                    mEvents.Add(new MacroKeyEvent2(MacroKeyEvent2.EventType.UP, i));
            }
        }

        // XML ================================================================================
        //--------------------------------------------------------------------------------
        //public override string XMLString { get { return ""; } }
        //Save(XmlDoc)?

        //--------------------------------------------------------------------------------
        /*public string XMLString {
            get {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("<Macro>");
                foreach (MacroEvent e in mEvents) {
                    stringBuilder.AppendLine(e.ToXml());
                }
                stringBuilder.AppendLine("</Macro>");
                return stringBuilder.ToString();
            }
        }*/
    }

}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDFScrape.Macros.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace PDFScrape.Macros {

    public class MacroNode : MacroElement {
        //================================================================================
        protected List<MacroElement>            mElements = new List<MacroElement>();
        

        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroNode() { }


        // PLAYING ================================================================================
        //--------------------------------------------------------------------------------
        public override bool Play(MacroPlayer player) {
            // Notify
            player.NotifyPlayingElement(this);

            // Play
            return PlayElements(player);
        }

        //--------------------------------------------------------------------------------
        protected bool PlayElements(MacroPlayer player) {
            // Elements
            for (int i = 0; i < ElementCount;) {
                if (player.Stopping)
                    return false;

                if (player.Enabled && !player.Paused) {
                    MacroElement element = Element(i);
                    if (!element.Play(player))
                        return false;
                    ++i;
                }
                else
                    Thread.Sleep(1);
            }

            // Return
            return true;
        }


        // MACRO ================================================================================
        //--------------------------------------------------------------------------------
        protected override Macro _Macro { get { return (HasParent ? Parent.Macro : null); } }


        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public override bool IsNode { get { return true; } }
        public override bool IsEvent { get { return false; } }


        // ELEMENTS ================================================================================
        //--------------------------------------------------------------------------------
        public override MacroElement AddElement(MacroElement element) {
            if (element.HasParent)
                throw new ArgumentException();
            mElements.Add(element);
            element.OnAdded(this);
            IncrementChangeCount();
            return element;
        }

        //--------------------------------------------------------------------------------
        protected void AddElement(string type, JToken jsonToken) {
            ConstructorInfo constructor = sElementTypes[type].GetConstructor(new Type[] {});
            MacroElement element = (MacroElement)constructor.Invoke(new object[] {});
            AddElement(element);
            element.ReadJSON(jsonToken);
        }
        
        //--------------------------------------------------------------------------------
        public override void RemoveElement(int index) {
            mElements[index].OnRemove(this);
            mElements.RemoveAt(index);
            IncrementChangeCount();
        }

        //--------------------------------------------------------------------------------
        public override void RemoveElement(MacroElement element) {
            if (mElements.Contains(element))
                element.OnRemove(this);
            mElements.Remove(element);
            IncrementChangeCount();
        }

        //--------------------------------------------------------------------------------
        public override void RemoveAllElements() {
            foreach (MacroElement e in mElements) {
                e.OnRemove(this);
            }
            mElements.Clear();
            IncrementChangeCount();
        }

        //--------------------------------------------------------------------------------
        public override MacroElement Element(int index) { return mElements[index]; }
        public override MacroNode Node(int index) { return (MacroNode)Element(index); }
        public override MacroEvent Event(int index) { return (MacroEvent)Element(index); }

        //--------------------------------------------------------------------------------
        public override int ElementCount { get { return mElements.Count; } }
        public override MacroElement[] Elements { get { return mElements.ToArray(); } }


        // KEYS ================================================================================
        //--------------------------------------------------------------------------------
        public void AddKeyUpsForUnreleasedKeys() {
            // Map
            bool[] keyDown = Enumerable.Repeat(false, 256).ToArray();

            // Events
            foreach (MacroElement e in mElements) {
                if (e is MacroKeyEvent) {
                    MacroKeyEvent keyEvent = (MacroKeyEvent)e;
                    if (keyEvent.IsDownAction)
                        keyDown[keyEvent.Key] = true;
                    else if (keyEvent.IsUpAction)
                        keyDown[keyEvent.Key] = false;
                }
            }

            // Key ups
            for (int i = 0; i < keyDown.Length; ++i) {
                if (keyDown[i])
                    AddElement(new MacroKeyEvent(MacroKeyEvent.ActionType.UP, i));
            }
        }


        // INFORMATION ================================================================================
        //--------------------------------------------------------------------------------
        public override string[] Information { get { return new string[] { "Node", "" }; } }


        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public override void WriteJSON(JsonTextWriter writer) {
            // Elements
            writer.WritePropertyName("elements");
            writer.WriteStartArray();
            foreach (MacroElement e in mElements) {
                writer.WriteStartObject();

                writer.WritePropertyName("type");
                writer.WriteValue(sElementTypes.FirstOrDefault(t => t.Value == e.GetType()).Key);
                e.WriteJSON(writer);

                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }

        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            // Reset
            RemoveAllElements();

            // Elements
            JArray elements = (JArray)token.SelectToken("elements");
            if (elements != null) {
                foreach (JToken e in elements) {
                    string type = (string)e.SelectToken("type");
                    AddElement(type, e);
                }
            }
        }
    }

}

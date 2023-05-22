using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDFScrape.Exceptions;
using PDFScrape.Macros.Events;
using PDFScrape.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Macros {

    public abstract class MacroElement : Bindable {
        //================================================================================
        public enum OverlayType {
            NONE,
            CENTRAL,
            POSITIONED
        }


        //================================================================================
        protected static Dictionary<string, Type>   sElementTypes = new Dictionary<string, Type>();

        //--------------------------------------------------------------------------------
        private MacroNode                           mParent = null;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroElement() {
            // Element types
            sElementTypes["Node"] = typeof(MacroNode);
            sElementTypes["RepetitionNode"] = typeof(MacroRepetitionNode);
            sElementTypes["BranchingNode"] = typeof(MacroBranchingNode);
            sElementTypes["BranchNode"] = typeof(MacroBranchNode);
            sElementTypes["DelayEvent"] = typeof(MacroDelayEvent);
            sElementTypes["MouseEvent"] = typeof(MacroMouseEvent);
            sElementTypes["KeyEvent"] = typeof(MacroKeyEvent);
            sElementTypes["DataInputEvent"] = typeof(MacroDataInputEvent);
        }


        // EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        internal void OnAdded(MacroNode parent) {
            mParent = parent;
        }
        
        //--------------------------------------------------------------------------------
        internal void OnRemove(MacroNode parent) {
            mParent = null;
        }


        // PLAYING ================================================================================
        //--------------------------------------------------------------------------------
        public abstract bool Play(MacroPlayer player);


        // MACRO ================================================================================
        //--------------------------------------------------------------------------------
        public virtual Macro Macro { get { return _Macro; } }
        protected virtual Macro _Macro { get { return null; } }


        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public abstract bool IsNode { get; }
        public abstract bool IsEvent { get; }


        // HIERARCHY ================================================================================
        //--------------------------------------------------------------------------------
        public MacroNode Parent { get { return mParent; } }
        public bool HasParent { get { return (Parent != null); } }


        // ELEMENTS ================================================================================
        //--------------------------------------------------------------------------------
        public virtual MacroElement AddElement(MacroElement element) { throw new InvalidCallException(); }
        public virtual void RemoveElement(int index) { throw new InvalidCallException(); }
        public virtual void RemoveElement(MacroElement element) { throw new InvalidCallException(); }
        public virtual void RemoveAllElements() { throw new InvalidCallException(); }
        public virtual MacroElement Element(int index) { throw new InvalidCallException(); }
        public virtual MacroNode Node(int index) { throw new InvalidCallException(); }
        public virtual MacroEvent Event(int index) { throw new InvalidCallException(); }
        public virtual int ElementCount { get { throw new InvalidCallException(); } }
        public virtual MacroElement[] Elements { get { throw new InvalidCallException(); } }


        // PRESENTATION ================================================================================
        //--------------------------------------------------------------------------------
        public virtual Color Colour { get { return Color.LightGray; } }
        public virtual OverlayType Overlay { get { return OverlayType.NONE; } }
        public virtual string OverlayMessage { get { return null; } }
        public virtual Color OverlayColour1 { get { return Color.White; } }
        public virtual Color OverlayColour2 { get { return Color.Gray; } }
        public virtual int? OverlayX { get { return null; } }
        public virtual int? OverlayY { get { return null; } }
        public virtual long OverlayDuration { get { return 2 * TimeSpan.TicksPerSecond; } }


        // CHANGE COUNT ================================================================================
        //--------------------------------------------------------------------------------
        public virtual void IncrementChangeCount() {
            if (Macro != null)
                Macro.IncrementChangeCount();
        }

        //--------------------------------------------------------------------------------
        public virtual void ResetChangeCount() {
            if (Macro != null)
                ResetChangeCount();
        }


        // INFORMATION ================================================================================
        //--------------------------------------------------------------------------------
        public abstract string[] Information { get; }


        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public abstract void WriteJSON(JsonTextWriter writer);
        public abstract void ReadJSON(JToken token);
    }

}

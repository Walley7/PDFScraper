using PDFScrape.Macros.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Macros.Presentation {

    public class MacroPresentationNode {
        //================================================================================
        public enum ChainType {
            None,
            Event,
            MouseMovement
        }


        //================================================================================
        // Only allow editing and deletion of macro elements for version 1.
        //================================================================================
        private List<MacroElement>              mElements = new List<MacroElement>();

        private List<MacroPresentationNode>     mChildren = new List<MacroPresentationNode>();


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroPresentationNode(IEnumerable<MacroElement> elements) {
            mElements = elements.ToList();
            BuildPresentation();
        }

        //--------------------------------------------------------------------------------
        public MacroPresentationNode(Macro macro) : this(new MacroElement[] { macro }) { }


        // PRESENTATION ================================================================================
        //--------------------------------------------------------------------------------
        public void BuildPresentation() {
            // Clear
            mChildren.Clear();

            // Build
            if (PrimaryElement.IsNode) {
                MacroNode node = (MacroNode)PrimaryElement;

                // Conglomeration
                List<MacroChainElement> elements = new List<MacroChainElement>();

                // Elements
                for (int i = 0; i < node.ElementCount; ++i) {
                    // Element
                    MacroElement element = node.Element(i);
                    elements.Add(new MacroChainElement(element));

                    // Mouse move event
                    if ((element is MacroMouseEvent) && ((MacroMouseEvent)element).IsMoveAction) {
                        foreach (MacroChainElement e in elements) {
                            e.chainType = ChainType.MouseMovement; // Mark all elements as part of a mouse move / delay chain
                        }
                    }

                    // Adding (when it's not a mouse move event or delay event)
                    if (!((element is MacroMouseEvent) && ((MacroMouseEvent)element).IsMoveAction) && !(element is MacroDelayEvent)) {
                        List<MacroElement> chainElements = new List<MacroElement>();
                        int j = 0;
                        while (j < elements.Count) {
                            // Chain
                            ChainType chainType = elements[j].chainType;
                            while ((j < elements.Count) && (elements[j].chainType == chainType)) {
                                chainElements.Add(elements[j].element);
                                ++j;
                            }

                            // Add node
                            mChildren.Add(new MacroPresentationNode(chainElements));
                            chainElements.Clear();
                        }

                        // Clear
                        elements.Clear();
                    }
                }

                // Remaining elements
                if (elements.Count > 0) {
                    // Mouse movement elements
                    List<MacroElement> chainElements = new List<MacroElement>();
                    foreach (MacroChainElement e in elements) {
                        if (e.chainType == ChainType.MouseMovement)
                            chainElements.Add(e.element);
                    }

                    // Add
                    mChildren.Add(new MacroPresentationNode(chainElements));
                }
            }
        }


        // ELEMENTS ================================================================================
        //--------------------------------------------------------------------------------
        public MacroElement PrimaryElement { get { return mElements.Last(); } }


        // CHILDREN ================================================================================
        //--------------------------------------------------------------------------------
        public int ChildCount { get { return mChildren.Count; } }
        public bool HasChildren { get { return (ChildCount > 0); } }
        public MacroPresentationNode[] Children { get { return mChildren.ToArray(); } }


        // APPEARANCE ================================================================================
        //--------------------------------------------------------------------------------
        public Color Colour { get { return PrimaryElement.Colour; } }


        // DESCRIPTION ================================================================================
        //--------------------------------------------------------------------------------
        public string Description { get { return PrimaryElement.Information[0]; } }
        public string TimeSpanDescription { get { return "-.-- ms  "; } }
        public string ParametersDescription { get { return PrimaryElement.Information[1]; } }


        //================================================================================
        //********************************************************************************
        public class MacroChainElement {
            public MacroElement element;
            public ChainType chainType;

            public MacroChainElement(MacroElement element, ChainType chainType = ChainType.Event) {
                this.element = element;
                this.chainType = chainType;
            }
        }
    }

}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDFScrape.Data;
using PDFScrape.PDF;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace PDFScrape.Macros {

    public class MacroBranchingNode : MacroNode {
        //================================================================================
        private Macro.SourceType                mSource;
        private string                          mExtractorName;
        private int                             mRepetitionIndex;
        private PDFExtractor.ColumnSpecifier    mColumn;
        
        private List<MacroBranchNode>           mBranches = new List<MacroBranchNode>();

        
        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroBranchingNode() : this(Macro.SourceType.EXTRACTOR, "", 0, new PDFExtractor.ColumnSpecifier()) { }

        //--------------------------------------------------------------------------------
        public MacroBranchingNode(Macro.SourceType source, string extractorName, int repetitionIndex, PDFExtractor.ColumnSpecifier column) {
            mSource = source;
            mExtractorName = extractorName;
            mRepetitionIndex = repetitionIndex;
            mColumn = column;
        }

        //--------------------------------------------------------------------------------
        public MacroBranchingNode(Macro.SourceType source, string extractorName, PDFExtractor.ColumnSpecifier column) :
        this(source, extractorName, 0, column) {

        }

        //--------------------------------------------------------------------------------
        public MacroBranchingNode(Macro.SourceType source, int repetitionIndex, PDFExtractor.ColumnSpecifier column) :
        this(source, "", repetitionIndex, column) {

        }


        // PLAYING ================================================================================
        //--------------------------------------------------------------------------------
        public override bool Play(MacroPlayer player) {
            // Notify
            player.NotifyPlayingElement(this);

            // Data
            string data = player.RetrieveScrapeData(mSource, mExtractorName, mRepetitionIndex, mColumn);

            // Branches
            foreach (MacroBranchNode b in mBranches) {
                if (b.MeetsCondition(data)) {
                    if (!b.Play(player))
                        return false;
                    if (b.Final)
                        break;
                }
            }

            // Return
            return true;
        }


        // SOURCE ================================================================================
        //--------------------------------------------------------------------------------
        public Macro.SourceType Source { get { return mSource; } }
        public string ExtractorName { get { return mExtractorName; } }
        public int RepetitionIndex { get { return mRepetitionIndex; } }
        public PDFExtractor.ColumnSpecifier Column { get { return mColumn; } }


        // SOURCE ================================================================================
        //--------------------------------------------------------------------------------
        private MacroBranchNode AddBranch(MacroBranchNode branchNode) {
            base.AddElement(branchNode);
            mBranches.Add(branchNode);
            return branchNode;
        }

        //--------------------------------------------------------------------------------
        public MacroBranchNode AddBranch(MacroBranchNode.ConditionType condition, string conditionArgument = "", bool final = false) {
            return AddBranch(new MacroBranchNode(condition, conditionArgument, final));
        }

        //--------------------------------------------------------------------------------
        protected void AddBranch(JToken jsonToken) {
            MacroBranchNode branchNode = new MacroBranchNode();
            branchNode.ReadJSON(jsonToken);
            AddBranch(branchNode);
        }
        
        //--------------------------------------------------------------------------------
        public void RemoveBranch(int index) {
            MacroBranchNode branchNode = Branch(index);
            RemoveElement(branchNode);
            mBranches.RemoveAt(index);
        }

        //--------------------------------------------------------------------------------
        public void RemoveBranch(MacroBranchNode branchNode) {
            // Find
            int branchIndex = -1;
            for (int i = 0; i < mBranches.Count; ++i) {
                if (mBranches[i] == branchNode) {
                    branchIndex = i;
                    break;
                }
            }

            if (branchIndex == -1)
                throw new NotFoundException();

            // Remove
            RemoveElement(branchNode);
            mBranches.RemoveAt(branchIndex);
        }

        //--------------------------------------------------------------------------------
        public void RemoveAllBranches() {
            // Nodes
            foreach (MacroBranchNode b in mBranches) {
                RemoveElement(b);
            }

            // Branches
            mBranches.Clear();
        }

        //--------------------------------------------------------------------------------
        public MacroBranchNode Branch(int index) { return mBranches[index]; }
        public int BranchCount { get { return mBranches.Count; } }
        public MacroBranchNode[] Branches { get { return mBranches.ToArray(); } }


        // PRESENTATION ================================================================================
        //--------------------------------------------------------------------------------
        public override Color Colour { get { return Color.FromArgb(255, 159, 95); } }
        public override OverlayType Overlay { get { return OverlayType.CENTRAL; } }
        public override string OverlayMessage { get { return "Began branching"; } }
        public override Color OverlayColour1 { get { return Color.FromArgb(255, 159, 95); } }
        public override Color OverlayColour2 { get { return Color.FromArgb(127, 31, 0); } }


        // INFORMATION ================================================================================
        //--------------------------------------------------------------------------------
        public override string[] Information {
            get {
                // Type
                string type = "Branching";

                // Source
                switch (mSource) {
                    case Macro.SourceType.EXTRACTOR:                            type += " (Extractor)"; break;
                    case Macro.SourceType.REPETITION_ROW:                       type += " (Repetition Row)"; break;
                    case Macro.SourceType.EXTRACTOR_ROW_AT_REPETITION_POSITION: type += " (Extractor Row At Repetition Position)"; break;
                }

                // Parameters
                string parameters = "";

                // Extractor
                if (mSource == Macro.SourceType.EXTRACTOR || mSource == Macro.SourceType.EXTRACTOR_ROW_AT_REPETITION_POSITION)
                    parameters += "extractor: " + ExtractorName;

                // Repetition
                if (mSource == Macro.SourceType.EXTRACTOR_ROW_AT_REPETITION_POSITION || mSource == Macro.SourceType.REPETITION_ROW)
                    parameters += (string.IsNullOrEmpty(parameters) ? "" : ", ") + "repetition index: " + RepetitionIndex;

                // Column
                parameters += ", column: " + mColumn.ToString();

                // Return
                return new string[] { type, parameters };
            }
        }


        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public override void WriteJSON(JsonTextWriter writer) {
            // Fields
            writer.WritePropertyName("source"); writer.WriteValue((int)mSource);
            writer.WritePropertyName("extractor_name"); writer.WriteValue(mExtractorName);
            writer.WritePropertyName("repetition_index"); writer.WriteValue(mRepetitionIndex);
            writer.WritePropertyName("column"); Column.WriteJSON(writer);

            // Elements
            writer.WritePropertyName("elements");
            writer.WriteStartArray();
            foreach (MacroElement e in mElements) {
                if (!mBranches.Contains(e)) {
                    writer.WriteStartObject();

                    writer.WritePropertyName("type");
                    writer.WriteValue(sElementTypes.FirstOrDefault(t => t.Value == e.GetType()).Key);
                    e.WriteJSON(writer);

                    writer.WriteEndObject();
                }
            }
            writer.WriteEndArray();

            // Branches
            writer.WritePropertyName("branches");
            writer.WriteStartArray();
            foreach (MacroBranchNode b in mBranches) {
                writer.WriteStartObject();
                b.WriteJSON(writer);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }

        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            // Fields
            mSource = (Macro.SourceType)((int)token.SelectToken("source"));
            mExtractorName = (string)token.SelectToken("extractor_name");
            mRepetitionIndex = (int)token.SelectToken("repetition_index");
            Column.ReadJSON(token.SelectToken("column"));
            
            // Elements
            JArray elements = (JArray)token.SelectToken("elements");
            if (elements != null) {
                foreach (JToken e in elements) {
                    string type = (string)e.SelectToken("type");
                    AddElement(type, e);
                }
            }

            // Branches
            JArray branches = (JArray)token.SelectToken("branches");
            if (branches != null) {
                foreach (JToken b in branches) {
                    AddBranch(b);
                }
            }
        }
    }

}

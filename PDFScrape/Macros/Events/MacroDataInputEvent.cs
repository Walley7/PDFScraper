using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDFScrape.Data;
using PDFScrape.PDF;
using Silence.Simulation.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace PDFScrape.Macros.Events {

    public class MacroDataInputEvent : MacroEvent {
        //================================================================================
        public const long                       INPUT_DELAY = TimeSpan.TicksPerSecond;


        //================================================================================
        private Macro.SourceType                mSource;
        private string                          mExtractorName;
        private int                             mRepetitionIndex;
        private PDFExtractor.ColumnSpecifier    mColumn;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroDataInputEvent() : this(Macro.SourceType.EXTRACTOR, "", 0, new PDFExtractor.ColumnSpecifier()) { }

        //--------------------------------------------------------------------------------
        public MacroDataInputEvent(Macro.SourceType source, string extractorName, int repetitionIndex, PDFExtractor.ColumnSpecifier column) {
            mSource = source;
            mExtractorName = extractorName;
            mRepetitionIndex = repetitionIndex;
            mColumn = column;
        }

        //--------------------------------------------------------------------------------
        public MacroDataInputEvent(Macro.SourceType source, string extractorName, PDFExtractor.ColumnSpecifier column) :
        this(source, extractorName, 0, column) {

        }

        //--------------------------------------------------------------------------------
        public MacroDataInputEvent(Macro.SourceType source, int repetitionIndex, PDFExtractor.ColumnSpecifier column) :
        this(source, "", repetitionIndex, column) {

        }


        // PLAYING ================================================================================
        //--------------------------------------------------------------------------------
        public override bool Play(MacroPlayer player) {
            // Notify
            player.NotifyPlayingElement(this);

            // Play
            string data = player.RetrieveScrapeData(mSource, mExtractorName, mRepetitionIndex, mColumn);
            if (data != null)
                PlayInput(player, data);

            // Return
            return true;
        }

        //--------------------------------------------------------------------------------
        public static void PlayInput(MacroPlayer player, string input) {
            foreach (char c in input) {
                // Delay
                long delay = (long)((double)INPUT_DELAY * (double)player.DataInputSpeed);
                Thread.Sleep(new TimeSpan(delay));

                // Play
                PlayInput(player, c);
            }
        }

        //--------------------------------------------------------------------------------
        public static void PlayInput(MacroPlayer player, char input) {
            // Virtual key / modifiers
            VirtualKeyStruct virtualKey = new VirtualKeyStruct { value = VkKeyScan(input) };
            bool shift = ((virtualKey.high & 1) != 0);

            // Play
            if (shift)
                player.KeyboardSimulator.KeyDown(VirtualKeyCode.SHIFT);
            player.KeyboardSimulator.KeyDown((VirtualKeyCode)virtualKey.low);
            player.KeyboardSimulator.KeyUp((VirtualKeyCode)virtualKey.low);
            if (shift)
                player.KeyboardSimulator.KeyUp(VirtualKeyCode.SHIFT);
        }

        //--------------------------------------------------------------------------------
        [DllImport("user32.dll")]static extern short VkKeyScan(char c);


        // SOURCE ================================================================================
        //--------------------------------------------------------------------------------
        public Macro.SourceType Source { get { return mSource; } }
        public string ExtractorName { get { return mExtractorName; } }
        public int RepetitionIndex { get { return mRepetitionIndex; } }
        public PDFExtractor.ColumnSpecifier Column { get { return mColumn; } }


        // PRESENTATION ================================================================================
        //--------------------------------------------------------------------------------
        public override Color Colour { get { return Color.FromArgb(159, 127, 255); } }
        public override OverlayType Overlay { get { return OverlayType.CENTRAL; } }
        public override string OverlayMessage { get { return "Entered data"; } }
        public override Color OverlayColour1 { get { return Color.FromArgb(159, 127, 255); } }
        public override Color OverlayColour2 { get { return Color.FromArgb(31, 0, 127); } }


        // INFORMATION ================================================================================
        //--------------------------------------------------------------------------------
        public override string[] Information {
            get {
                // Type
                string type = "Data Input";

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
            writer.WritePropertyName("source"); writer.WriteValue((int)mSource);
            writer.WritePropertyName("extractor_name"); writer.WriteValue(mExtractorName);
            writer.WritePropertyName("repetition_index"); writer.WriteValue(mRepetitionIndex);
            writer.WritePropertyName("column"); Column.WriteJSON(writer);
        }

        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            mSource = (Macro.SourceType)((int)token.SelectToken("source"));
            mExtractorName = (string)token.SelectToken("extractor_name");
            mRepetitionIndex = (int)token.SelectToken("repetition_index");
            Column.ReadJSON(token.SelectToken("column"));
        }


        //================================================================================
        //********************************************************************************
        [StructLayout(LayoutKind.Explicit)]
        struct VirtualKeyStruct {
            [FieldOffset(0)] public short value;
            [FieldOffset(0)] public byte low;
            [FieldOffset(1)] public byte high;
        }
    }

}

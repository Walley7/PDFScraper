using Silence.Simulation.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace PDFScrape.Macros.Events {

    class MacroDataInputEvent2 : MacroEvent2 {
        //================================================================================
        public const long                       DELAY = 10000;


        //================================================================================
        private string                          mInputField;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroDataInputEvent2(string inputField) {
            mInputField = inputField;
        }


        // INPUT FIELD ================================================================================
        //--------------------------------------------------------------------------------
        public string InputField { get { return mInputField; } }


        // PLAYING ================================================================================
        //--------------------------------------------------------------------------------
        public override void Play(MacroPlayer2 player) {
            // Input
            if ((player.DataStoreRecord != null) && player.DataStoreRecord.HasValue(mInputField)) {
                string value = player.DataStoreRecord.Value(mInputField);
                PlayInput(player, value);
            }
        }
        
        //--------------------------------------------------------------------------------
        private void PlayInput(MacroPlayer2 player, string input) {
            Console.Out.WriteLine("PlayInput: " + input);
            foreach (char c in input) {
                PlayInput(player, c);
            }
        }
        
        //--------------------------------------------------------------------------------
        private void PlayInput(MacroPlayer2 player, char input) {
            // Virtual key
            VirtualKeyStruct virtualKey = new VirtualKeyStruct { value = VkKeyScan(input) };

            // Modifiers
            bool shift = ((virtualKey.high & 1) != 0);

            // Delay
            Thread.Sleep(new TimeSpan(DELAY));

            // Play
            if (shift)
                player.KeyboardSimulator.KeyDown(VirtualKeyCode.SHIFT);
            player.KeyboardSimulator.KeyDown((VirtualKeyCode)virtualKey.low);
            player.KeyboardSimulator.KeyUp((VirtualKeyCode)virtualKey.low);
            if (shift)
                player.KeyboardSimulator.KeyUp(VirtualKeyCode.SHIFT);
        }


        //================================================================================
        //********************************************************************************
        [DllImport("user32.dll")]static extern short VkKeyScan(char ch);
        
        //********************************************************************************
        [StructLayout(LayoutKind.Explicit)]
        struct VirtualKeyStruct {
            [FieldOffset(0)] public short value;
            [FieldOffset(0)] public byte low;
            [FieldOffset(1)] public byte high;
        }
    }

}

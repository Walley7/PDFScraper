using Silence.Hooking;
using Silence.Hooking.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Hotkeys {

    class HotkeyHooker {
        //================================================================================
        // Possible alternative to registrator, with purpose being to be able to set a
        // hotkey to not fire until the keys are released.
        //================================================================================
        private HookManager                     mHookManager = new HookManager();


        //================================================================================
        //--------------------------------------------------------------------------------
        public HotkeyHooker() {
            // Hooking
            mHookManager.KeyUp += OnHookKeyUp;
            mHookManager.KeyDown += OnHookKeyDown;
        }

        //--------------------------------------------------------------------------------
        public void Dispose() {
            mHookManager.Dispose();
        }
        
            
        // EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        private void OnHookKeyUp(object sender, GlobalKeyEventHandlerArgs e) {

        }

        //--------------------------------------------------------------------------------
        private void OnHookKeyDown(object sender, GlobalKeyEventHandlerArgs e) {

        }


        //================================================================================
        //********************************************************************************
        private class HotkeyRegistration {
            //VirtualKeyCode keys[];
            //bool pressed[];
        }
    }

}

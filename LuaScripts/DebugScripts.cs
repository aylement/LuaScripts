using GTA;
using System;

namespace ControllerExample
{
    // Debug helpers for development
    public class DebugScripts : ScriptBase
    {
        public DebugScripts()
        {
            Tick += OnTick;
            Interval = 2000;
        }

        private void OnTick(object sender, EventArgs e)
        {
            var fps = Game.FPS;
            Notify("FPS: " + fps);
        }
    }
}

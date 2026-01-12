using GTA;
using GTA.Native;
using System;

namespace ControllerExample
{
    // Time manipulation helpers
    public class TimeScripts : ScriptBase
    {
        readonly uint speedUpTimeHash = NativeMemory.GetHashKey("TIMEFAST");
        readonly uint slowTimeHash = NativeMemory.GetHashKey("TIMESLOW");
        readonly uint freezeTimeHash = NativeMemory.GetHashKey("TIMEFRZ");

        private bool freezeTime = false;

        public TimeScripts()
        {
            Tick += OnTick;
            Interval = 500;
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (CheatActivated(speedUpTimeHash))
            {
                Function.Call(Hash.SET_TIME_SCALE, 2.0f);
                Notify("Time speed x2 for a moment");
            }

            if (CheatActivated(slowTimeHash))
            {
                Function.Call(Hash.SET_TIME_SCALE, 0.5f);
                Notify("Time slowed for a moment");
            }

            if (CheatActivated(freezeTimeHash))
            {
                freezeTime = !freezeTime;
                Function.Call(Hash.PAUSE_CLOCK, freezeTime);
                Notify("Time freeze: " + (freezeTime ? "ON" : "OFF"));
            }
        }
    }
}

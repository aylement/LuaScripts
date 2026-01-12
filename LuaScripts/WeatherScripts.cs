using GTA;
using GTA.Native;
using System;
using SHVDN;

namespace ControllerExample
{
    // Weather and time scripts
    public class WeatherScripts : ScriptBase
    {
        readonly uint clearWeatherHash = NativeMemory.GetHashKey("CLEARWD");
        readonly uint stormWeatherHash = NativeMemory.GetHashKey("STORMIT");
        readonly uint nightTimeHash = NativeMemory.GetHashKey("NIGHTS");

        public WeatherScripts()
        {
            Tick += OnTick;
            Interval = 500;
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (CheatActivated(clearWeatherHash))
            {
                World.Weather = Weather.Clear;
                Notify("Weather: Clear");
            }

            if (CheatActivated(stormWeatherHash))
            {
                World.Weather = Weather.ThunderStorm;
                Notify("Weather: Storm");
            }

            if (CheatActivated(nightTimeHash))
            {
                Function.Call(Hash.SET_CLOCK_TIME, 2, 0, 0);
                Notify("Time set to night");
            }
        }
    }
}

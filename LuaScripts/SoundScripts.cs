using GTA;
using GTA.Native;
using System;
using SHVDN;

namespace ControllerExample
{
    // Play simple sounds and control audio
    public class SoundScripts : ScriptBase
    {
        readonly uint hornHash = NativeMemory.GetHashKey("CARHORN");
        readonly uint radioOffHash = NativeMemory.GetHashKey("RADOFF");

        public SoundScripts()
        {
            Tick += OnTick;
            Interval = 400;
        }

        private void OnTick(object sender, EventArgs e)
        {
            var player = Game.Player.Character;
            if (CheatActivated(hornHash))
            {
                if (player.IsInVehicle())
                {
                    Notify("Honk (placeholder)");
                }
            }

            if (CheatActivated(radioOffHash))
            {
                if (player.IsInVehicle())
                {
                    Notify("Radio turned off (placeholder)");
                }
            }
        }
    }
}

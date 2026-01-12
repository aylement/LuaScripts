using GTA;
using System;
using SHVDN;

namespace ControllerExample
{
    // Simple mission helper utilities
    public class MissionHelperScripts : ScriptBase
    {
        readonly uint startMissionHash = NativeMemory.GetHashKey("STARTMI");
        readonly uint completeMissionHash = NativeMemory.GetHashKey("COMPMI");
        readonly uint resetWantedHash = NativeMemory.GetHashKey("RESPOL");

        public MissionHelperScripts()
        {
            Tick += OnTick;
            Interval = 400;
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (CheatActivated(startMissionHash))
            {
                var player = Game.Player.Character;
                var pos = player.Position + player.ForwardVector * 20f;
                Notify("Mission started (placeholder)");
            }

            if (CheatActivated(completeMissionHash))
            {
                Notify("Mission marked complete (placeholder)");
            }

            if (CheatActivated(resetWantedHash))
            {
                Game.Player.WantedLevel = 0;
                Notify("Wanted level cleared");
            }
        }
    }
}

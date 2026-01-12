using GTA;
using GTA.Native;
using System;
using SHVDN;

namespace ControllerExample
{
    // Misc utilities
    public class UtilityScripts : ScriptBase
    {
        readonly uint healHash = NativeMemory.GetHashKey("HEALME");
        readonly uint armorHash = NativeMemory.GetHashKey("ARMORUP");
        readonly uint slowFallHash = NativeMemory.GetHashKey("SLOWFALL");

        private bool slowFall = false;

        public UtilityScripts()
        {
            Tick += OnTick;
            Interval = 200;
        }

        private void OnTick(object sender, EventArgs e)
        {
            var ped = Game.Player.Character;

            if (CheatActivated(healHash))
            {
                ped.Health = ped.MaxHealth;
                Notify("Healed to full");
            }

            if (CheatActivated(armorHash))
            {
                ped.Armor = 100;
                Notify("Armor set to 100");
            }

            if (CheatActivated(slowFallHash))
            {
                slowFall = !slowFall;
                Notify("Slow fall: " + (slowFall ? "ON" : "OFF"));
            }

            if (slowFall && ped.IsFalling)
            {
                ped.Velocity = ped.Velocity * 0.2f;
            }
        }
    }
}

using GTA;
using GTA.Native;
using System;
using SHVDN;

namespace ControllerExample
{
    // Player state modifiers
    public class PlayerStateScripts : ScriptBase
    {
        readonly uint stealthHash = NativeMemory.GetHashKey("STEALTHY");
        readonly uint sprintHash = NativeMemory.GetHashKey("SPRNTUP");

        private bool stealth = false;

        public PlayerStateScripts()
        {
            Tick += OnTick;
            Interval = 200;
        }

        private void OnTick(object sender, EventArgs e)
        {
            var player = Game.Player.Character;

            if (CheatActivated(stealthHash))
            {
                stealth = !stealth;
                Function.Call(Hash.SET_PED_STEALTH_MOVEMENT, player.Handle, stealth, 0);
                Notify("Stealth mode: " + (stealth ? "ON" : "OFF"));
            }

            if (CheatActivated(sprintHash))
            {
                Function.Call(Hash.SET_RUN_SPRINT_MULTIPLIER_FOR_PLAYER, Function.Call<int>(Hash.PLAYER_ID), 1.5f);
                Notify("Sprint boost (one shot)");
            }
        }
    }
}

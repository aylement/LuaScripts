using GTA;
using GTA.Native;
using System;
using SHVDN;

namespace ControllerExample
{
    // Spawning items and pickups (safe placeholders)
    public class SpawnAndItemScripts : ScriptBase
    {
        readonly uint spawnPickupHash = NativeMemory.GetHashKey("SPAWNPICK");
        readonly uint giveMoneyHash = NativeMemory.GetHashKey("GIVEMNY");

        public SpawnAndItemScripts()
        {
            Tick += OnTick;
            Interval = 400;
        }

        private void OnTick(object sender, EventArgs e)
        {
            var player = Game.Player.Character;

            if (CheatActivated(spawnPickupHash))
            {
                var pos = player.Position + player.ForwardVector * 2f;
                try
                {
                    Function.Call(Hash.CREATE_OBJECT, "prop_beach_ball_02", pos.X, pos.Y, pos.Z, true, true, false);
                    Notify("Pickup (prop) spawned");
                }
                catch { }
            }

            if (CheatActivated(giveMoneyHash))
            {
                Notify("Given $500 (placeholder)");
            }
        }
    }
}

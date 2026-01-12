using GTA;
using GTA.Native;
using System;
using SHVDN;

namespace ControllerExample
{
    // NPC spawning and simple NPC utilities
    public class NPCScripts : ScriptBase
    {
        readonly uint spawnCopHash = NativeMemory.GetHashKey("SPAWNCOP");
        readonly uint spawnPedHash = NativeMemory.GetHashKey("SPAWNPED");
        readonly uint friendlyPedsHash = NativeMemory.GetHashKey("FRIENDLY");

        private bool friendlyPeds = false;

        public NPCScripts()
        {
            Tick += OnTick;
            Interval = 300;
        }

        private void OnTick(object sender, EventArgs e)
        {
            var player = Game.Player.Character;

            if (CheatActivated(spawnCopHash))
            {
                if (RequestModel("s_m_y_cop_01", out var copModel))
                {
                    var pos = player.Position + player.ForwardVector * 5f;
                    var ped = World.CreatePed(copModel, pos);
                    ped.Weapons.Give(WeaponHash.Pistol, 120, true, true);
                    ped.IsPersistent = true;
                    Notify("Spawned cop");
                    ReleaseModel(copModel);
                }
            }

            if (CheatActivated(spawnPedHash))
            {
                if (RequestModel("a_f_m_beach_01", out var pedModel))
                {
                    var pos = player.Position + player.ForwardVector * 3f;
                    var ped = World.CreatePed(pedModel, pos);
                    ped.Task.WanderAround();
                    ped.IsPersistent = true;
                    Notify("Spawned ped");
                    ReleaseModel(pedModel);
                }
            }

            if (CheatActivated(friendlyPedsHash))
            {
                friendlyPeds = !friendlyPeds;
                Notify("Peds friendly: " + (friendlyPeds ? "ON" : "OFF"));
            }

            if (friendlyPeds)
            {
                var allPeds = World.GetAllPeds();
                foreach (var p in allPeds)
                {
                    if (!p.IsPlayer && p.IsAlive)
                    {
                        p.Task.ClearAll();
                        p.BlockPermanentEvents = true;
                        p.IsPersistent = true;
                    }
                }
            }
        }
    }
}

using GTA;
using GTA.Math;
using System;
using GTA.Native;
using SHVDN;

namespace ControllerExample
{
    // Collection of simple scripts for single-player legacy (cheat-activated)
    public class BasicScripts : ScriptBase
    {
        readonly uint spawnHash = NativeMemory.GetHashKey("SPAWNADD");
        readonly uint giveHash = NativeMemory.GetHashKey("GIVEAR");
        readonly uint godHash = NativeMemory.GetHashKey("GODTOG");
        readonly uint unloadHash = NativeMemory.GetHashKey("UNLOAD");

        private bool godMode = false;

        public BasicScripts()
        {
            Interval = 100; // 10 ticks per second
            Tick += OnTick;
        }

        private void OnTick(object sender, EventArgs e)
        {
            var player = Game.Player.Character;

            if (!player.IsInVehicle() && player.Health < player.MaxHealth)
            {
                player.Health = Math.Min(player.MaxHealth, player.Health + 1);
            }

            player.IsInvincible = godMode;

            if (CheatActivated(spawnHash))
            {
                SpawnVehicle("adder");
            }

            if (CheatActivated(giveHash))
            {
                GiveWeapon(WeaponHash.AssaultRifle, 250);
            }

            if (CheatActivated(godHash))
            {
                godMode = !godMode;
                Notify("God Mode: " + (godMode ? "ON" : "OFF"));
            }

            if (CheatActivated(unloadHash))
            {
                Core.RuntimeController.RequestUnload(Core.CurrentDirectory);
            }
        }

        private void SpawnVehicle(string modelName)
        {
            if (!RequestModel(modelName, out var model))
            {
                Notify("Failed to load model: " + modelName);
                return;
            }

            Vector3 pos = Game.Player.Character.Position + Game.Player.Character.ForwardVector * 5f;
            Vehicle veh = World.CreateVehicle(model, pos);
            if (veh != null)
            {
                veh.PlaceOnGround();
                Game.Player.Character.SetIntoVehicle(veh, VehicleSeat.Driver);
                Notify("Spawned: " + modelName);
            }
            ReleaseModel(model);
        }

        private void GiveWeapon(WeaponHash weapon, int ammo)
        {
            var ped = Game.Player.Character;
            if (!ped.Weapons.HasWeapon(weapon))
                ped.Weapons.Give(weapon, ammo, true, true);
            else
                ped.Weapons[weapon].Ammo = ammo;

            Notify("Given weapon: " + weapon);
        }
    }
}

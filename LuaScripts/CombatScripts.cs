using GTA;
using GTA.Native;
using System;
using SHVDN;

namespace ControllerExample
{
    // Combat related simple cheats and behaviors
    public class CombatScripts : ScriptBase
    {
        readonly uint explosiveAmmoHash = NativeMemory.GetHashKey("EXPAMS"); // example cheat name
        readonly uint infiniteAmmoHash = NativeMemory.GetHashKey("INFINAM");
        readonly uint copsOffHash = NativeMemory.GetHashKey("NOPOLICE");

        private bool infiniteAmmo = false;

        public CombatScripts()
        {
            Tick += OnTick;
            Interval = 100;
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (CheatActivated(explosiveAmmoHash))
            {
                MakeExplosiveAmmo();
                Notify("Explosive ammo toggled (one-shot)");
            }

            if (CheatActivated(infiniteAmmoHash))
            {
                infiniteAmmo = !infiniteAmmo;
                Notify("Infinite ammo: " + (infiniteAmmo ? "ON" : "OFF"));
            }

            if (infiniteAmmo)
            {
                var ped = Game.Player.Character;
                TrySetAmmo(ped, WeaponHash.Pistol, 9999);
                TrySetAmmo(ped, WeaponHash.AssaultRifle, 9999);
                TrySetAmmo(ped, WeaponHash.SMG, 9999);
                TrySetAmmo(ped, WeaponHash.PumpShotgun, 9999);
                TrySetAmmo(ped, WeaponHash.SniperRifle, 9999);
            }

            if (CheatActivated(copsOffHash))
            {
                Game.Player.WantedLevel = 0;
                Game.Player.CanControlCharacter = true;
                Notify("Police cleared");
            }
        }

        private void TrySetAmmo(Ped ped, WeaponHash hash, int ammo)
        {
            if (ped.Weapons.HasWeapon(hash))
            {
                ped.Weapons[hash].Ammo = ammo;
            }
        }

        private void MakeExplosiveAmmo()
        {
            var ped = Game.Player.Character;
            ped.Weapons.Give(WeaponHash.RPG, 10, true, true);
        }
    }
}

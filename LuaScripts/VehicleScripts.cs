using GTA;
using GTA.Math;
using System;
using SHVDN;

namespace ControllerExample
{
    // Vehicle related utilities and cheats
    public class VehicleScripts : ScriptBase
    {
        readonly uint lockCarHash = NativeMemory.GetHashKey("LOCKCAR");
        readonly uint flipCarHash = NativeMemory.GetHashKey("FLIPCAR");
        readonly uint fixCarHash = NativeMemory.GetHashKey("FIXCAR");

        public VehicleScripts()
        {
            Tick += OnTick;
            Interval = 200;
        }

        private void OnTick(object sender, EventArgs e)
        {
            var ped = Game.Player.Character;
            if (CheatActivated(lockCarHash))
            {
                if (ped.IsInVehicle())
                {
                    var veh = ped.CurrentVehicle;
                    veh.LockStatus = VehicleLockStatus.CannotBeTriedToEnter;
                    Notify("Vehicle locked");
                }
            }

            if (CheatActivated(flipCarHash))
            {
                if (ped.IsInVehicle())
                {
                    var veh = ped.CurrentVehicle;
                    veh.PlaceOnGround();
                    Notify("Vehicle flipped upright");
                }
            }

            if (CheatActivated(fixCarHash))
            {
                if (ped.IsInVehicle())
                {
                    var veh = ped.CurrentVehicle;
                    veh.Repair();
                    Notify("Vehicle repaired");
                }
            }
        }
    }
}

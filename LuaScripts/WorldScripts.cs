using GTA;
using GTA.Native;
using System;
using GTA.Math;
using SHVDN;

namespace ControllerExample
{
    // World and environment scripts
    public class WorldScripts : ScriptBase
    {
        readonly uint slowTimeHash = NativeMemory.GetHashKey("SLOWTIME");
        readonly uint noTrafficHash = NativeMemory.GetHashKey("NOTRAF");
        readonly uint freezePedHash = NativeMemory.GetHashKey("FREEZEP");

        private bool slowTime = false;
        private bool noTraffic = false;
        private Vector3? frozenPos = null;

        public WorldScripts()
        {
            Tick += OnTick;
            Interval = 250;
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (CheatActivated(slowTimeHash))
            {
                slowTime = !slowTime;
                Notify("Slow time: " + (slowTime ? "ON" : "OFF"));
            }

            if (CheatActivated(noTrafficHash))
            {
                noTraffic = !noTraffic;
                Notify("Traffic disabled: " + (noTraffic ? "ON" : "OFF"));
            }

            if (slowTime)
            {
                Function.Call(Hash.SET_TIME_SCALE, 0.3f);
            }
            else
            {
                Function.Call(Hash.SET_TIME_SCALE, 1.0f);
            }

            if (noTraffic)
            {
                // Remove nearby vehicles to reduce traffic, safer than removing all
                var vehicles = World.GetAllVehicles();
                foreach (var v in vehicles)
                {
                    if (v.Position.DistanceTo(Game.Player.Character.Position) < 200f)
                        v.Delete();
                }
            }

            if (CheatActivated(freezePedHash))
            {
                var ped = Game.Player.Character;
                if (!frozenPos.HasValue)
                {
                    frozenPos = ped.Position;
                    Function.Call(Hash.CLEAR_PED_TASKS_IMMEDIATELY, ped.Handle);
                    Notify("Player frozen: YES");
                }
                else
                {
                    frozenPos = null;
                    Notify("Player frozen: NO");
                }
            }

            // Keep player at frozen position if set
            if (frozenPos.HasValue)
            {
                var p = Game.Player.Character;
                p.Position = frozenPos.Value;
                p.Velocity = new GTA.Math.Vector3(0, 0, 0);
            }
        }
    }
}

using GTA;
using GTA.Native;
using System;
using SHVDN;

namespace ControllerExample
{
    // Simple AI behavior toggles
    public class AIBehaviorScripts : ScriptBase
    {
        readonly uint aggressivePedsHash = NativeMemory.GetHashKey("AGGROPED");
        readonly uint passiveTrafficHash = NativeMemory.GetHashKey("PASSIVE");

        private bool aggressive = false;
        private bool passiveTraffic = false;

        public AIBehaviorScripts()
        {
            Tick += OnTick;
            Interval = 400;
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (CheatActivated(aggressivePedsHash))
            {
                aggressive = !aggressive;
                Notify("Aggressive peds: " + (aggressive ? "ON" : "OFF"));
            }

            if (CheatActivated(passiveTrafficHash))
            {
                passiveTraffic = !passiveTraffic;
                Notify("Passive traffic: " + (passiveTraffic ? "ON" : "OFF"));
            }

            if (aggressive)
            {
                var peds = World.GetAllPeds();
                foreach (var p in peds)
                {
                    if (!p.IsPlayer && p.IsAlive)
                    {
                        p.Task.FightAgainst(Game.Player.Character);
                    }
                }
            }

            if (passiveTraffic)
            {
                var vehicles = World.GetAllVehicles();
                foreach (var v in vehicles)
                {
                    // set very low speed to simulate passive traffic
                    v.Speed = 0.1f;
                }
            }
        }
    }
}

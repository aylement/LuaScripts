using GTA;
using GTA.Native;
using System;
using SHVDN;
using static GTA.Native.NativeInvoker;

namespace ControllerExample
{
    // Fun and silly scripts
    public class FunScripts : ScriptBase
    {
        readonly uint fireworksHash = NativeMemory.GetHashKey("FIREWORKS");
        readonly uint superJumpHash = NativeMemory.GetHashKey("SUPERJP");
        readonly uint lowGravityHash = NativeMemory.GetHashKey("LOWGRAV");

        private bool lowGravity = false;

        public FunScripts()
        {
            Tick += OnTick;
            Interval = 300;
        }

        private void OnTick(object sender, EventArgs e)
        {
            var player = Game.Player.Character;

            if (CheatActivated(fireworksHash))
            {
                for (int i = 0; i < 6; i++)
                {
                    var pos = player.Position + player.ForwardVector * (2f + i);
                    World.AddExplosion(pos, ExplosionType.Grenade, 1f, 0f);
                }
                Notify("Fireworks!");
            }

            if (CheatActivated(superJumpHash))
            {
                // small upward impulse
                player.ApplyForce(new GTA.Math.Vector3(0, 0, 0.2f));
                Notify("Super jump activated (one shot)");
            }

            if (CheatActivated(lowGravityHash))
            {
                lowGravity = !lowGravity;
                Function.Call(Hash.SET_GRAVITY_LEVEL, lowGravity ? 0 : 1);
                Notify("Low gravity: " + (lowGravity ? "ON" : "OFF"));
            }
        }
    }
}

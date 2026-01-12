using GTA;
using GTA.Native;
using System;
using SHVDN;

namespace ControllerExample
{
    // Camera manipulation helpers
    public class CameraScripts : ScriptBase
    {
        readonly uint cinematicHash = NativeMemory.GetHashKey("CINEMAC");
        readonly uint resetCamHash = NativeMemory.GetHashKey("RESETCM");

        private Camera activeCam = null;

        public CameraScripts()
        {
            Tick += OnTick;
            Interval = 200;
        }

        private void OnTick(object sender, EventArgs e)
        {
            var player = Game.Player.Character;

            if (CheatActivated(cinematicHash))
            {
                if (activeCam == null)
                {
                    activeCam = World.CreateCamera(player.Position + new GTA.Math.Vector3(0, 2, 1), player.Rotation, 50f);
                    activeCam.PointAt(player);
                    Function.Call(Hash.SET_CAM_ACTIVE, activeCam.Handle, true);
                    Function.Call(Hash.RENDER_SCRIPT_CAMS, true, false, 3000, true, false);
                    Notify("Cinematic camera on");
                }
                else
                {
                    Function.Call(Hash.RENDER_SCRIPT_CAMS, false, false, 3000, true, false);
                    Function.Call(Hash.SET_CAM_ACTIVE, activeCam.Handle, false);
                    activeCam = null;
                    Notify("Cinematic camera off");
                }
            }

            if (CheatActivated(resetCamHash))
            {
                if (activeCam != null)
                {
                    Function.Call(Hash.RENDER_SCRIPT_CAMS, false, false, 3000, true, false);
                    Function.Call(Hash.SET_CAM_ACTIVE, activeCam.Handle, false);
                    activeCam = null;
                }
                Notify("Camera reset");
            }
        }
    }
}

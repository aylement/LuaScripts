using GTA;
using static GTA.Native.NativeInvoker;

namespace ControllerExample
{
    // Common base for scripts providing helpers for cheats, notifications and model requests
    public abstract class ScriptBase : Script
    {
        protected bool CheatActivated(uint hash) => HAS_PC_CHEAT_WITH_HASH_BEEN_ACTIVATED(hash);

        protected void Notify(string text)
        {
            try { NotificationHelper.Show(text); } catch { }
        }

        protected bool RequestModel(string name, out Model model, int timeout = 500)
        {
            model = new Model(name);
            try
            {
                if (!model.IsInCdImage)
                    return false;
                return model.Request(timeout);
            }
            catch
            {
                return false;
            }
        }

        protected void ReleaseModel(Model model)
        {
            try { model.MarkAsNoLongerNeeded(); } catch { }
        }
    }
}

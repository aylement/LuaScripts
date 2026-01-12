using GTA;
using System.Drawing;

namespace ControllerExample
{
    public static class NotificationHelper
    {
        public static void Show(string text)
        {
            var txt = new GTA.UI.TextElement(text, new PointF(200, 200), 0.7f);
            txt.Color = System.Drawing.Color.White;
            txt.Draw();
        }
    }
}

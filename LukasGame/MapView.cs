using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Gui;

namespace LukasGame {
    class MapView : View {
        public Map Map;
        public MapCoord CameraPos;

        public override void OnDrawContent(Rect viewport) {
            for (int i = 0; i < viewport.Width; i++) {
                for (int j = 0; j < viewport.Height; j++) {
                    var fg = (i + j) % 2 == 0 ? Color.Green : Color.Red;
                    var bg = Color.Black;
                    AddRuneWithColor(i, j, new Rune('.'), fg, bg);
                }
            }
        }


        public void AddRuneWithColor(int row, int col, Rune rune, Color? fg, Color? bg) {
            var curAttribute = Driver.GetAttribute();
            var newAttribute = Driver.MakeAttribute(fg ?? curAttribute.Foreground, bg ?? curAttribute.Background);
            Driver.SetAttribute(newAttribute);
            AddRune(col, row, rune);
            Driver.SetAttribute(curAttribute);
        }
    }
}

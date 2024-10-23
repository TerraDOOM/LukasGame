using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Gui;

namespace LukasGame {
    class AbyssTile : MapTile {
        static Random rng = new Random();

        static Color[] colors = { Color.Red, Color.BrightYellow, Color.Yellow, Color.Green, Color.Blue, Color.Magenta, Color.BrightMagenta };

        public override Entity? Entity { get => null; set { } }
        public AbyssTile() {
            Passable = true;
            Transparent = true;
        }

        public override Glyph GetGlyph() {
            return new Glyph { c = '*', fg = colors[rng.Next(colors.Length)], bg = Color.Black };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Gui;

namespace LukasGame {
    class MapTile {
        public List<Item> Items = new List<Item>();
        public virtual Entity? Entity { get; set; }

        public bool Transparent = true;
        public bool Passable = true;

        public MapTile() {}

        public virtual Glyph GetGlyph() {
            if (Entity != null) {
                return Entity.Glyph;
            }

            var c = Passable ? '.' : '#';
            var fg = Color.Gray;

            if (Items.Count > 0) {
                c = '(';
                fg = Color.BrightGreen;
            }

            return new Glyph {
                c = c,
                fg = fg,
                bg = Color.Black,
            };
        }

        public static MapTile Ground() => new MapTile();
        public static MapTile Wall() => new MapTile { Transparent = false, Passable = false };
    }
}

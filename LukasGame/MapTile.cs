using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Gui;

namespace LukasGame {
    class MapTile {
        public List<Item> Items = new List<Item>();
        public Entity Entity = null;

        public bool Transparent = true;
        public bool Passable = true;

        public MapTile() {}

        public Glyph GetGlyph() {
            if (Entity != null) {
                return Entity.Glyph;
            }

            var c = Passable ? '.' : '#';
            return new Glyph {
                c = c,
                fg = Color.Gray,
                bg = Color.Black,
            };
        }

        public static MapTile Ground() => new MapTile();
        public static MapTile Wall() => new MapTile { Transparent = false, Passable = false };
    }
}

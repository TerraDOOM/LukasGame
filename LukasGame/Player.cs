using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Gui;

namespace LukasGame {
    class Player : Entity {
        public override Glyph Glyph => new Glyph { c = '@', fg = Color.White, bg = Color.Black };
    }
}

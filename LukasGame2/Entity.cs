using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukasGame {
    abstract class Entity {
        bool visible;
        public abstract Glyph Glyph { get; }
        // Position in the current Map
        public Point Position;
    }
}

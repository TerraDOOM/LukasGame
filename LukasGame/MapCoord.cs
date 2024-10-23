using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukasGame {
    struct MapCoord {
        public static readonly MapCoord ZERO = new MapCoord(0, 0);

        public int x, y;

        public MapCoord(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public static MapCoord operator +(MapCoord a, MapCoord b) {
            return new MapCoord { x = a.x + b.x, y = a.y + b.y };
        }

        public static MapCoord operator -(MapCoord a, MapCoord b) {
            return new MapCoord { x = a.x - b.x, y = a.y - b.y };
        }
    }
}

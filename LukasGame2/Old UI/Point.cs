using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukasGame.Old {
    struct Point {
        public static readonly Point ZERO = new Point(0, 0);

        public int x, y;

        public Point(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public static Point operator+(Point a, Point b) {
            return new Point { x = a.x + b.x, y = a.y + b.y };
        }

        public static Point operator-(Point a, Point b) {
            return new Point { x = a.x - b.x, y = a.y - b.y };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukasGame {
    record struct Point(int X, int Y) {
        public static readonly Point ZERO = new Point(0, 0);

        public readonly double Magnitude() {
            return Math.Sqrt(X * X + Y * Y);
        }

        public static Point operator +(Point a, Point b) {
            return new(a.X + b.X, a.Y + b.Y);
        }

        public static Point operator -(Point a, Point b) {
            return new(a.X - b.X, a.Y - b.Y);
        }

        public static implicit operator Point((int, int) p)
            => new Point(p.Item1, p.Item2);
    }
}

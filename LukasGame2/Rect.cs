using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using LukasGame;

namespace LukasGame {
    record struct Rect(int X, int Y, int W, int H) {

        public Rect(Point pos, int w, int h) : this(pos.X, pos.Y, w, h) {}

        public int X { get; set; } = X;
        public int Y { get; set; } = Y;

        public Point Pos { readonly get => new(X, Y); set { X = value.X; Y = value.Y; } }

        public int W { get; set; } = W;
        public int H { get; set; } = H;

        public static explicit operator System.Drawing.Rectangle(Rect rect) {
            return new System.Drawing.Rectangle(rect.X, rect.Y, rect.W, rect.H);
        }

        public static explicit operator Rect(System.Drawing.Rectangle rect) {
            return new Rect(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public IEnumerable<Point> Points() {
            for (int i = X; i < X + W; i++) {
                for (int j = Y; j < Y + H; j++) {
                    yield return new Point(i, j);
                }
            }
        }

        public bool Overlaps(Rect other) {
            return XOverlap(this, other) && YOverlap(this, other);
        }

        bool XOverlap(Rect a, Rect b) {
            if (a.X > b.X) {
                return a.X - b.X < b.W;
            } else {
                return b.X - a.X < a.W;
            }
        }

        bool YOverlap(Rect a, Rect b) {
            if (a.Y > b.Y) {
                return a.Y - b.Y < b.H;
            } else {
                return b.Y - a.Y < a.H;
            }
        }
    }
}

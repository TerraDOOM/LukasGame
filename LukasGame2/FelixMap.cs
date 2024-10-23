using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LukasGame {
    class FelixMap : Map {
        const int MAP_SIZE = 100;

        public FelixMap() : base(MAP_SIZE, MAP_SIZE) { }

        public override void Generate() {
            foreach (var point in Rect.Points()) {
                this[point] = MapTile.Wall();
            }

            var rng = new Random();

            Rect[] rooms = new Rect[10];
            for (int i = 0; i < 10; i++) {
                int w = rng.Next(9, 15);
                int h = rng.Next(4, 7);

                var x = rng.Next(MAP_SIZE - w);
                var y = rng.Next(MAP_SIZE - h);

                var rect = new Rect(x, y, w, h);

                foreach (var point in rect.Points()) {
                    this[point] = MapTile.Ground();
                }

                rooms[i] = rect;
            };

            for (int i = 0; i < 10; i++) {
                var a = rooms[i];
                var b = rooms[(i + 1) % 10];

                if (a.Overlaps(b)) {
                    continue;
                }

                var pa = PerimeterPoint(a, rng);
                var pb = PerimeterPoint(b, rng);

                DrawLine(pa.X, pa.Y, pb.X, pb.Y);
            }
        }

        void DrawLine(int x0, int y0, int x1, int y1) {
            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2, e2;
            for (; ; ) {
                this[(x0, y0)] = MapTile.Ground();
                this[(x0 + 1, y0)] = MapTile.Ground();
                this[(x0, y0 + 1)] = MapTile.Ground();
                this[(x0 - 1, y0)] = MapTile.Ground();
                this[(x0, y0 - 1)] = MapTile.Ground();
                if (x0 == x1 && y0 == y1) break;
                e2 = err;
                if (e2 > -dx) { err -= dy; x0 += sx; }
                if (e2 < dy) { err += dx; y0 += sy; }
            }
        }

        Point PerimeterPoint(Rect room, Random rng) {
            var side = rng.Next(4);

            int x, y;

            Func<int> randomX = () => rng.Next(room.X, room.X + room.W);
            Func<int> randomY = () => rng.Next(room.Y, room.Y + room.H);

            switch (side) {
                case 0:
                    y = room.Y;
                    x = randomX();
                    break;
                case 1:
                    y = randomY();
                    x = room.X + room.W - 1;
                    break;
                case 2:
                    x = randomX();
                    y = room.Y + room.H - 1;
                    break;
                case 3:
                    y = randomY();
                    x = room.X;
                    break;
                default:
                    throw new Exception("wtf");
            }
            return new Point(x, y);
        }
    }
}

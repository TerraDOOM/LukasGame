using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukasGame {
    abstract class Map {
        public MapTile[,] Tiles;
        public string Name;

        public Map(int width, int height) {
            Tiles = new MapTile[width, height];
            Generate();
        }

        public abstract void Generate();

        public MapTile this[Point p] {
            get {
                if (p.X < 0 || p.X >= Tiles.GetLength(0) || p.Y < 0 || p.Y >= Tiles.GetLength(1)) {
                    return new AbyssTile();
                } else {
                    return Tiles[p.X, p.Y];
                }
            }
            set {
                if (p.X >= 0 && p.X < Tiles.GetLength(0) && p.Y >= 0 && p.Y < Tiles.GetLength(1)) {
                    Tiles[p.X, p.Y] = value;
                }
            }
        }

        public Rect Rect { get => new Rect(0, 0, Tiles.GetLength(0), Tiles.GetLength(1)); }
    }
}

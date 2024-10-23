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
    }
}

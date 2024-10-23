using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukasGame {
    class TestMap : Map {
        public TestMap() : base(100, 100) {
            Name = "Test map";
        }

        public override void Generate() {
            for (int i = 0; i < 100; i++) {
                for (int j = 0; j < 100; j++) {
                    MapTile tile;
                    if (i == 0 || i == 99 || j == 0 || j == 99) {
                        tile = MapTile.Wall();
                    } else {
                        tile = MapTile.Ground();
                    }
                    Tiles[i, j] = tile;
                }
            }

            for (int i = 0; i < 40; i++) {
                Tiles[i + 10, 7] = MapTile.Wall();
            }
        }
    }
}

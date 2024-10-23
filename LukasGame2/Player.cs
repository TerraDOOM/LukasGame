using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

using Terminal.Gui;

namespace LukasGame {
    class Player : Entity {
        required public Game game;

        public int Health;
        public int AC { get => 20; }

        public override Glyph Glyph => new Glyph { c = '@', fg = Color.White, bg = Color.Black };

        public List<Item> Inventory = new List<Item>() { new Shield(), new Weapon() };

        public Weapon? Weapon;
        public Shield? shield;

        public Armor? Body, Helmet, Gloves, Boots, Amulet, Clothing, Underwear;

        public List<Ring> Rings = new List<Ring>();

        public Player() {
        }

        public bool[,] LightMap(Map map, bool[,] lightMap) {
            var vis = new AdamVisibility(
                (x, y) => !map[(x, y)].Transparent,
                (x, y) => { if (x >= 0 && x < lightMap.GetLength(0) && y >= 0 && y < lightMap.GetLength(1)) lightMap[x, y] = true; },
                (x, y) => (int)Math.Ceiling(((Point)(x, y)).Magnitude())
            );

            foreach (var point in map.Rect.Points()) {
                lightMap[point.X, point.Y] = false;

                if ((point - Position).Magnitude() > 10) {
                    continue;
                }
                
                vis.Compute(Position, 10);
            }

            return lightMap;
        }

        public void PickUp(Map map) {
            var tile = map[Position];
            if (tile.Items.Count == 0) {
                game.Log("There are no items here.", Color.White);
                return;
            }

            if (Inventory.Count == 52) {
                game.Log("Your inventory is full.", Color.White);
                return;
            }

            if (tile.Items.Count == 1) {
                var item = tile.Items[0];
                game.Log($"You picked up the {item.Name}");
                tile.Items.Clear();
                Inventory.Add(item);
                game.UpdateUI();
                return;
            } else {
                var menu = new ItemMenu() {
                    Items = tile.Items,
                    MenuAction = (item) => { },
                    Width = 40,
                    Height = tile.Items.Count + 2,
                    X = Pos.Center(),
                    Y = Pos.Center(),
                };

                menu.MenuAction = (item) => {
                    Inventory.Add(item);
                    if (!tile.Items.Remove(item)) {
                        throw new Exception("wtf");
                    }
                    game.Log($"You picked up the {item.Name}");
                    game.window.CleanPopup(menu);
                };

                menu.Show();

                game.window.MakePopup(menu);
            }
        }
        
        public void DropItem(Item item) {
            Inventory.Remove(item);
            game.currentMap[Position].Items.Add(item);
            game.Log($"You dropped the {item.Name}");
            game.UpdateUI();
        }
    }
}

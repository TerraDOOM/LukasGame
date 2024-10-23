using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Terminal.Gui;

namespace LukasGame {
    class Game {
        public Player Player;
        public Map currentMap;
        bool[,] lightMap;
        public List<(Color, string, int)> Messages = new List<(Color, string, int)>();
        public int Turn { get; internal set; } = 0;
        public bool TurnTaken = false;
        public GameWindow window;

        public List<ITracksGameUpdate> UiElems = new List<ITracksGameUpdate>();

        public Game() {
            Player = new Player() { game = this };
            currentMap = new FelixMap();
            Random rng = new Random();
            int x = rng.Next(100), y = rng.Next(100);
            for (; !currentMap.Tiles[x, y].Passable; x = rng.Next(100), y = rng.Next(100))
                ;
            Player.Position = (x, y);
            currentMap[(x, y)].Entity = Player;
            lightMap = new bool[currentMap.Tiles.GetLength(0), currentMap.Tiles.GetLength(1)];
        }

        public bool[,] LightMap() {
            return Player.LightMap(currentMap, lightMap);
        }

        /// <summary>
        /// Handles input that doesn't do any menuing
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool HandleInput(Key key) {
            bool handled = false;

            var movementKeys = new Dictionary<Rune, (int, int)> {
                { (Rune)'h', (-1, 0) },
                { (Rune)'j', (0, 1) },
                { (Rune)'k', (0, -1) },
                { (Rune)'l', (1, 0) },
                { (Rune)'y', (-1, -1) },
                { (Rune)'u', (1, -1) },
                { (Rune)'b', (-1, 1) },
                { (Rune)'n', (1, 1) },
            };

            if (movementKeys.TryGetValue((Rune)key, out var v)) {
                var newPos = Player.Position + v;
                tryMovePlayerTo(newPos);
                TurnTaken = true;
                handled = true;
            }

            if (TurnTaken) {
                TurnTaken = false;
                foreach (var elem in UiElems) {
                    elem.Update(this);
                }
            }

            return handled;
        }

        void tryMovePlayerTo(Point newPos) {
            if (!currentMap[newPos].Passable) {
                Log("That's a wall");
            }

            if (currentMap[newPos].Entity != null) {
                Log("This position is occupied");
            }

            if (currentMap[newPos].Passable && currentMap[newPos].Entity == null) {
                movePlayerTo(newPos);
            }
        }

        void movePlayerTo(Point pos) {
            currentMap[Player.Position].Entity = null;
            currentMap[pos].Entity = Player;
            Player.Position = pos;

            var tile = currentMap[pos];

            if (tile.Items.Count > 0) {
                if (tile.Items.Count == 1) {
                    Log($"You see here a {tile.Items[0].Name}");
                } else {
                    Log($"You see here a bunch of items");
                }
            }
        }

        public void Log(string message) {
            Log(message, Color.White);
        }

        public void Log(string message, Color color) {
            var newLog = (color, message);
            var last = Messages.Count > 0 ? Messages.Last() : (Color.Black, "", 0);

            if (newLog == (last.Item1, last.Item2)) {
                last.Item3 += 1;
                Messages[Messages.Count - 1] = last;
            } else {
                Messages.Add((color, message, 1));
            }
        }

        public void AddUI(params ITracksGameUpdate[] elems) {
            foreach (var elem in elems) {
                elem.Update(this);
            }
            UiElems.AddRange(elems);
        }

        public void UpdateUI() {
            foreach (var elem in UiElems) {
                elem.Update(this);
            }

            Application.Refresh();
        }
    }
}

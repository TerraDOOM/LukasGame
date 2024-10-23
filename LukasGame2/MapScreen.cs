using System;
using System.Collections.Generic;
using Size = System.Drawing.Size;
using Rectangle = System.Drawing.Rectangle;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Gui;

namespace LukasGame {
    class MapScreen : View {
        public required Game Game;

        Map map { get => Game.currentMap; }

        public Point CameraPos { get => Game.Player.Position - (Viewport.Width / 2, Viewport.Height / 2); }

        Rect CameraRect() {
            return new Rect(CameraPos, Viewport.Width, Viewport.Height);
        }

        Rect ScreenRect() {
            return (Rect)Viewport;
        }

        public MapScreen() : base() {
            CanFocus = true;
        }


        public override void OnDrawContent(Rectangle viewport) {
            var size = GetContentSize();

            var lightMap = Game.LightMap();

            foreach ((var p_map, var p_screen) in CameraRect().Points().Zip(ScreenRect().Points())) {
                var tile = map[p_map];
                if (checkLightMap(p_map, lightMap) && (p_map - Game.Player.Position).Magnitude() < 10.0) {
                    var glyph = tile.GetGlyph();
                    AddRuneWithColor(p_screen.X, p_screen.Y, (Rune)glyph.c, glyph.fg, glyph.bg);
                } else {
                    AddRuneWithColor(p_screen.X, p_screen.Y, (Rune)' ', Color.Black, Color.Black);
                }
            }
        }

        bool checkLightMap(Point p, bool[,] map) {
            if (p.X < 0 || p.X >= map.GetLength(0) || p.Y < 0 || p.Y >= map.GetLength(1)) {
                return true;
            } else {
                return map[p.X, p.Y];
            }
        }

        public void AddRuneWithColor(int x, int y, Rune rune, Color? fg, Color? bg) {
            var curAttribute = Driver.GetAttribute();
            var newAttribute = Driver.MakeColor(fg ?? curAttribute.Foreground, bg ?? curAttribute.Background);
            Driver.SetAttribute(newAttribute);
            AddRune(x, y, rune);
            Driver.SetAttribute(curAttribute);
        }
    }
}

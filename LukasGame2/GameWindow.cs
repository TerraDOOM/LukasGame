using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Gui;

namespace LukasGame {


    class GameWindow : Window {
        Game game = new Game();
        KeybindManager keymap = new KeybindManager();

        MapScreen mapScreen;
        StatsWindow stats;
        InventoryMenu invMenu;
        GameMessages messages;


        public GameWindow() {
            Title = $"Example App ({Application.QuitKey} to quit)";
            Func<ColorName, Terminal.Gui.Attribute> makeAttribute = color => new Terminal.Gui.Attribute(color, Color.Black);

            ColorScheme = new ColorScheme {
                Disabled = makeAttribute(Color.DarkGray),
                Focus = makeAttribute(Color.White),
                HotFocus = makeAttribute(Color.BrightGreen),
                HotNormal = makeAttribute(Color.BrightGreen),
                Normal = makeAttribute(Color.Gray)
            };

            game.window = this;

            mapScreen = new MapScreen { Game = game, Height = Dim.Fill()! - 7, Width = Dim.Fill()! - 20 };

            stats = new StatsWindow {
                Y = Pos.Bottom(mapScreen),
                Height = 7,
                Width = 18,
            };

            invMenu = new InventoryMenu() {
                Enabled = false,
                Visible = false,
                Width = Dim.Auto(DimAutoStyle.Content, 50),
                Height = Dim.Auto(DimAutoStyle.Content, 20),
                X = Pos.Center(),
                Y = Pos.Center(),
            };

            messages = new GameMessages {
                Title = "Messages",
                Y = Pos.Bottom(mapScreen),
                X = Pos.Right(stats),
                Height = 7,
                Width = Dim.Fill()! - 20
            };

            game.AddUI(stats, messages, invMenu);

            Add(mapScreen, stats, messages, invMenu);
            mapScreen.SetFocus();

            setKeybinds();
        }

        void setKeybinds() {
            keymap.AddKey(KeyCode.I, GCommand.Inventory);
            keymap.AddKey(KeyCode.G, GCommand.PickUp);
        }

        public override bool OnKeyDown(Key kev) {
            if ((kev.KeyCode & KeyCode.CharMask) == KeyCode.Null) {
                return false;
            }

            Keybind key = kev.KeyCode;

            var handled = game.HandleInput(kev);
            if (handled) {
                SetNeedsDisplay();
            } else {
                GCommand cmd = keymap.GetKey(key);
                if (cmd == GCommand.Nothing) handled = false;
                else {
                    handleCommand(cmd);
                }
            }
            return handled;
        }

        void handleCommand(GCommand cmd) {
            switch (cmd) {
                case GCommand.Inventory:
                    invMenu.Show();
                    break;
                case GCommand.PickUp:
                    game.Player.PickUp(game.currentMap);
                    break;
            }
        }

        public void MakePopup(View view) {
            Add(view);
            view.SetFocus();
        }

        public void CleanPopup(View view) {
            Remove(view);
            game.UpdateUI();
        }
    }
}

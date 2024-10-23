using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Gui;

namespace LukasGame {
    class GameWindow : Window {
        public GameWindow() {
            Title = $"Example App ({Application.QuitKey} to quit)";

            var map = new MapView();
            map.Set

            Add(map);
        }
    }
}

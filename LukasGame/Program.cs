using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Terminal.Gui;

namespace LukasGame {
    class Program {
        static void Main(string[] args) {
            Application.Run<GameWindow>();
            Application.Shutdown();

        }
    }
}

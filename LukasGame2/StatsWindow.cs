using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Gui;

namespace LukasGame {
    class StatsWindow : FrameView, ITracksGameUpdate {

        public void Update(Game game) {
            Title = $"({game.Player.Position.X}, {game.Player.Position.Y})";

            Text = $"HP {game.Player.Health}\n"
                + $"AC {game.Player.AC}";

            SetNeedsDisplay();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Gui;

namespace LukasGame {
    class GameMessages : FrameView, ITracksGameUpdate {

        public void Update(Game game) {
            var height = Viewport.Height;
            var messages = game.Messages.TakeLast(height);
            Text = "";
            foreach ((var c, var text, var n) in messages) {
                Text += text + (n > 1 ? $" x{n}" : "") + '\n';
            }

            SetNeedsDisplay();
        }
    }
}

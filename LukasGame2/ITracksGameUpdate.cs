using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukasGame {
    /// <summary>
    /// <see langword="interface"/> that implies this UI element needs to be updated whenever the gamestate changes
    /// </summary>
    interface ITracksGameUpdate {
        public void Update(Game game);
    }
}

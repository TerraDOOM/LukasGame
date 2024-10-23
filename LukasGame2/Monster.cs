using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukasGame {
    class Monster : Entity {
        public int Health;
        public bool Aware;

        public required Action<Game, Monster> monsterActions;

        public Glyph glyph;

        public override Glyph Glyph => glyph;

        public void Act(Game game) {
            monsterActions(game, this);
        }
    }
}

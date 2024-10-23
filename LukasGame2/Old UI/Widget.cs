using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukasGame.Old {
    abstract class Widget {
        public Widget Parent { get; set; }
        public Rect Area { get; set; }

        public delegate void ChildrenUpdates(ConsoleKeyInfo cki);
        public abstract void Update(ConsoleKeyInfo cki);
        public abstract void Draw();
    }
}

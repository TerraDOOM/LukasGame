using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukasGame.Old {
    class TextBox : Paragraph {
        public WrappingCounter Cursor = new WrappingCounter(0, 1);

        public TextBox(Rect area) : base(area) {

        }

        public override void Draw() {
            Console.CursorVisible = false;
            base.Draw();
            Console.CursorVisible = true;
            var line = Cursor / Area.w;
            var column = Cursor % Area.w;
            Console.SetCursorPosition(Area.x + column, Area.y + line);
        }

        public override void Update(ConsoleKeyInfo cki) {
            switch (cki.Key) {
                case ConsoleKey.LeftArrow:
                    Cursor--;
                    break;
                case ConsoleKey.RightArrow:
                    Cursor++;
                    break;
                case ConsoleKey.Backspace:
                    if (Contents.Length > 0 && Cursor > 0) {
                        Contents = Contents.Remove(--Cursor, 1);
                        Cursor.Max--;
                    }
                    break;
                default:
                    Cursor.Max++;
                    Contents = Contents.Insert(Cursor++, cki.KeyChar.ToString());
                    break;
            }
        }
    }
}

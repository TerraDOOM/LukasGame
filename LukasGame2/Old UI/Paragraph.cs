using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukasGame.Old {
    class Paragraph : Widget {
        public string Contents { get; set; } = "";

        public Paragraph(Rect area) {
            Area = area;
        }

        public override void Draw() {
            var lines = WrappedLines();
            for (int i = 0; i < Area.h; i++) {
                Console.SetCursorPosition(Area.x, Area.y + i);
                if (i < lines.Count) { 
                    Console.Write(lines[i]); 
                } else {
                    Console.Write(new string(' ', Area.w));
                }
            }
        }

        public override void Update(ConsoleKeyInfo cki) { }

        List<string> WrappedLines() {
            var lines = new List<string>();
            var line = 0;
            while (line < Area.h && line * Area.w < Contents.Length) {
                if ((line + 1) * Area.w >= Contents.Length) {
                    lines.Add(Contents.Substring(line * Area.w) + new string(' ', Area.w - Contents.Length % Area.w));
                } else {
                    lines.Add(Contents.Substring(line * Area.w, Area.w));
                }
                line++;
            }
            return lines;
        }
    }
}

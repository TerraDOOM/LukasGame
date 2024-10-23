using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukasGame.Old {
    [DebuggerDisplay("i = {i}")]
    struct WrappingCounter {
        int min, max, i;
        
        public int Min {
            get => min;
            set {
                if (min >= max) {
                    throw new ArgumentException($"Tried to set Min to {value} while Max was {max}");
                } else {
                    min = value;
                }
            } 
        }
        public int Max {
            get => max;
            set {
                if (min >= max) {
                    throw new ArgumentException($"Tried to set Max to {value} while Min was {min}");
                } else {
                    max = value;
                }
            }
        }

        public int I {
            get => i + Min;
            set {
                i = mod(value);
            }
        }

        public WrappingCounter(int _min, int _max) {
            min = _min;
            max = _max;
            i = 0;
        }

        int mod(int n) {
            return (n - Min) % (Max - Min);
        }

        public static WrappingCounter operator ++(WrappingCounter counter) {
            counter.I = counter.I + 1;
            return counter;
        }

        public static WrappingCounter operator --(WrappingCounter counter) {
            counter.I = counter.I - 1;
            return counter;
        }

        public static implicit operator int(WrappingCounter counter) {
            return counter.I;
        }
    }
}

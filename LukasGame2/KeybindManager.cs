using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Gui;

namespace LukasGame {
    record Keybind {
        public bool Ctrl, Shift;
        public KeyCode Key;

        public override string ToString() {
            var s = (Key & KeyCode.CharMask).ToString();
            return (Ctrl ? "ctrl+" : "") + (Shift ? s : s.ToLower());
        }

        public static implicit operator Keybind(KeyCode kc) {
            return new Keybind {
                Ctrl = kc.HasFlag(KeyCode.CtrlMask),
                Shift = kc.HasFlag(KeyCode.ShiftMask),
                Key = kc & KeyCode.CharMask,
            };
        }
    }

    enum GCommand {
        Inventory,
        PickUp,
        Move,
        Examine,
        Nothing,
    }
    class KeybindManager {
        internal class KeySlot {
            public GCommand? normal, shift, ctrl;
        }

        Dictionary<KeyCode, KeySlot> keymap = new Dictionary<KeyCode, KeySlot>();

        public KeybindManager() { }

        public GCommand GetKey(Keybind key) {
            if (keymap.TryGetValue(key.Key, out var slot)) {
                if (key.Ctrl) {
                    return slot.ctrl ?? slot.normal ?? GCommand.Nothing;
                }

                if (key.Shift) {
                    return slot.shift ?? slot.normal ?? GCommand.Nothing;
                }

                return slot.normal ?? GCommand.Nothing;
            }

            return GCommand.Nothing;
        }

        public void AddKey(Keybind key, GCommand cmd) {
            if (keymap.TryGetValue(key.Key, out var slot)) {
                if (key.Ctrl) slot.ctrl = cmd;
                if (key.Shift) slot.shift = cmd;
                slot.normal = cmd;
            } else {
                keymap.Add(key.Key & KeyCode.CharMask, new KeySlot { normal = cmd });
            }
        }
    }
}

using System.Collections.Generic;

using Terminal.Gui;

namespace LukasGame {
    public class Choice {
        public required string Name;
        public required Action Action;
    }
    class GameMenu : ScrollView {
        public virtual List<Choice> Choices { get; init;  } = new List<Choice>();

        public GameMenu() : base() {
            new Border(this);
            Border.Thickness = new Thickness(1);
        }

        public override bool OnKeyDown(Key k) {
            k = k.NoCtrl.NoAlt;

            var keys = MakeKeyTable(Choices.Count);
            int i;

            if ((i = keys.FindIndex(e => e.KeyCode == k.KeyCode)) != -1) {
                Choices[i].Action();
                return true;
            } else {
                if (k == Key.Esc) {
                    Hide();
                    return true;
                }
            }

            return false;
        }

        protected static List<Key> MakeKeyTable(int n = 52) {
            var keys = new List<Key>();

            int i = 0;
            for (char c = 'a'; c <= 'z' && i < n; c++, i++) {
                if (Key.TryParse(c.ToString(), out var k)) {
                    keys.Add(k);
                } else {
                    throw new Exception("what the fuck");
                }
            }
            for (char c = 'A'; c <= 'Z' && i < n; c++, i++) {
                if (Key.TryParse(c.ToString(), out var k)) {
                    keys.Add(k);
                } else {
                    throw new Exception("what the fuck");
                }
            }

            return keys;
        }

        protected void UpdateLabels() {

            RemoveAll();

            View? last = null;
            var keys = MakeKeyTable();
            int i = 0;
            foreach (Choice c in Choices) {
                last = new Label { Text = $"   {keys[i++].ToString()} {c.Name}", Y = last != null ? Pos.Bottom(last) : 0 };
                Add(last);
            }
        }
        public void Show() {
            Enabled = true;
            Visible = true;
            SetFocus();

            UpdateLabels();

            SetNeedsDisplay();
        }

        public void Hide() {
            Enabled = false;
            Visible = false;
            HasFocus = false;
        }
    }

    class ItemMenu : GameMenu {
        public List<Item> Items = new List<Item>();

        public override List<Choice> Choices {
            get {
                var keys = MakeKeyTable();
                return Items
                    .Select((item, i) => new Choice { 
                        Name = item.Name,
                        Action = () => MenuAction(item)
                    })
                    .ToList();
            }
            init { }
        }
        public required Action<Item> MenuAction;

        public ItemMenu() : base() {}
    }

    class InventoryMenu : GameMenu, ITracksGameUpdate {
        public InventoryMenu() : base() {
            Title = "Inventory";
        }

        public void Update(Game game) {
            Choices.Clear();
            foreach (var item in game.Player.Inventory) {
                Choices.Add(new Choice { Name = item.Name, Action = () => { game.Player.DropItem(item); game.UpdateUI(); }, });
            }
            UpdateLabels();
            SetNeedsDisplay();
        }
    }
}
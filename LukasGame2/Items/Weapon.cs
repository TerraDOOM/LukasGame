namespace LukasGame {
    class Weapon : Item {
        public override ItemType Id { get => ItemType.Sword; }

        public override string Name => "Sword";

        public override string Description => "A sword lol";
    }
}
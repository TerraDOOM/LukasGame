namespace LukasGame {
    enum ItemType {
        Shield,
        Sword,
    }

    abstract class Item {
        public abstract ItemType Id { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }
    }

    abstract class ItemStack : Item {
        public int Count { get; set; }
    }
}
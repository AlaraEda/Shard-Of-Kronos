using System;

namespace DEV.Scripts.Inventory
{
    public class ItemAddedEventArgs : EventArgs
    {
        public Item Item { get; set; }
        public int Amount { get; set; }
    }

    public class ItemRemovedEventArgs : ItemAddedEventArgs
    {
        public int AmountLeft { get; set; }
        public bool SlotRemoved => AmountLeft <= 0;
    }
}
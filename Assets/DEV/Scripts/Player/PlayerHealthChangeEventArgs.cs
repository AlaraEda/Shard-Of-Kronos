using System;

namespace DEV.Scripts.Player
{
    public class PlayerHealthChangeEventArgs : EventArgs
    {
        public float OldValue { get; set; }
        public float NewValue { get; set; }
        public bool IsDamage => NewValue < OldValue;
    }
}
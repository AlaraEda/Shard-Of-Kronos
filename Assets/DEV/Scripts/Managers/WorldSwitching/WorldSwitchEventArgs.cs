using System;

namespace DEV.Scripts.Managers.WorldSwitching
{
    public class WorldSwitchEventArgs : EventArgs
    {
        public enum WorldType
        {
            Normal,
            Spirit
        }
        
        public WorldType NextWorld { get; set; }
    }
}
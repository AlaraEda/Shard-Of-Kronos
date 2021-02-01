using System;

namespace DEV.Scripts.Enemy
{
    public class EnemyStateChangeEventArgs : EventArgs
    {
        public EnemyBase.StateEnum PreviousState { get; set; }
        public EnemyBase.StateEnum NewState { get; set; }
    }
}
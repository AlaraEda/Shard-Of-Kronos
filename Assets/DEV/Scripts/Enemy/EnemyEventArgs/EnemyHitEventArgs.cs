using System;
using UnityEngine;

namespace DEV.Scripts.Enemy
{
    public class EnemyHitEventArgs : EventArgs
    {
        public EnemyBase Enemy { get; set; }
        public float DamageAmount { get; set; }
        public float NewHealth { get; set; }
        public float OldHealth { get; set; }
    }
}
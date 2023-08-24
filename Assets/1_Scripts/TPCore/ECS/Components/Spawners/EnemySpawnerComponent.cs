using System;
using TPCore.Data.Enemies;
using UnityEngine;

namespace TPCore.Ecs.Components
{
    [Serializable]
    public struct EnemySpawnerComponent
    {
        public EnemySO EnemyData;
        public int Amount;
        public Transform Origin;
        public float XRange;
        public float YRange;
        public float Cooldown;
    }
}


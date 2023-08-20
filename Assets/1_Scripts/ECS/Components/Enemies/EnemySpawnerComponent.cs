using System;
using Scripts.Data.Enemies;
using UnityEngine;

namespace Scripts.Ecs.Components
{
    [Serializable]
    public struct EnemySpawnerComponent
    {
        public EnemySO EnemyData;
        public int Amount;
        public Vector3 Origin;
        public float XRange;
        public float YRange;
    }
}
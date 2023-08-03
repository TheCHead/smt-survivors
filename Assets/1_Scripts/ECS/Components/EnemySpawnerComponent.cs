using System;
using UnityEngine;

namespace Scripts.Ecs.Components
{
    [Serializable]
    public struct EnemySpawnerComponent
    {
        public GameObject EnemyPrefab;
        public int Amount;
    }
}
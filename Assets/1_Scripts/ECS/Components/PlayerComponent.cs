using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Ecs.Components
{
    [Serializable]
    public struct PlayerComponent
    {
        public Transform Transform;
        public float Speed;
        public SpriteRenderer Renderer;
        public float AttackCooldown;
        public float TimeSinceLastAttack;
        public GameObject AttackPrefab;
    }
}
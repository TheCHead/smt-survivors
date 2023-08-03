using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Ecs.Components
{
    [Serializable]
    public struct EnemyComponent
    {
        public Transform Transform;
        public float Speed;
        public Collider2D Collider;
        public SpriteRenderer Renderer;
    }
}
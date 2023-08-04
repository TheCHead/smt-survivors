using System;
using UnityEngine;

namespace Scripts.Ecs.Components
{
    [Serializable]
    public struct MoverComponent
    {
        public float Speed;
        public Transform Transform;
        public Vector2 LookDirection;
        public SpriteRenderer Renderer;
    }
}
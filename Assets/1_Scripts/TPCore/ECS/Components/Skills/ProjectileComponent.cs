using UnityEngine;

namespace TPCore.Ecs.Components
{
    public struct ProjectileComponent
    {
        public float Damage;
        public float Duration;
        public float Lifetime;
        public float Pushback;
        public Vector2 Direction;
        public Transform Transform;
        public SpriteRenderer Renderer;
    }
}
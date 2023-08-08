using UnityEngine;

namespace Scripts.Ecs.Components
{
    public struct ProjectileComponent
    {
        public float Duration;
        public float Lifetime;
        public Vector2 Direction;
        public GameObject GameObject;
    }
}
using UnityEngine;

namespace Scripts.Ecs.Components
{
    public struct DamageComponent
    {
        public float DamagePoints;
        public Vector2 DamageDir;
        public float Pushback;
    }
}
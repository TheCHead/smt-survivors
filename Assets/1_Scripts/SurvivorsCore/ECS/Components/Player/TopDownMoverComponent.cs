using System;
using UnityEngine;

namespace Scripts.Ecs.Components
{
    [Serializable]
    public struct TopDownMoverComponent
    {
        public float Speed;
        [HideInInspector]
        public Vector2 LookDirection;
        public Rigidbody2D Rigidbody;
    }
}
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Ecs.Components
{
    public struct AttackComponent
    {
        public GameObject Prefab;
        public Vector2 Direction;
        public Vector2 Origin;
        public float Duration;
        public float Lifetime;
        public GameObject AttackGo;
    }
}
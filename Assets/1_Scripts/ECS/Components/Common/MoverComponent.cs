using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Ecs.Components
{
    [Serializable]
    public struct MoverComponent
    {
        //public Transform BaseTf;
        //public Transform BodyTf;
        public float Speed;
        [HideInInspector]
        public Vector2 LookDirection;
    }
}
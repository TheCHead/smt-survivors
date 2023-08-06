using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Ecs.Components
{
    [Serializable]
    public struct WhipComponent
    {
        public Transform Transform;
    }
}
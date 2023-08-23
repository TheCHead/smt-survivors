using System;
using UnityEngine.Serialization;

namespace TPCore.Ecs.Components
{
    [Serializable]
    public struct LifeComponent
    {
        public float HealthPoints;
    }
}
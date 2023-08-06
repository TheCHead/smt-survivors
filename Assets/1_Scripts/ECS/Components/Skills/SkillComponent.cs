using System;
using UnityEngine;

namespace Scripts.Ecs.Components
{
    [Serializable]
    public struct SkillComponent
    {
        public float Cooldown;
        public float Duration;
        [HideInInspector]
        public float Lifetime;
    }
}
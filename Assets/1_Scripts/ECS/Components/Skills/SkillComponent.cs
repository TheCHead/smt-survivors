using System;
using Leopotam.EcsLite;
using Scripts.Data.Skills;
using UnityEngine;

namespace Scripts.Ecs.Components
{
    [Serializable]
    public struct SkillComponent
    {
        public int Level;
        public SkillSO Data;
        [HideInInspector]
        public float Lifetime;
        [HideInInspector]
        public EcsPackedEntity UserEntity;
    }
}
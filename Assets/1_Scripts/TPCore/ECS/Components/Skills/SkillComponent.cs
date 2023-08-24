using System;
using Leopotam.EcsLite;
using TPCore.Data.Skills;
using UnityEngine;

namespace TPCore.Ecs.Components
{
    [Serializable]
    public struct SkillComponent
    {
        public int Level;
        public SkillSO Data;
        [HideInInspector]
        public EcsPackedEntity UserEntity;
    }
}
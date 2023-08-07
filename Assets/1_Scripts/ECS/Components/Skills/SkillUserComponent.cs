using System;
using Leopotam.EcsLite;
using Scripts.Data.Skills;
using UnityEngine;

namespace Scripts.Ecs.Components
{
    [Serializable]
    public struct SkillUserComponent
    {
        public SpriteRenderer Renderer;
        public SkillSO SkillData;
    }
}
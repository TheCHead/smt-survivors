using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using TPCore.Data.Skills;
using UnityEngine;

namespace TPCore.Ecs.Components
{
    [Serializable]
    public struct SkillUserComponent
    {
        public SkillSO SkillData;
    }
}
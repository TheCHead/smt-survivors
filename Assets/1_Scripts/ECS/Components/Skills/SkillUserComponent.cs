using System;
using Scripts.Data;
using Scripts.Data.Skills;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Ecs.Components
{
    [Serializable]
    public struct SkillUserComponent
    {
        public SpriteRenderer Renderer;
        public float UseCooldown;
        [HideInInspector] public float TimeSinceLastUse;
        [FormerlySerializedAs("SkillPrefab")] public SkillSO SkillData;
    }
}
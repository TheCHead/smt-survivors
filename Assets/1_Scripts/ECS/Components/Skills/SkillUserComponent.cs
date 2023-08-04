using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Ecs.Components
{
    [Serializable]
    public struct SkillUserComponent
    {
        public SpriteRenderer Renderer;
        public float AttackCooldown;
        public float TimeSinceLastAttack;
        public GameObject SkillPrefab;
    }
}
using UnityEngine;

namespace Scripts.Data.Skills
{
    [CreateAssetMenu(fileName = "NewSkillData", menuName = "Skills/NewSkillData", order = 1)]
    public class SkillSO : ScriptableObject
    {
        public GameObject ProjectilePrefab;
        public int Projectiles;
        public float Cooldown;
        public float Duration;
        public float Damage;
    }
}

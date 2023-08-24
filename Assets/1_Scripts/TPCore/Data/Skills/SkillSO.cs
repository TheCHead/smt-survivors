using UnityEngine;

namespace TPCore.Data.Skills
{
    [CreateAssetMenu(fileName = "NewSkillData", menuName = "TPSkills/NewSkillData", order = 1)]
    public class SkillSO : ScriptableObject
    {
        public GameObject ProjectilePrefab;
        public int Projectiles;
        public float Cooldown;
        public float Duration;
        public float Damage;
        public float Pushback;
    }
}

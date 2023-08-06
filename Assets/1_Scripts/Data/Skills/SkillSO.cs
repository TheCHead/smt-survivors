using UnityEngine;

namespace Scripts.Data.Skills
{
    [CreateAssetMenu(fileName = "NewSkillData", menuName = "Skills/NewSkillData", order = 1)]
    public class SkillSO : ScriptableObject
    {
        public GameObject Prefab;
        public float Cooldown;
        public float Duration;
    }
}

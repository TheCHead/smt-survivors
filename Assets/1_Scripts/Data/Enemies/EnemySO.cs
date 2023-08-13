using UnityEngine;

namespace Scripts.Data.Enemies
{
    [CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemies/NewEnemyData", order = 1)]
    public class EnemySO : ScriptableObject
    {
        public GameObject Prefab;
        public float Speed;
        public float HealthPoints;
    }
}
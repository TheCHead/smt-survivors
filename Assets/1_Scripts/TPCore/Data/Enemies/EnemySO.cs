using UnityEngine;

namespace TPCore.Data.Enemies
{
    [CreateAssetMenu(fileName = "NewEnemyData", menuName = "TPEnemies/NewEnemyData", order = 1)]
    public class EnemySO : ScriptableObject
    {
        public GameObject Prefab;
        public float Speed;
        public float HealthPoints;
    }
}
using AB_Utility.FromSceneToEntityConverter;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Scripts.Ecs.Components;
using UnityEngine;

namespace Scripts.Ecs.Systems
{
    public class EnemySpawnerSystem : IEcsRunSystem
    {
        private EcsWorldInject _world = default;
        private EcsFilterInject<Inc<EnemySpawnerComponent>> _spawnerEntities = default;
        private EcsFilterInject<Inc<EnemyTag>> _enemyEntities = default;
        private EcsPoolInject<EnemySpawnerComponent> _spawnerPool = default;
        

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _spawnerEntities.Value)
            {
                ref var spawner = ref _spawnerPool.Value.Get(entity);

                if (_enemyEntities.Value.GetEntitiesCount() < spawner.Amount)
                {
                    // TODO - need to make a generic enemy factory
                     GameObject newGameObject = EcsConverter.InstantiateAndCreateEntity(spawner.EnemyPrefab, _world.Value);
                     newGameObject.transform.position = new Vector3(Random.Range(-10f, 10f), Random.Range(-8f, 8f), 0f);
                }
            }
        }
    }
}
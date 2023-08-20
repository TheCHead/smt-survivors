using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Scripts.Ecs.Components;
using Scripts.Ecs.Factories;
using UnityEngine;

namespace Scripts.Ecs.Systems
{
    public class EnemySpawnerSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        
        private readonly EcsFilterInject<Inc<EnemySpawnerComponent>, Exc<CooldownComponent>> _spawnerEntities = default;
        private readonly EcsFilterInject<Inc<EnemyTag>> _enemyEntities = default;
        private readonly EcsFilterInject<Inc<EnemyTag, KillComponent>> _killedEntities = default;
        
        private readonly EcsPoolInject<EnemySpawnerComponent> _spawnerPool = default;
        
        private readonly EnemyFactory _enemyFactory = new();

        public void Run(IEcsSystems systems)
        {
            OnSpawn();
            OnKill();
        }

        private void OnSpawn()
        {
            foreach (int entity in _spawnerEntities.Value)
            {
                ref var spawner = ref _spawnerPool.Value.Get(entity);

                if (_enemyEntities.Value.GetEntitiesCount() < spawner.Amount)
                {
                    // get enemy of type of T from factory
                    EcsPackedEntity enemyPacked = _enemyFactory.GetEnemy<BlackFrostComponent>(_world.Value, spawner.EnemyData);

                    // configure entity
                    if (enemyPacked.Unpack(_world.Value, out int enemyEntity))
                    {
                        ref var enemyTf = ref _world.Value.GetPool<TransformComponent>().Get(enemyEntity);
                        
                        enemyTf.BaseTf.position = spawner.Origin + new Vector3(
                            Random.Range(-spawner.XRange, spawner.XRange), 
                            Random.Range(-spawner.YRange, spawner.YRange), 
                            0f);
                    }
                   
                    // set spawner cooldown
                    ref var cooldown = ref _world.Value.GetPool<CooldownComponent>().Add(entity);
                    cooldown.CooldownTime = 0.25f;
                }
            }
        }

        private void OnKill()
        {
            foreach (int entity in _killedEntities.Value)
            {
                // TODO - make enemy kill system instead
                _enemyFactory.ReleaseEnemy<BlackFrostComponent>(_world.Value, entity);
            }
        }
    }
}
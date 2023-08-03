using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Scripts.Ecs.Components;
using UnityEngine;

namespace Scripts.Ecs.Systems
{
    public class EnemyMovementSystem : IEcsRunSystem
    {
        private EcsWorldInject _world = default;
        private EcsFilterInject<Inc<EnemyComponent>> _enemyEntities = default;
        private EcsPoolInject<EnemyComponent> _enemyPool = default;

        private Transform _playerTf;

        public EnemyMovementSystem(Transform playerTf)
        {
            _playerTf = playerTf;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _enemyEntities.Value)
            {
                ref var enemy = ref _enemyPool.Value.Get(entity);

                Vector2 dir = (_playerTf.position - enemy.Transform.position).normalized;

                enemy.Renderer.flipX = dir.x < 0;
                
                enemy.Transform.Translate(dir * enemy.Speed * Time.deltaTime);
            }
        }
    }
}
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
        private EcsFilterInject<Inc<EnemyTag, MoverComponent>> _enemyMoverEntities = default;
        private EcsPoolInject<MoverComponent> _moverPool = default;

        private Transform _playerTf;

        public EnemyMovementSystem(Transform playerTf)
        {
            _playerTf = playerTf;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _enemyMoverEntities.Value)
            {
                ref var mover = ref _enemyMoverEntities.Pools.Inc2.Get(entity);
                mover.LookDirection = (_playerTf.position - mover.Transform.position).normalized;

                mover.Renderer.flipX = mover.LookDirection.x < 0;
                
                mover.Transform.Translate(mover.LookDirection * mover.Speed * Time.deltaTime);
            }
        }
    }
}
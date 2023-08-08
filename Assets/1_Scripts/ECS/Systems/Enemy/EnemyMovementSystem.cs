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
        private EcsFilterInject<Inc<EnemyTag, TransformComponent, MoverComponent>> _enemyMoverEntities = default;
        
        //private readonly EcsPoolInject<TransformComponent> _transformPool = default;
        //private EcsPoolInject<MoverComponent> _moverPool = default;

        private Transform _playerTf;

        public EnemyMovementSystem(Transform playerTf)
        {
            _playerTf = playerTf;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _enemyMoverEntities.Value)
            {
                ref var transform = ref _enemyMoverEntities.Pools.Inc2.Get(entity);
                ref var mover = ref _enemyMoverEntities.Pools.Inc3.Get(entity);
                
                mover.LookDirection = (_playerTf.position - transform.BaseTf.position).normalized;

                transform.BodyTf.right = mover.LookDirection.x < 0 ? Vector3.left : Vector3.right;
                transform.BaseTf.Translate(mover.LookDirection * mover.Speed * Time.deltaTime);
            }
        }
    }
}
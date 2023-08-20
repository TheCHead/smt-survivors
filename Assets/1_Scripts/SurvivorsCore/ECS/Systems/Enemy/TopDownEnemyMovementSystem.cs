using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Scripts.Ecs.Components;
using UnityEngine;

namespace Scripts.Ecs.Systems
{
    public class TopDownEnemyMovementSystem : IEcsRunSystem
    {
        private EcsWorldInject _world = default;
        private EcsFilterInject<Inc<EnemyTag, TransformComponent, TopDownMoverComponent>> _enemyMoverEntities = default;
        private EcsFilterInject<Inc<EnemyTag, TransformComponent, TopDownMoverComponent, DamageComponent>> _damageEntities = default;
        
        private Transform _playerTf;

        public TopDownEnemyMovementSystem(Transform playerTf)
        {
            _playerTf = playerTf;
        }

        public void Run(IEcsSystems systems)
        {
            OnMove();
            OnDamage();
        }

        private void OnMove()
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
        
        private void OnDamage()
        {
            foreach (int entity in _damageEntities.Value)
            {
                ref var mover = ref _damageEntities.Pools.Inc3.Get(entity);
                ref var damage = ref _damageEntities.Pools.Inc4.Get(entity);

                mover.Rigidbody.AddForce(damage.DamageDir * damage.Pushback, ForceMode2D.Impulse);
            }
        }
    }
}
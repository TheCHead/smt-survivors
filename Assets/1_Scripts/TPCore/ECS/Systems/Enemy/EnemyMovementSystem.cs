using Common.Ecs.Components;
using DG.Tweening;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TPCore.Ecs.Components;
using UnityEngine;

namespace TPCore.Ecs.Systems
{
    public class EnemyMovementSystem : IEcsRunSystem
    {
        private EcsWorldInject _world = default;
        private EcsFilterInject<Inc<EnemyTag, TransformComponent, EnemyMoverComponent, BornEvent>> _bornEnemyEntities = default;
        //private EcsFilterInject<Inc<EnemyTag, TransformComponent, EnemyMoverComponent, DamageComponent>> _damageEntities = default;
        
        private Transform _playerTf;

        public EnemyMovementSystem(Transform playerTf)
        {
            _playerTf = playerTf;
        }

        public void Run(IEcsSystems systems)
        {
            OnBorn();
            //OnDamage();
        }

        private void OnBorn()
        {
            foreach (int entity in _bornEnemyEntities.Value)
            {
                ref var transform = ref _bornEnemyEntities.Pools.Inc2.Get(entity);
                ref var mover = ref _bornEnemyEntities.Pools.Inc3.Get(entity);

                float targetY = _playerTf.transform.position.y;
                float targetZ = _playerTf.transform.position.z;

                Vector3 targetPos = new Vector3(
                    transform.BaseTf.position.x,
                    targetY,
                    targetZ
                ) + Vector3.up * 2;

                mover.ReachTween = transform.BaseTf.DOMove(targetPos, mover.ReachTime).SetEase(Ease.Linear);
                SpriteRenderer sprite = transform.BodyTf.GetComponent<SpriteRenderer>();
                sprite.color = Color.black;
                mover.ColorTween = sprite.DOColor(Color.white, mover.ReachTime).SetEase(Ease.Linear);
                transform.BodyTf.localScale = Vector3.one;
                mover.ScaleTween = transform.BodyTf.DOScale(Vector3.one * 0.5f, mover.ReachTime).From().SetEase(Ease.Linear);
                //transform.BodyTf.DOBlendableScaleBy(Vector3.one * 0.05f, 0.5f).SetLoops(-1, LoopType.Yoyo);
            }
        }
        
        /*private void OnDamage()
        {
            foreach (int entity in _damageEntities.Value)
            {
                ref var mover = ref _damageEntities.Pools.Inc3.Get(entity);
                ref var damage = ref _damageEntities.Pools.Inc4.Get(entity);

                mover.Rigidbody.AddForce(damage.DamageDir * damage.Pushback, ForceMode2D.Impulse);
            }
        }*/
    }
}


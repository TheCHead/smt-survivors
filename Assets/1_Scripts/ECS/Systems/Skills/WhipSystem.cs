using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Scripts.Ecs.Components;
using Scripts.Ecs.Utility;
using UnityEngine;

namespace Scripts.Ecs.Systems
{
    public class WhipSystem : IEcsRunSystem
    {
        private EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<WhipComponent, FireSkillComponent>> _fireFilter = default;
        private readonly EcsFilterInject<Inc<WhipComponent, ProcessSkillComponent>> _processFilter = default;
        private readonly EcsFilterInject<Inc<WhipComponent, KillSkillComponent>> _killFilter = default;
        
        private readonly EcsPoolInject<SkillComponent> _skillPool = default;
        private readonly EcsPoolInject<WhipComponent> _whipPool = default;
        private readonly EcsPoolInject<MoverComponent> _moverPool = default;


        public void Run(IEcsSystems systems)
        {
            OnFire();
            OnProcess();
            OnKill();
        }


        private void OnFire()
        {
            foreach (int entity in _fireFilter.Value)
            {
                ref var whip = ref _whipPool.Value.Get(entity);

                if (_skillPool.Value.Get(entity).UserEntity.Unpack(_world.Value, out int userEntity))
                {
                    ref var userMover = ref _moverPool.Value.Get(userEntity);

                    whip.Transform.right = userMover.Renderer.flipX ? Vector3.left : Vector3.right;
                }

                whip.Transform.gameObject.SetActive(true);
            }
        }

        private void OnKill()
        {
            foreach (var entity in _killFilter.Value)
            {
                ref var whip = ref _whipPool.Value.Get(entity);
                whip.Transform.gameObject.SetActive(false);
            }
        }

        private void OnProcess()
        {
            foreach (var entity in _processFilter.Value)
            {
                ref var whip = ref _whipPool.Value.Get(entity);

                LayerMask mask = LayerMask.GetMask("Enemy");
                RaycastHit2D[] hits = Physics2D.CircleCastAll(whip.Transform.position, 0.3f,
                    whip.Transform.right, 2f, mask);

                if (hits.Length <= 0) continue;

                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.transform.TryGetComponent(out EntityReference entityReference))
                    {
                        if (entityReference.Entity.Unpack(_world.Value, out int hitEntity))
                        {
                            _world.Value.DelEntity(hitEntity);
                            GameObject.Destroy(entityReference.gameObject);
                        }
                    }
                }
            }
        }
    }
}
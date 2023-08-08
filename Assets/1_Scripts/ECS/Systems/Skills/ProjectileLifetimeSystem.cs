using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Scripts.Ecs.Components;
using Scripts.Ecs.Utility;
using UnityEngine;

namespace Scripts.Ecs.Systems
{
    public class ProjectileLifetimeSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<ProjectileComponent, FireComponent>> _fireFilter = default;
        private readonly EcsFilterInject<Inc<ProjectileComponent>, Exc<KillComponent>> _liveFilter = default;
        private readonly EcsFilterInject<Inc<ProjectileComponent, KillComponent>> _killFilter = default;
        
        private readonly EcsPoolInject<ProjectileComponent> _projectilePool = default;
        private readonly EcsPoolInject<KillComponent> _killPool = default;


        public void Run(IEcsSystems systems)
        {
            OnFire();
            OnLive();
            OnKill();
        }
        
        private void OnFire()
        {
            foreach (var entity in _fireFilter.Value)
            {
                ref var projectile = ref _projectilePool.Value.Get(entity);
                
                LayerMask mask = LayerMask.GetMask("Enemy");
                RaycastHit2D[] hits = Physics2D.CircleCastAll(projectile.GameObject.transform.position, 0.3f,
                    projectile.GameObject.transform.right, 2f, mask);

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

        private void OnLive()
        {
            foreach (var entity in _liveFilter.Value)
            {
                ref var projectile = ref _projectilePool.Value.Get(entity);
                
                projectile.Lifetime += Time.deltaTime;

                if (projectile.Lifetime > projectile.Duration)
                {
                    //_projectilePool.Value.Del(entity);
                    _killPool.Value.Add(entity);
                }
            }
        }
        
        private void OnKill()
        {
            foreach (var entity in _killFilter.Value)
            {
                ref var projectile = ref _projectilePool.Value.Get(entity);
                
                projectile.GameObject.gameObject.SetActive(false);

                _world.Value.DelEntity(entity);
            }
        }
    }
}
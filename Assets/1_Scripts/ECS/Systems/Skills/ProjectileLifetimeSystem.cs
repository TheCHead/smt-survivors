using DG.Tweening;
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
        private readonly EcsFilterInject<Inc<ProjectileComponent, ShootProjectileComponent>, Exc<DelayComponent>> _shootFilter = default;
        private readonly EcsFilterInject<Inc<ProjectileComponent>, Exc<KillComponent, DelayComponent>> _liveFilter = default;
        private readonly EcsFilterInject<Inc<ProjectileComponent, KillComponent>> _killFilter = default;
        
        private readonly EcsPoolInject<ProjectileComponent> _projectilePool = default;
        private readonly EcsPoolInject<KillComponent> _killPool = default;

        
        public void Run(IEcsSystems systems)
        {
            OnShoot();
            OnLive();
            OnKill();
        }

        private void OnShoot()
        {
            foreach (var entity in _shootFilter.Value)
            {
                _shootFilter.Pools.Inc2.Del(entity);
                
                ref var projectile = ref _projectilePool.Value.Get(entity);

                projectile.GameObject.transform.DOScale(Vector3.zero, projectile.Duration / 2f).From();

                if (projectile.Renderer != null)
                {
                    projectile.Renderer.DOFade(0, projectile.Duration * 0.5f).SetDelay(projectile.Duration * 0.5f);
                }

                projectile.GameObject.SetActive(true);
                
                
                // TODO - try replacing raycasts with on trigger enter
                LayerMask mask = LayerMask.GetMask("Enemy");
                RaycastHit2D[] hits = Physics2D.CircleCastAll(projectile.GameObject.transform.position, 0.75f,
                    projectile.GameObject.transform.right, 2f, mask);

                if (hits.Length <= 0) continue;

                foreach (RaycastHit2D hit in hits)
                {
                    /*if (hit.transform.TryGetComponent(out Rigidbody2D rb))
                    {
                        rb.AddForce(projectile.Direction, ForceMode2D.Impulse);
                        continue;
                    }*/
                    
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
                    _killPool.Value.Add(entity);
                }
            }
        }

        private void OnKill()
        {
            foreach (var entity in _killFilter.Value)
            {
                ref var projectile = ref _projectilePool.Value.Get(entity);
                
                // TODO - return to projectile pool
                Object.Destroy(projectile.GameObject);

                _world.Value.DelEntity(entity);
            }
        }
    }
}
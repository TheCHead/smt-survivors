using Common.Ecs.Components;
using Common.Ecs.Utility;
using DG.Tweening;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TPCore.Ecs.Components;
using TPCore.Ecs.Factories;
using UnityEngine;

namespace TPCore.Ecs.Systems
{
    public class ProjectileLifetimeSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<ProjectileComponent, FireEvent>, Exc<DelayComponent>> _shootFilter = default;
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

                projectile.Renderer.color = Color.white;
                projectile.Transform.localScale = Vector3.one;
                
                projectile.Transform.DOScale(Vector3.zero, projectile.Duration / 2f).From();

                if (projectile.Renderer != null)
                {
                    projectile.Renderer.DOFade(0, projectile.Duration * 0.5f).SetDelay(projectile.Duration * 0.5f);
                }

                projectile.Transform.gameObject.SetActive(true);
                
                
                // TODO - try replacing raycasts with on trigger enter
                LayerMask mask = LayerMask.GetMask("Enemy");
                RaycastHit2D[] hits = Physics2D.CircleCastAll(projectile.Transform.position, 0.5f,
                    Vector2.up, 0.1f, mask);

                if (hits.Length <= 0) continue;


                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.transform.TryGetComponent(out EntityReference entityReference))
                    {
                        if (entityReference.Entity.Unpack(_world.Value, out int hitEntity))
                        {
                            ref var damage = ref _world.Value.GetPool<DamageComponent>().Add(hitEntity);
                            damage.DamagePoints = projectile.Damage;
                            damage.DamageDir = (hit.transform.position - projectile.Transform.position).normalized;
                            damage.Pushback = projectile.Pushback;
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
                ProjectileFactory<SlashComponent>.ReleaseProjectile(_world.Value, entity);
            }
        }
    }
}
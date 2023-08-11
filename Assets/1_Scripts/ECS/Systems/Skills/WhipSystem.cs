using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Scripts.Ecs.Components;
using Scripts.Ecs.Factories;
using UnityEngine;
using UnityEngine.Pool;

namespace Scripts.Ecs.Systems
{
    public class WhipSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<SkillComponent, WhipComponent, FireSkillComponent>> _fireFilter = default;

        private readonly EcsPoolInject<TransformComponent> _transformPool = default;
        private readonly EcsPoolInject<ProjectileComponent> _projectilePool = default;
        private readonly EcsPoolInject<ShootProjectileComponent> _shootPool = default;
        private readonly EcsPoolInject<DelayComponent> _delayPool = default;

        public void Run(IEcsSystems systems)
        {
            OnFire();
        }

        private void OnFire()
        {
            foreach (int entity in _fireFilter.Value)
            {
                ref var skill = ref _fireFilter.Pools.Inc1.Get(entity);
                ref var whip = ref _fireFilter.Pools.Inc2.Get(entity);
                
                // fire skill only if user entity is available
                if (!skill.UserEntity.Unpack(_world.Value, out int userEntity)) 
                    continue;
                
                ref var userTf = ref _transformPool.Value.Get(userEntity);
                
                // TODO - skill level-up config (use Odin?)
                for (int i = 0; i < skill.Data.Projectiles; i++)
                {
                    int projectileEntity = ProjectileFactory<WhipComponent>.GetProjectile(_world.Value, skill.Data);
                        
                    // add shoot component with delay
                    _shootPool.Value.Add(projectileEntity);
                    ref var delay = ref _delayPool.Value.Add(projectileEntity);
                    delay.Delay = 0.1f * i;

                    // configure projectile component
                    ref var projectileComponent = ref _projectilePool.Value.Get(projectileEntity);

                    projectileComponent.Renderer = projectileComponent.Transform.GetComponentInChildren<SpriteRenderer>();
                        
                    // configure projectile with skill user data
                    projectileComponent.Transform.parent = userTf.BaseTf;
                    projectileComponent.Transform.localPosition = Vector3.up * i * 0.5f;
                    projectileComponent.Direction = userTf.BodyTf.right;
                        
                    Quaternion rot = Quaternion.identity;
                        

                    if (i % 2 != 0)
                    {
                        projectileComponent.Direction *= -1;
                        rot = Quaternion.Euler(180f, 0f, 0f);
                    } // flip every second projectile
                            
                    projectileComponent.Transform.right = projectileComponent.Direction;
                    projectileComponent.Transform.rotation *= rot;
                        
                    projectileComponent.Transform.gameObject.SetActive(false);
                }
            }
        }
    }
}
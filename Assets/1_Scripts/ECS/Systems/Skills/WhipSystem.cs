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
                
                
                if (skill.UserEntity.Unpack(_world.Value, out int userEntity))
                {
                    for (int i = 0; i < skill.Level; i++)
                    {
                        // TODO - projectile pool
                        int projectileEntity = _world.Value.NewEntity();
                        
                        // add components
                        _shootPool.Value.Add(projectileEntity);
                        ref var delay = ref _delayPool.Value.Add(projectileEntity);
                        delay.Delay = 0.1f * i;

                        ref var projectileComponent = ref _projectilePool.Value.Add(projectileEntity);
                        
                        
                        projectileComponent.GameObject = GameObject.Instantiate(skill.Data.ProjectilePrefab);
                        projectileComponent.Duration = skill.Data.Duration;
                        

                        ref var userTf = ref _transformPool.Value.Get(userEntity);
                        
                        projectileComponent.GameObject.transform.position = userTf.BaseTf.position + Vector3.up * i * 0.5f;
                        projectileComponent.GameObject.SetActive(false);
                        
                        projectileComponent.Direction = userTf.BodyTf.right;
                        if (i % 2 != 0) // flip every second projectile
                            projectileComponent.Direction *= -1;
                    }
                }
            }
        }
    }
}
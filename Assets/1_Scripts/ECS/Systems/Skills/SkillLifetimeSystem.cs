using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Scripts.Ecs.Components;
using UnityEngine;

namespace Scripts.Ecs.Systems
{
    public class SkillLifetimeSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<SkillComponent>, Exc<CooldownComponent>> _readyFilter = default;
        private readonly EcsFilterInject<Inc<SkillComponent, FireSkillComponent>> _fireFilter = default;
        private readonly EcsFilterInject<Inc<SkillComponent, ProcessSkillComponent>> _processFilter = default;
            
        private readonly EcsPoolInject<SkillComponent> _skillPool = default; 
        private readonly EcsPoolInject<FireSkillComponent> _firePool = default;
        private readonly EcsPoolInject<ProcessSkillComponent> _processPool = default;
        private readonly EcsPoolInject<CooldownComponent> _cooldownPool = default;
        private readonly EcsPoolInject<KillSkillComponent> _killPool = default;
        

        public void Run(IEcsSystems systems)
        {
            OnReady();
            OnFire();
            OnProcess();
        }

        private void OnReady()
        {
            foreach (var entity in _readyFilter.Value)
            {
                // add Fire component
                _firePool.Value.Add(entity);
                
                // add Cooldown component
                ref var cooldown = ref _cooldownPool.Value.Add(entity);
                ref var skill = ref _skillPool.Value.Get(entity);
                cooldown.CooldownTime = skill.Cooldown;
            }
        }

        private void OnFire()
        {
            foreach (int entity in _fireFilter.Value)
            {
                _processPool.Value.Add(entity);
            }
        }
        
        private void OnProcess()
        {
            foreach (var entity in _processFilter.Value)
            {
                ref var skill = ref _skillPool.Value.Get(entity);
                
                skill.Lifetime += Time.deltaTime;

                if (skill.Lifetime > skill.Duration)
                {
                    _processPool.Value.Del(entity);
                    _killPool.Value.Add(entity);
                    skill.Lifetime = 0f;
                }
            }
        }
    }
}
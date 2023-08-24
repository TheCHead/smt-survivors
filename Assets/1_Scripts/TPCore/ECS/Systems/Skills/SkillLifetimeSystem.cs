using Common.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TPCore.Ecs.Components;

namespace TPCore.Ecs.Systems
{
    public class SkillLifetimeSystem : IEcsRunSystem
    {
        //private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<SkillComponent>, Exc<CooldownComponent>> _readyFilter = default;
        //private readonly EcsFilterInject<Inc<SkillComponent, FireSkillComponent>> _fireFilter = default;
        //private readonly EcsFilterInject<Inc<SkillComponent, ProcessSkillComponent>> _processFilter = default;
          
        private readonly EcsPoolInject<FireEvent> _firePool = default;
        private readonly EcsPoolInject<CooldownComponent> _cooldownPool = default;
        
        //private readonly EcsPoolInject<SkillComponent> _skillPool = default; 
        //private readonly EcsPoolInject<ProcessSkillComponent> _processPool = default;
        //private readonly EcsPoolInject<KillComponent> _killPool = default;
        

        public void Run(IEcsSystems systems)
        {
            OnReady();
            //OnFire();
            //OnProcess();
        }

        private void OnReady()
        {
            foreach (var entity in _readyFilter.Value)
            {
                // add Fire component
                _firePool.Value.Add(entity);
                
                // add Cooldown component
                ref var cooldown = ref _cooldownPool.Value.Add(entity);
                ref var skill = ref _readyFilter.Pools.Inc1.Get(entity);
                cooldown.CooldownTime = skill.Data.Cooldown;
            }
        }

        // commented as there's no need to process skills, but it's projectiles only
        /*private void OnFire()
        {
            foreach (int entity in _fireFilter.Value)
            {
                _processPool.Value.Add(entity);
            }
        }*/
        
        // commented as there's no need to kill skills, but it's projectiles only
        /*private void OnProcess()
        {
            foreach (var entity in _processFilter.Value)
            {
                ref var skill = ref _skillPool.Value.Get(entity);
                
                skill.Lifetime += Time.deltaTime;

                if (skill.Lifetime > skill.Data.Duration)
                {
                    _processPool.Value.Del(entity);
                    _killPool.Value.Add(entity);
                    skill.Lifetime = 0f;
                }
            }
        }*/
    }
}
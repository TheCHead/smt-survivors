using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Common.Ecs.Components;
using Time = UnityEngine.Time;

namespace Common.Ecs.Systems
{
    public class CooldownSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<CooldownComponent>> _cooldownEntities = default;
        private readonly EcsPoolInject<CooldownComponent> _cooldownPool = default;


        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _cooldownEntities.Value)
            {
                ref var cooldown = ref _cooldownPool.Value.Get(entity);

                cooldown.CooldownTime -= Time.deltaTime;

                if (cooldown.CooldownTime <= 0)
                    _cooldownPool.Value.Del(entity);
            }
        }
    }
}
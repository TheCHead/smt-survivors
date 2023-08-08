using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Scripts.Ecs.Components;
using UnityEngine;

namespace Scripts.Ecs.Systems
{
    public class DelaySystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<DelayComponent>> _delayEntities = default;
        private readonly EcsPoolInject<DelayComponent> _delayPool = default;


        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _delayEntities.Value)
            {
                ref var delay = ref _delayPool.Value.Get(entity);

                delay.Delay -= Time.deltaTime;

                if (delay.Delay <= 0)
                    _delayPool.Value.Del(entity);
            }
        }
    }
}
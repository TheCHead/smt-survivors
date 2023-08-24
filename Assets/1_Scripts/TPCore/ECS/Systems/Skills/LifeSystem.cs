using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TPCore.Ecs.Components;
using UnityEngine;

namespace TPCore.Ecs.Systems
{
    public class LifeSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<LifeComponent, DamageComponent>> _damageEntities = default;

        private readonly EcsPoolInject<KillComponent> _killPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _damageEntities.Value)
            {
                ref var life = ref _damageEntities.Pools.Inc1.Get(entity);
                ref var damage = ref _damageEntities.Pools.Inc2.Get(entity);

                life.HealthPoints -= damage.DamagePoints;
                
                Debug.Log("Remaining HP: " + life.HealthPoints);

                if (life.HealthPoints <= 0)
                {
                    _killPool.Value.Add(entity);
                }
            }
        }
    }
}
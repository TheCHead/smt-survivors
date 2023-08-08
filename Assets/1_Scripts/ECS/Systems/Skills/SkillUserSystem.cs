using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Scripts.Ecs.Components;
using UnityEngine;

namespace Scripts.Ecs.Systems
{
    public class SkillUserSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<SkillUserComponent, TransformComponent>> _userEntities = default;
        private readonly EcsPoolInject<SkillUserComponent> _userPool = default;

        private readonly EcsPoolInject<SkillComponent> _skillPool = default;
        private readonly EcsPoolInject<WhipComponent> _whipPool = default;


        public void Init(IEcsSystems systems)
        {
            foreach (int entity in _userEntities.Value)
            {
                ref var user = ref _userEntities.Pools.Inc1.Get(entity);
                ref var transform = ref _userEntities.Pools.Inc2.Get(entity);

                int skillEntity = _world.Value.NewEntity();

                // TODO - need skill factory
                // add skill base
                ref var skillComponent = ref _skillPool.Value.Add(skillEntity);
                skillComponent.Level = 5;
                skillComponent.Data = user.SkillData;

                skillComponent.UserEntity = _world.Value.PackEntity(entity);
                
                // add skill extension
                ref var whipComponent = ref _whipPool.Value.Add(skillEntity);
                
            }
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _userEntities.Value)
            {
                
            }
        }
    }
}
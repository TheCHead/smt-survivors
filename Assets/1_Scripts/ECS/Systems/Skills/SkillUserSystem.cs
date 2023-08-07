using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Scripts.Ecs.Components;
using UnityEngine;

namespace Scripts.Ecs.Systems
{
    public class SkillUserSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorldInject _world = default;
        private EcsFilterInject<Inc<SkillUserComponent>> _userEntities = default;
        private EcsPoolInject<SkillUserComponent> _userPool = default;

        private EcsPoolInject<SkillComponent> _skillPool = default;
        private EcsPoolInject<WhipComponent> _whipPool = default;


        public void Init(IEcsSystems systems)
        {
            foreach (int entity in _userEntities.Value)
            {
                ref var user = ref _userPool.Value.Get(entity);

                int skillEntity = _world.Value.NewEntity();

                // need skill factory
                // add skill base
                ref var skillComponent = ref _skillPool.Value.Add(skillEntity);
                skillComponent.Cooldown = user.SkillData.Cooldown;
                skillComponent.Duration = user.SkillData.Duration;
                skillComponent.UserEntity = _world.Value.PackEntity(entity);
                // add skill extension
                ref var whipComponent = ref _whipPool.Value.Add(skillEntity);
                GameObject skillGo = Object.Instantiate(user.SkillData.Prefab, user.Renderer.transform);
                whipComponent.Transform = skillGo.transform;
                whipComponent.Transform.localPosition = Vector3.zero;
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
using AB_Utility.FromSceneToEntityConverter;
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


        public void Init(IEcsSystems systems)
        {
            foreach (int entity in _userEntities.Value)
            {
                ref var user = ref _userPool.Value.Get(entity);
                
                GameObject skill = EcsConverter.InstantiateAndCreateEntity(user.SkillData.Prefab, _world.Value);
                skill.transform.parent = user.Renderer.transform;
                skill.transform.localPosition = Vector3.zero;
                // get skill entity, config it with skill data, assign user
                
                
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
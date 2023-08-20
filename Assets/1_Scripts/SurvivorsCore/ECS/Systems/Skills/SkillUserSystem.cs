using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Scripts.Ecs.Components;
using Scripts.Ecs.Factories;

namespace Scripts.Ecs.Systems
{
    public class SkillUserSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<SkillUserComponent, TransformComponent>> _userEntities = default;

        private SkillFactory _skillFactory;

        public void Init(IEcsSystems systems)
        {
            // probably should inject factory instead
            _skillFactory = new SkillFactory();
            
            foreach (int entity in _userEntities.Value)
            {
                EcsPackedEntity skillEntity = _skillFactory.GetNewSkill<WhipComponent>(_world.Value, entity);
            }
        }
    }
}
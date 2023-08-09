using Leopotam.EcsLite;
using Scripts.Ecs.Components;
    
namespace Scripts.Ecs.Factories
{
    public class SkillFactory
    {
        public EcsPackedEntity GetNewSkill<T>(EcsWorld world, int userEntity) where T : struct
        {
            // get pools
            EcsPool<SkillUserComponent> userPool = world.GetPool<SkillUserComponent>();
            EcsPool<SkillComponent> skillPool = world.GetPool<SkillComponent>();
            
            // create new skill entity
            int skillEntity = world.NewEntity();

            // configure skill component
            ref var userComponent = ref userPool.Get(userEntity);
            ref var skillComponent = ref skillPool.Add(skillEntity);
            skillComponent.Level = 1;
            skillComponent.Data = userComponent.SkillData;
            skillComponent.UserEntity = world.PackEntity(userEntity);
                
            // add skill extension
            EcsPool<T> extensionPool = world.GetPool<T>();
            extensionPool.Add(skillEntity);

            return world.PackEntity(skillEntity);
        }
    }
}
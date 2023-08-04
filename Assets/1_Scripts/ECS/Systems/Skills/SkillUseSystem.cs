using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Scripts.Ecs.Components;
using UnityEngine;

namespace Scripts.Ecs.Systems
{
    public class SkillUseSystem : IEcsRunSystem
    {
        private EcsWorldInject _world = default;
        private EcsFilterInject<Inc<SkillUserComponent>> _userEntities = default;
        private EcsPoolInject<SkillUserComponent> _userPool = default;
        private EcsPoolInject<AttackComponent> _attackPool = default;


        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _userEntities.Value)
            {
                ref var user = ref _userPool.Value.Get(entity);

                user.TimeSinceLastAttack += Time.deltaTime;

                if (user.TimeSinceLastAttack > user.AttackCooldown)
                {
                    int attackEntity = _world.Value.NewEntity();
                    ref var attackComponent = ref _attackPool.Value.Add(attackEntity);
                    
                    attackComponent.Direction = user.Renderer.flipX ? Vector2.left : Vector2.right;
                    attackComponent.Origin = user.Renderer.transform.position;
                    attackComponent.Prefab = user.SkillPrefab;
                    attackComponent.Duration = 0.5f;
                    
                    user.TimeSinceLastAttack = 0;
                }
            }
        }
    }
}
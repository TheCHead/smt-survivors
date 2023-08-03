using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Scripts.Ecs.Components;
using UnityEngine;

namespace Scripts.Ecs.Systems
{
    public class AttackSystem : IEcsRunSystem
    {
        private EcsWorldInject _world = default;
        private EcsFilterInject<Inc<PlayerComponent>> _playerEntities = default;
        private EcsPoolInject<PlayerComponent> _playerPool = default;
        private EcsPoolInject<AttackComponent> _attackPool = default;


        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _playerEntities.Value)
            {
                ref var player = ref _playerPool.Value.Get(entity);

                player.TimeSinceLastAttack += Time.deltaTime;

                if (player.TimeSinceLastAttack > player.AttackCooldown)
                {
                    int attackEntity = _world.Value.NewEntity();
                    ref var attackComponent = ref _attackPool.Value.Add(attackEntity);
                    
                    attackComponent.Direction = player.Renderer.flipX ? Vector2.left : Vector2.right;
                    attackComponent.Origin = player.Transform.position;
                    attackComponent.Prefab = player.AttackPrefab;
                    attackComponent.Duration = 0.5f;
                    
                    player.TimeSinceLastAttack = 0;
                }
            }
        }
    }
}
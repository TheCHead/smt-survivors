using Common.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TPCore.Ecs.Components;
using UnityEngine;

namespace TPCore.Ecs.Systems
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<PlayerTag, TransformComponent, PlayerMoverComponent>> _playerMoveEntities = default;
        

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _playerMoveEntities.Value)
            {
                ref var transform = ref _playerMoveEntities.Pools.Inc2.Get(entity);
                ref var mover = ref _playerMoveEntities.Pools.Inc3.Get(entity);

                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");
                
                if (h == 0)
                    continue;
                
                transform.BaseTf.Translate( Vector3.right * h * mover.Speed * Time.deltaTime);
            }
        }
    }
}
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Scripts.Ecs.Components;
using UnityEngine;

namespace Scripts.Ecs.Systems
{
    public class PlayerMovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<PlayerTag, MoverComponent>> _playerMoveEntities = default;
        private readonly EcsPoolInject<MoverComponent> _moverPool = default;

        public void Init(IEcsSystems systems)
        {
            // Without DI Example
            // get world
            //_world = systems.GetWorld();
            // get entities
            //_playerEntities = _world.Value.Filter<PlayerComponent>().End();
            // get components
            //_playerPool = _world.Value.GetPool<PlayerComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _playerMoveEntities.Value)
            {
                ref var mover = ref _moverPool.Value.Get(entity);

                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");

                if (h != 0)
                    mover.Renderer.flipX = h < 0;
                
                
                mover.Transform.Translate(new Vector3(h, v, 0f).normalized * mover.Speed * Time.deltaTime);
            }
        }
    }
}
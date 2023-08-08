using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Scripts.Ecs.Components;
using UnityEngine;

namespace Scripts.Ecs.Systems
{
    public class PlayerMovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<PlayerTag, TransformComponent, MoverComponent>> _playerMoveEntities = default;
        
        //private readonly EcsPoolInject<TransformComponent> _transformPool = default;
        //private readonly EcsPoolInject<MoverComponent> _moverPool = default;

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
                ref var transform = ref _playerMoveEntities.Pools.Inc2.Get(entity);
                ref var mover = ref _playerMoveEntities.Pools.Inc3.Get(entity);

                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");
                
                Vector3 inputDir = new Vector3(h, v, 0f).normalized;
                
                if (inputDir.sqrMagnitude == 0)
                    continue;
                
                // flip body
                if (h != 0)
                    transform.BodyTf.right = h < 0 ? Vector3.left : Vector3.right;

                mover.LookDirection = inputDir;
                transform.BaseTf.Translate( inputDir * mover.Speed * Time.deltaTime);
            }
        }
    }
}
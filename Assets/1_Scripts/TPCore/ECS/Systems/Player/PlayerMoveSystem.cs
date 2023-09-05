using Common.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TPCore.Ecs.Components;
using UnityEngine;

namespace TPCore.Ecs.Systems
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        //private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<PlayerTag, TransformComponent, PlayerMoverComponent, MoveCommand>, 
        Exc<DashProcessComponent, BlockCommand>> _moveEntities = default;

        public void Run(IEcsSystems systems)
        {
            OnMove();
        }

        private void OnMove()
        {
            foreach (int entity in _moveEntities.Value)
            {
                ref var transform = ref _moveEntities.Pools.Inc2.Get(entity);
                ref var mover = ref _moveEntities.Pools.Inc3.Get(entity);
                ref var moveCommand = ref _moveEntities.Pools.Inc4.Get(entity);

                float h = moveCommand.HDeltaR;


                transform.BaseTf.Translate(Vector3.right * h * mover.Speed * Time.deltaTime);
            }
        }
    }
}
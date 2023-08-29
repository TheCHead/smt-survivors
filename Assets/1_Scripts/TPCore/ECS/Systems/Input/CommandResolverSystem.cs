using System.Collections;
using System.Collections.Generic;
using Common.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using TPCore.Ecs.Components;

namespace TPCore.Ecs.Systems
{
    public class CommandResolverSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<PlayerInputComponent, InputReceiverTag, PlayerTag>> _playerInputEntities = default;

        private readonly EcsPoolInject<DashCommand> _dashPool = default;
        private readonly EcsPoolInject<MoveCommand> _movePool = default;
        private readonly EcsPoolInject<BlockCommand> _blockPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _playerInputEntities.Value)
            {

                ref var input = ref _playerInputEntities.Pools.Inc1.Get(entity);

                if (input.Dash)
                {
                    _dashPool.Value.Add(entity);
                }

                if (input.Block)
                {
                    _blockPool.Value.Add(entity);
                }
                
                if (input.HDeltaR != 0)
                {
                    _movePool.Value.Add(entity).HDeltaR = input.HDeltaR;
                }
            }
        }
    }
}


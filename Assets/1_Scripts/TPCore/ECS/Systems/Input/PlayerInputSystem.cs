using System.Collections;
using System.Collections.Generic;
using Common.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TPCore.Ecs.Components;
using UnityEngine;

namespace TPCore.Ecs.Systems
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<InputReceiverTag>> _receiverEntities = default;

        private readonly EcsPoolInject<PlayerInputComponent> _inputPool = default;

        public void Run(IEcsSystems systems)
        {

            float hR = Input.GetAxisRaw("Horizontal");
            float vR = Input.GetAxisRaw("Vertical");
            bool dash = Input.GetKeyDown(KeyCode.Space);

            foreach (int entity in _receiverEntities.Value)
            {
                ref var player = ref _receiverEntities.Pools.Inc1.Get(entity);
                
                ref var input = ref _inputPool.Value.Add(entity);

                input.HDeltaR = hR;
                input.VDeltaR = vR;
                input.Dash = dash;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using Common.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TPCore.Ecs.Components;
using UnityEngine;

namespace TPCore.Ecs.Systems
{
    public class PlayerBlockSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlayerTag, BlockCommand>> _blockEntities = default;

        public void Run(IEcsSystems systems)
        {
            OnBlock();
        }

        private void OnBlock()
        {
            foreach (int entity in _blockEntities.Value)
            {

                Debug.Log("Block");
            }
        }
    }
}


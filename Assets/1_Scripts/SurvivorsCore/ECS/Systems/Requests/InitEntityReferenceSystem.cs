using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Scripts.Ecs.Components;

namespace Scripts.Ecs.Systems
{
    public class InitEntityReferenceSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<InitEntityReferenceRequest>> _initEntities = default;
        private readonly EcsPoolInject<InitEntityReferenceRequest> _initPool = default;


        public void Init(IEcsSystems systems)
        {
            foreach (int entity in _initEntities.Value)
            {
                ref var request = ref _initPool.Value.Get(entity);
                request.Reference.Entity = _world.Value.PackEntity(entity);
                _initPool.Value.Del(entity);
            }
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _initEntities.Value)
            {
                ref var request = ref _initPool.Value.Get(entity);
                request.Reference.Entity = _world.Value.PackEntity(entity);
                _initPool.Value.Del(entity);
            }
        }
    }
}
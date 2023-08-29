using Common.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TPCore.Ecs.Components;
using DG.Tweening;

namespace TPCore.Ecs.Systems
{
    public class PlayerViewSystem : IEcsRunSystem
    {
        //private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<PlayerTag, SpriteComponent, DashCommand, DashProcessComponent>> _dashEntities = default;


        public void Run(IEcsSystems systems)
        {
            OnDash();
        }

        private void OnDash()
        {
            foreach (int entity in _dashEntities.Value)
            {
                ref var sprite = ref _dashEntities.Pools.Inc2.Get(entity);

                sprite.BodySprite.DOFade(0.5f, 0f);
                sprite.BodySprite.DOFade(1f, 0f).SetDelay(0.5f);
            }
        }
    }
}


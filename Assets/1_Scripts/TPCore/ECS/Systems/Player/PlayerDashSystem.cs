using Common.Ecs.Components;
using DG.Tweening;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TPCore.Ecs.Components;
using UnityEngine;

namespace TPCore.Ecs.Systems
{
    public class PlayerDashSystem : IEcsRunSystem
    {
        //private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<PlayerTag, TransformComponent, PlayerMoverComponent, DashCommand, MoveCommand>,
        Exc<DashProcessComponent>> _dashEntities = default;

        private readonly EcsFilterInject<Inc<PlayerTag, DashProcessComponent, BlockCommand>> _dashBlockEntities = default;



        private readonly EcsPoolInject<DashProcessComponent> _dashPool = default;
        private readonly EcsPoolInject<ExitDashCommand> _exitDashPool = default;

        public void Run(IEcsSystems systems)
        {
            OnDash();
            OnDashBlock();
        }

        private void OnDash()
        {
            foreach (int entity in _dashEntities.Value)
            {
                ref var transform = ref _dashEntities.Pools.Inc2.Get(entity);
                ref var mover = ref _dashEntities.Pools.Inc3.Get(entity);
                ref var dashCommand = ref _dashEntities.Pools.Inc4.Get(entity);
                ref var moveCommand = ref _dashEntities.Pools.Inc5.Get(entity);

                ref var dashProcess = ref _dashPool.Value.Add(entity);

                Transform baseTf = transform.BaseTf;
                Vector3 baseTranslation = Vector3.right * moveCommand.HDeltaR * mover.Speed;

                dashProcess.DashTween = DOTween
                .To(()=> baseTranslation, x => baseTranslation = x, baseTranslation * 5f, 0.25f)
                .OnUpdate(()=> baseTf.Translate(baseTranslation * Time.deltaTime))
                .OnComplete(() => 
                {
                    _dashPool.Value.Del(entity);
                    _exitDashPool.Value.Add(entity);
                })
                .SetLoops(2, LoopType.Yoyo)
                //.SetEase(Ease.OutCubic)
                ;
            }
        }

        private void OnDashBlock()
        {
            foreach (int entity in _dashBlockEntities.Value)
            {
                ref var dash = ref _dashBlockEntities.Pools.Inc2.Get(entity);
                
                dash.DashTween.Kill();
                _dashPool.Value.Del(entity);
                _exitDashPool.Value.Add(entity);
            }
        }
    }
}


using AB_Utility.FromSceneToEntityConverter;
using Common.Ecs.Components;
using Common.Ecs.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using TPCore.Ecs.Components;
using TPCore.Ecs.Systems;
using UnityEngine;

namespace TPCore.Ecs.Startups
{
    public class TPCoreStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;

        public Transform player;

        private void Awake()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            _systems.ConvertScene();
            
            AddSystems();

            _systems.Inject();
            _systems.Init();
        }

        private void AddSystems()
        {
            _systems
                .Add(new InitEntityReferenceSystem())
                .Add(new CooldownSystem())
                .Add(new DelaySystem())
                
                // player
                .Add(new PlayerInputSystem())
                .Add(new CommandResolverSystem())
                .Add(new PlayerBlockSystem())
                .Add(new PlayerDashSystem())
                .Add(new PlayerMoveSystem())
                .Add(new PlayerViewSystem())
                
                // skills
                .Add(new SkillUserSystem())
                .Add(new SkillLifetimeSystem())
                .Add(new SlashSystem())
                .Add(new ProjectileLifetimeSystem())
                
                // enemies
                .Add(new EnemySpawnerSystem())
                .Add(new EnemyMovementSystem(player))
                
                .Add(new LifeSystem())

                // one-frame killers
                .DelHere<PlayerInputComponent>()
                .DelHere<DamageComponent>()
                .DelHere<BornEvent>()
                // del player commands
                .DelHere<MoveCommand>()
                .DelHere<BlockCommand>()
                .DelHere<DashCommand>()
                .DelHere<ExitDashCommand>()
                ;
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            if (_systems == null) return;
            _systems.Destroy();
            _systems = null;
            _world.Destroy();
            _world = null;
        }
    }
}


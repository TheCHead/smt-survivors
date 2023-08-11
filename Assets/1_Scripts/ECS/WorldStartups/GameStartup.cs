using AB_Utility.FromSceneToEntityConverter;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using Scripts.Ecs.Components;
using Scripts.Ecs.Systems;
using UnityEngine;

namespace Scripts.Ecs.Startups
{
    public class GameStartup : MonoBehaviour
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
                .Add(new PlayerMovementSystem())
                
                // skills
                .Add(new SkillUserSystem())
                .Add(new SkillLifetimeSystem())
                .Add(new WhipSystem())
                .Add(new ProjectileLifetimeSystem())
                
                // enemies
                .Add(new EnemySpawnerSystem())
                .Add(new EnemyMovementSystem(player))

                // one-frame killers
                .DelHere<FireSkillComponent>()
                .DelHere<KillComponent>();
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


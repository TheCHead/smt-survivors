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
            //AddOneFrames();
            
            _systems.Inject();
            _systems.Init();
        }

        private void AddSystems()
        {
            _systems
                .Add(new InitEntityReferenceSystem())
                .Add(new CooldownSystem())
                
                // player
                .Add(new PlayerMovementSystem())
                
                // enemies
                .Add(new EnemySpawnerSystem())
                .Add(new EnemyMovementSystem(player))
                
                // skills
                .Add(new SkillUserSystem())
                .Add(new SkillLifetimeSystem())
                .Add(new WhipSystem())
                
                // one frame killers
                .DelHere<FireSkillComponent>()
                .DelHere<KillSkillComponent>();
        }

        private void AddOneFrames()
        {
            // no OneFrames in lite
        }

        private void Start()
        {
            AddOneFrames();
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


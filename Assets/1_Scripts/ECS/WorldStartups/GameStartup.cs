using System;
using System.Collections;
using System.Collections.Generic;
using _1_Scripts.ECS.Systems.Common;
using AB_Utility.FromSceneToEntityConverter;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
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
            
            //AddOneFrames();
        }

        private void AddSystems()
        {
            _systems
                .Add(new InitEntityReferenceSystem())
                .Add(new CooldownSystem())
                .Add(new PlayerMovementSystem())
                .Add(new EnemyMovementSystem(player))
                .Add(new EnemySpawnerSystem())
                
                // skills
                .Add(new SkillUserSystem())
                .Add(new SkillLifetimeSystem())
                .Add(new WhipSystem())
                
                // one frame killer
                .Add(new OneFrameKillSystem()); 
        }

        private void AddOneFrames()
        {
            // no OneFrames in lite
            //OneFrameKillSystem.AddOneFrame<FireSkillComponent>();
            //OneFrameKillSystem.AddOneFrame<KillSkillComponent>();
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


using System;
using System.Collections;
using System.Collections.Generic;
using AB_Utility.FromSceneToEntityConverter;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
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
                .Add(new PlayerMovementSystem())
                .Add(new EnemyMovementSystem(player))
                .Add(new EnemySpawnerSystem())
                .Add(new SkillUseSystem())
                .Add(new SkillProcessSystem())
                ;
        }

        private void AddOneFrames()
        {
            // no OneFrames in lite
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


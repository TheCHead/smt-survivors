using System;
using System.Collections.Generic;
using Common.Ecs.Components;
using Common.Ecs.Utility;
using Leopotam.EcsLite;
using TPCore.Data.Enemies;
using TPCore.Ecs.Components;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace TPCore.Ecs.Factories
{
    public class EnemyFactory
    {
        private static Dictionary<Type, ObjectPool<GameObject>> _enemyPoolDict = new();
        
        public EcsPackedEntity GetEnemy<T>(EcsWorld world, EnemySO enemyConfig) where T : struct
        {
            int newEnemyEntity = world.NewEntity();

            // add T component
            world.GetPool<T>().Add(newEnemyEntity);
            // add enemy tag
            world.GetPool<EnemyTag>().Add(newEnemyEntity);
            // add and configure mover
            ref var mover = ref world.GetPool<EnemyMoverComponent>().Add(newEnemyEntity);
            mover.Speed = enemyConfig.Speed;
            mover.ReachTime = enemyConfig.ReachTime;

            // add and configure damage receiver
            ref var life = ref world.GetPool<LifeComponent>().Add(newEnemyEntity);
            life.HealthPoints = enemyConfig.HealthPoints;
            
            // check if no pool available for type of T
            if (!_enemyPoolDict.ContainsKey(typeof(T)))
            {
                // create new GameObject pool for type of T
                _enemyPoolDict[typeof(T)] = GetNewObjectPool(enemyConfig.Prefab);
            }

            // add and configure transform component
            GameObject newGo = _enemyPoolDict[typeof(T)].Get();
            ref var enemyTf = ref world.GetPool<TransformComponent>().Add(newEnemyEntity);
            enemyTf.BaseTf = newGo.transform;
            enemyTf.BodyTf = newGo.transform.GetChild(0);

            //mover.Rigidbody = enemyTf.BaseTf.GetComponent<Rigidbody2D>();

            // add initialize entity reference request
            ref var initRequest = ref world.GetPool<InitEntityReferenceRequest>().Add(newEnemyEntity);
            initRequest.Reference = enemyTf.BaseTf.GetComponent<EntityReference>();

            // add born component
            ref var bornEvent = ref world.GetPool<BornEvent>().Add(newEnemyEntity);

            // return packed entity
            return world.PackEntity(newEnemyEntity);
        }

        public void ReleaseEnemy<T>(EcsWorld world, int enemyEntity)
        {
            ref var enemyTf = ref world.GetPool<TransformComponent>().Get(enemyEntity);
            
            _enemyPoolDict[typeof(T)].Release(enemyTf.BaseTf.gameObject);
                
            world.DelEntity(enemyEntity);
        }

        private ObjectPool<GameObject> GetNewObjectPool(GameObject prefab)
        {
            return new ObjectPool<GameObject>(
                () => Object.Instantiate(prefab),
                enemy =>
                {
                    enemy.gameObject.SetActive(true);
                },
                enemy =>
                {
                    enemy.gameObject.SetActive(false);
                },
                Object.Destroy,
                false,
                100
            );
        }
    }
}
using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Scripts.Data.Enemies;
using Scripts.Ecs.Components;
using Scripts.Ecs.Utility;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Scripts.Ecs.Factories
{
    public class EnemyFactory
    {
        private Dictionary<Type, ObjectPool<GameObject>> _enemyPoolDict = new();
        
        public EcsPackedEntity GetEnemy<T>(EcsWorld world, EnemySO enemyConfig) where T : struct
        {
            int newEnemyEntity = world.NewEntity();

            // add T component
            world.GetPool<T>().Add(newEnemyEntity);
            // add enemy tag
            world.GetPool<EnemyTag>().Add(newEnemyEntity);
            // add and config mover
            ref var mover = ref world.GetPool<MoverComponent>().Add(newEnemyEntity);
            mover.Speed = enemyConfig.Speed;
            
            // check if no pool available for type of T
            if (!_enemyPoolDict.ContainsKey(typeof(T)))
            {
                // create new GameObject pool for type of T
                _enemyPoolDict[typeof(T)] = GetNewObjectPool(enemyConfig.Prefab);
            }

            GameObject newGo = _enemyPoolDict[typeof(T)].Get();
            ref var enemyTf = ref world.GetPool<TransformComponent>().Add(newEnemyEntity);
            enemyTf.BaseTf = newGo.transform;
            enemyTf.BodyTf = newGo.transform.GetChild(0);

            ref var initRequest = ref world.GetPool<InitEntityReferenceRequest>().Add(newEnemyEntity);
            initRequest.Reference = enemyTf.BaseTf.GetComponent<EntityReference>();

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
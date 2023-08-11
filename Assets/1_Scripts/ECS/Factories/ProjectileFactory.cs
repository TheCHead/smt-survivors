using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Scripts.Data.Skills;
using Scripts.Ecs.Components;
using UnityEngine;
using UnityEngine.Pool;
using GameObject = UnityEngine.GameObject;
using Object = UnityEngine.Object;

namespace Scripts.Ecs.Factories
{
    public static class ProjectileFactory<T> where T : struct
    {
        private static ObjectPool<GameObject> _projectilePool;
        

        public static int GetProjectile(EcsWorld world, SkillSO skillSo)
        {
            int projectileEntity = world.NewEntity();

            if (_projectilePool == null)
            {
                _projectilePool = GetNewPool(skillSo.ProjectilePrefab);
            }
            
            ref var projectileComponent = ref world.GetPool<ProjectileComponent>().Add(projectileEntity);
            projectileComponent.Transform = _projectilePool.Get().transform;
            projectileComponent.Duration = skillSo.Duration;
            
            return projectileEntity;
        }
        
        public static void ReleaseProjectile(EcsWorld world, int projectileEntity)
        {
            ref var projectile = ref world.GetPool<ProjectileComponent>().Get(projectileEntity);
            
            _projectilePool.Release(projectile.Transform.gameObject);
                
            world.DelEntity(projectileEntity);
        }


        private static ObjectPool<GameObject> GetNewPool(GameObject prefab)
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
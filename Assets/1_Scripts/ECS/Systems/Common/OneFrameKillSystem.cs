using System;
using System.Collections.Generic;
using System.Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Scripts.Ecs.Components;
using UnityEngine;

namespace _1_Scripts.ECS.Systems.Common
{
    public class OneFrameKillSystem : IEcsRunSystem
    {
        private static readonly EcsWorldInject _world = default;
        
        // Fire skill event
        private readonly EcsFilterInject<Inc<FireSkillComponent>> _fireEntities = default;
        private readonly EcsPoolInject<FireSkillComponent> _firePool = default;
        // Kill skill event
        private readonly EcsFilterInject<Inc<KillSkillComponent>> _killEntities = default;
        private readonly EcsPoolInject<KillSkillComponent> _killPool = default;

        
        public void Run(IEcsSystems systems)
        {
            FireSkillKill();
            KillSkillKill();
            //KillOneFrames();
        }

        private void FireSkillKill()
        {
            foreach (int entity in _fireEntities.Value)
            {
                _firePool.Value.Del(entity);
            }
        }
        
        private void KillSkillKill()
        {
            foreach (int entity in _killEntities.Value)
            {
                _killPool.Value.Del(entity);
            }
        }


        
        /*private interface IOneFrame { }
        private class OneFrameComponent<T> : IOneFrame where T : struct
        {
            public T Type;
        }

        private static Dictionary<Type, IOneFrame> _oneFrames = new();
        //private static List<IOneFrame> _oneFrames = new();
        private static Dictionary<Type, EcsFilter> _filters = new();
        //private static Dictionary<Type, IEcsPool> _pools = new();



        public static void AddOneFrame<T>() where T : struct
        {
            OneFrameComponent<T> comp = new OneFrameComponent<T>();
            
            _oneFrames[typeof(T)] = comp;
            
            _filters[typeof(T)] = _world.Value.Filter<T>().End();
            
            //_pools[typeof(T)] = _world.Value.GetPool<T>();
        }

        

        private void KillOneFrame<T>() where T : struct
        {
            EcsFilter filter = _world.Value.Filter<T>().End();
            EcsPool<T> pool = _world.Value.GetPool<T>();
            foreach (int entity in filter)
            {
                pool.Del(entity);
            }
        }*/
    }
}
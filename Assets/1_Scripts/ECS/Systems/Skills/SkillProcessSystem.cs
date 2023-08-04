using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Scripts.Ecs.Components;
using Scripts.Ecs.Utility;
using UnityEngine;

namespace Scripts.Ecs.Systems
{
    public class SkillProcessSystem : IEcsRunSystem
    {
        private EcsWorldInject _world = default;
        private EcsFilterInject<Inc<AttackComponent>> _attackEntities = default;
        private EcsPoolInject<AttackComponent> _attackPool = default;


        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _attackEntities.Value)
            {
                ref var attack = ref _attackPool.Value.Get(entity);

                if (attack.Lifetime == 0)
                {
                    attack.AttackGo = GameObject.Instantiate(attack.Prefab, attack.Origin, Quaternion.identity);
                    attack.AttackGo.transform.right = attack.Direction;
                }

                attack.Lifetime += Time.deltaTime;

                if (attack.Lifetime > attack.Duration)
                {
                    GameObject.Destroy(attack.AttackGo);
                    _world.Value.DelEntity(entity);
                    continue;
                }


                LayerMask mask = LayerMask.GetMask("Enemy");
                RaycastHit2D[] hits = Physics2D.CircleCastAll(attack.AttackGo.transform.position, 0.3f, attack.AttackGo.transform.right, 2f, mask);

                if (hits.Length <= 0) continue;
                
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.transform.TryGetComponent(out EntityReference entityReference))
                    {
                        if (entityReference.Entity.Unpack(_world.Value, out int hitEntity))
                        {
                            _world.Value.DelEntity(hitEntity);
                            GameObject.Destroy(entityReference.gameObject);
                        }
                        
                    }
                }
            }
        }
    }
}
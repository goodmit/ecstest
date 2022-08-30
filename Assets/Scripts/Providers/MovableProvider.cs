using Leopotam.EcsLite;
using TestEcs.Components;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace TestEcs.Providers
{
    public class MovableProvider : BaseMonoProvider, IConvertToEntity
    {
        private EcsPool<Movable> _movablePool;
        
        public void Convert(int entity, EcsWorld world)
        {
            _movablePool = world.GetPool<Movable>();
            if (!_movablePool.Has(entity))
            {
                _movablePool.Add(entity);
            }

            _movablePool.Get(entity).MoveSpeed = Constants.MoveSpeed;
            _movablePool.Get(entity).CharacterController = gameObject.GetComponent<CharacterController>();
        }
    }
}
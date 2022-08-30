using System;
using Leopotam.EcsLite;
using TestEcs.Api;
using TestEcs.Components;
using TestEcs.Components.Tag;
using UnityEngine;

namespace TestEcs.Systems
{
    public class MovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ITimeService _timeService;
        
        private EcsWorld _world;
        private EcsFilter _movableFilter;

        public MovementSystem(ITimeService timeService)
        {
            _timeService = timeService;
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _movableFilter = _world.Filter<Player>().Inc<Position>().End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var goToFilter = _world.Filter<GoToCommand>().End();
            if (goToFilter.GetEntitiesCount() == 0) return;
            
            var transformPool = _world.GetPool<TransformComponent>();
            
            var goToPool = _world.GetPool<GoToCommand>();
            GoToCommand targetPosComponent = default;
            int goToEntity = default;
            
            foreach (var targetEntity in goToFilter)
            {
                targetPosComponent = goToPool.Get(targetEntity);
                goToEntity = targetEntity;
                break;
            }
            
            foreach (var entity in _movableFilter)
            {
                ref var transformComponent = ref transformPool.Get(entity);
                
                var movablePool = _world.GetPool<Movable>();
                ref var movable = ref movablePool.Get(entity);
                
                var position = transformComponent.transform.position;
                
                if (Math.Abs(Vector3.Distance(targetPosComponent.Destination, position)) > 0.1f)
                {
                    var moveSpeed = movable.MoveSpeed * _timeService.fixedDeltaTime;
                    var movement = (targetPosComponent.Destination - position).normalized * moveSpeed;
                    movement.y = 0;
                    movable.CharacterController.Move(movement);
                    
                    transformComponent.transform.LookAt(new Vector3(targetPosComponent.Destination.x, position.y, targetPosComponent.Destination.z));
                }
                else
                {
                    movable.CharacterController.Move(Vector3.zero);
                    goToPool.Del(goToEntity);
                }
            }
        }
    }
}
using System;
using Leopotam.EcsLite;
using TestEcs.Api;
using TestEcs.Components;
using TestEcs.Components.Tag;
using TestEcs.Data;
using UnityEngine;

namespace TestEcs.Systems
{
    public class MovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ITimeService _timeService;
        private readonly CachedData _cachedData;
        
        private EcsWorld _world;
        private EcsFilter _playersFilter;

        public MovementSystem(ITimeService timeService, CachedData cachedData)
        {
            _timeService = timeService;
            _cachedData = cachedData;
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
           _playersFilter = _world.Filter<Player>().Inc<Speed>().End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var goToFilter = _world.Filter<GoToCommand>().End();
            if (goToFilter.GetEntitiesCount() == 0) return;
            
            var goToPool = _world.GetPool<GoToCommand>();
            GoToCommand targetPosComponent = default;
            int goToEntity = default;
            
            foreach (var targetEntity in goToFilter)
            {
                targetPosComponent = goToPool.Get(targetEntity);
                goToEntity = targetEntity;
                break;
            }
            
            foreach (var entity in _playersFilter)
            {
                var moveSpeedPool = _world.GetPool<Speed>();
                ref var movable = ref moveSpeedPool.Get(entity);
                
                var position = _cachedData.Players[entity].GetPosition();
                
                if (Math.Abs(Vector3.Distance(targetPosComponent.Destination, position)) > 0.1f)
                {
                    var moveSpeed = movable.speed * _timeService.fixedDeltaTime;
                    var movement = (targetPosComponent.Destination - position).normalized * moveSpeed;
                    movement.y = 0;
                    
                    _cachedData.Players[entity].MoveTo(movement);
                    _cachedData.Players[entity].LookAt(new Vector3(targetPosComponent.Destination.x, position.y, targetPosComponent.Destination.z));
                }
                else
                {
                    _cachedData.Players[entity].MoveTo(Vector3.zero);
                    goToPool.Del(goToEntity);
                }
            }
        }
    }
}
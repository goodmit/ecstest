using Leopotam.EcsLite;
using TestEcs.Api;
using TestEcs.Components;
using TestEcs.Components.Tag;
using TestEcs.Data;
using UnityEngine;

namespace TestEcs.Systems
{
    public class OpenDoorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ITimeService _timeService;
        private readonly CachedData _cachedData;
        
        private EcsWorld _world;
        private EcsFilter _doorsFilter;
        private EcsFilter _doorButtonsFilter;
        
        public OpenDoorSystem(ITimeService timeService, CachedData cachedData)
        {
            _timeService = timeService;
            _cachedData = cachedData;
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _doorsFilter = _world.Filter<Door>().Inc<Id>().Inc<Progress>().Inc<Speed>().End();
            _doorButtonsFilter = _world.Filter<DoorButton>().Inc<Id>().Inc<Activable>().End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var activablePool = _world.GetPool<Activable>();
            var idPool = _world.GetPool<Id>();
            var progressPool = _world.GetPool<Progress>();
            var speedPool = _world.GetPool<Speed>();
            
            foreach (var doorButtonEntity in _doorButtonsFilter)
            {
                var activatedComponent = activablePool.Get(doorButtonEntity).IsActive;
                if (!activatedComponent) continue;

                var buttonId = idPool.Get(doorButtonEntity).id;
                
                foreach (var doorEntity in _doorsFilter)
                {
                    var doorId = idPool.Get(doorEntity).id;
                    if (!doorId.Equals(buttonId)) continue;

                    ref var speedComponent = ref speedPool.Get(doorEntity);
                    ref var progressComponent = ref progressPool.Get(doorEntity);
                    progressComponent.progress += speedComponent.speed * _timeService.fixedDeltaTime;
                    progressComponent.progress = Mathf.Clamp01(progressComponent.progress);
                    
                    var doorView = _cachedData.Doors[doorEntity];
                    doorView?.Open(progressComponent.progress);
                }
            }
        }
    }
}
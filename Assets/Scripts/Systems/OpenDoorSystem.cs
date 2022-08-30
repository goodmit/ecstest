using Leopotam.EcsLite;
using TestEcs.Api;
using TestEcs.Components;
using TestEcs.Components.Tag;
using UnityEngine;

namespace TestEcs.Systems
{
    public class OpenDoorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ITimeService _timeService;
        
        private EcsWorld _world;
        private EcsFilter _doorsFilter;
        private EcsFilter _doorButtonsFilter;

        public OpenDoorSystem(ITimeService timeService)
        {
            _timeService = timeService;
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _doorsFilter = _world.Filter<Door>().Inc<Id>().Inc<TransformComponent>().Inc<Progress>().End();
            _doorButtonsFilter = _world.Filter<DoorButton>().Inc<Id>().Inc<Activable>().End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var activablePool = _world.GetPool<Activable>();
            var idPool = _world.GetPool<Id>();
            var progressPool = _world.GetPool<Progress>();
            var transformPool = _world.GetPool<TransformComponent>();
            
            foreach (var doorButtonEntity in _doorButtonsFilter)
            {
                var activatedComponent = activablePool.Get(doorButtonEntity).IsActive;
                if (!activatedComponent) continue;

                var buttonId = idPool.Get(doorButtonEntity).id;
                
                foreach (var doorEntity in _doorsFilter)
                {
                    var doorId = idPool.Get(doorEntity).id;
                    if (!doorId.Equals(buttonId)) continue;

                    ref var progressComponent = ref progressPool.Get(doorEntity);
                    ref var transformComponent = ref transformPool.Get(doorEntity);
                    
                    progressComponent.progress += Constants.OpenDoorSpeed * _timeService.fixedDeltaTime;
                    progressComponent.progress = Mathf.Clamp01(progressComponent.progress);
                    
                    var newPos = transformComponent.transform.localPosition;
                    newPos.y = Constants.ClosedDoorY + (Constants.OpenDoorY - Constants.ClosedDoorY) * progressComponent.progress;
                    transformComponent.transform.localPosition = newPos;
                }
            }
        }
    }
}
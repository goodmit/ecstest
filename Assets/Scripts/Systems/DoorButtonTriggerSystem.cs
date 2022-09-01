using LeoEcsPhysics;
using Leopotam.EcsLite;
using TestEcs.Components;
using TestEcs.Components.Tag;
using TestEcs.Data;

namespace TestEcs.Systems
{
    public class DoorButtonTriggerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly CachedData _cachedData;
        
        private EcsWorld _world;
        private EcsFilter _doorButtonsFilter;

        public DoorButtonTriggerSystem(CachedData cachedData)
        {
            _cachedData = cachedData;
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _doorButtonsFilter = _world.Filter<DoorButton>().Inc<Id>().Inc<Activable>().End();
        }

        public void Run(IEcsSystems ecsSystems)
        {
            var enterFilter = _world.Filter<OnTriggerEnterEvent>().End();
            var exitFilter = _world.Filter<OnTriggerExitEvent>().End();
            
            var enterPool = _world.GetPool<OnTriggerEnterEvent>();
            var exitPool = _world.GetPool<OnTriggerExitEvent>();
            var activablePool = _world.GetPool<Activable>();

            foreach (var entity in enterFilter)
            {
                ref var eventData = ref enterPool.Get(entity);
                
                foreach (var buttonEntity in _doorButtonsFilter)
                {
                    if(!_cachedData.DoorButtons.ContainsKey(buttonEntity) || !_cachedData.DoorButtons[buttonEntity].HasEnterTrigger(eventData)) continue;
                    
                    ref var activateComponent = ref activablePool.Get(buttonEntity);
                    activateComponent.IsActive = true;
                }
                
                enterPool.Del(entity);
            }
            
            foreach (var entity in exitFilter)
            {
                ref var eventData = ref exitPool.Get(entity);
                
                foreach (var buttonEntity in _doorButtonsFilter)
                {
                    if(!_cachedData.DoorButtons.ContainsKey(buttonEntity) || !_cachedData.DoorButtons[buttonEntity].HasExitTrigger(eventData)) continue;
                    
                    ref var activateComponent = ref activablePool.Get(buttonEntity);
                    activateComponent.IsActive = false;
                }
                
                exitPool.Del(entity);
            }
        }
    }
}
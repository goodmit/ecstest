using LeoEcsPhysics;
using Leopotam.EcsLite;
using TestEcs.Components;
using TestEcs.Components.Tag;

namespace TestEcs.Systems
{
    public class DoorButtonCollisionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _doorButtonsFilter;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _doorButtonsFilter = _world.Filter<DoorButton>().Inc<Id>().Inc<TransformComponent>().Inc<Activable>().End();
        }

        public void Run(IEcsSystems ecsSystems)
        {
            var enterFilter = _world.Filter<OnTriggerEnterEvent>().End();
            var exitFilter = _world.Filter<OnTriggerExitEvent>().End();
            
            var enterPool = _world.GetPool<OnTriggerEnterEvent>();
            var exitPool = _world.GetPool<OnTriggerExitEvent>();
            
            var activablePool = _world.GetPool<Activable>();
            var transformPool = _world.GetPool<TransformComponent>();
            
            foreach (var entity in enterFilter)
            {
                ref var eventData = ref enterPool.Get(entity);
                
                foreach (var buttonEntity in _doorButtonsFilter)
                {
                    ref var buttonTransformComponent = ref transformPool.Get(buttonEntity);
                    if (buttonTransformComponent.transform != eventData.senderGameObject.transform) continue;
                    
                    ref var activableComponent = ref activablePool.Get(buttonEntity);
                    activableComponent.IsActive = true;
                }
                
                enterPool.Del(entity);
            }
            
            foreach (var entity in exitFilter)
            {
                ref var eventData = ref exitPool.Get(entity);
                
                foreach (var buttonEntity in _doorButtonsFilter)
                {
                    ref var buttonTransformComponent = ref transformPool.Get(buttonEntity);
                    if (buttonTransformComponent.transform != eventData.senderGameObject.transform) continue;
                    
                    ref var activableComponent = ref activablePool.Get(buttonEntity);
                    activableComponent.IsActive = false;
                }
                
                exitPool.Del(entity);
            }
        }
    }
}
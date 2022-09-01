using Leopotam.EcsLite;
using TestEcs.Client.Components;
using TestEcs.Components.Tag;
using UnityEngine;
using Camera = TestEcs.Components.Tag.Camera;

namespace TestEcs.Client.Systems
{
    public class CameraFollowSystem : IEcsInitSystem,  IEcsRunSystem
    {
        private EcsWorld _world;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
        }
        
        public void Run(IEcsSystems systems)
        {
            var playerFilter = _world.Filter<Player>().Inc<TransformComponent>().End();
            if (playerFilter.GetEntitiesCount() == 0) return;
            
            var cameraFilter = _world.Filter<Camera>().Inc<TransformComponent>().End();
            
            var transformPool = _world.GetPool<TransformComponent>();
            TransformComponent playerTransformComponent = default;
            
            foreach (var player in playerFilter)
            {
                playerTransformComponent = transformPool.Get(player);
                break;
            }
            
            foreach (var camera in cameraFilter)
            {
                ref var transformComponent = ref transformPool.Get(camera);
                var targetPosition = playerTransformComponent.transform.position + Constants.CameraOffset;
                var position = transformComponent.transform.position;
                targetPosition.y = position.y;
                position = Vector3.Lerp(position, targetPosition, Time.deltaTime * 5);
                transformComponent.transform.position = position;
            }
        }
    }
}
using Leopotam.EcsLite;
using TestEcs.Api;
using TestEcs.Components;

namespace TestEcs.Systems
{
    public class MouseInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ICameraRaycastService _cameraService;
        private readonly IMouseInputService _inputService;
        
        private EcsWorld _world;
        
        public MouseInputSystem(IMouseInputService inputService, ICameraRaycastService cameraService)
        {
            _inputService = inputService;
            _cameraService = cameraService;
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
        }
        
        public void Run(IEcsSystems systems)
        {
            if (!_inputService.GetLeftMouseButtonDown()) return;

            var mousePosition = _inputService.GetMousePosition();

            var raycastPosition = _cameraService.GetRaycastPosition(mousePosition);
            if (!_cameraService.HasRaycastHit) return;

            var goToCommandFilter = _world.Filter<GoToCommand>().End();
            var goToPool = _world.GetPool<GoToCommand>();

            if (goToCommandFilter.GetEntitiesCount() > 0)
            {
                foreach (var entity in goToCommandFilter)
                {
                    goToPool.Del(entity);
                }
            }

            var goToEntity = _world.NewEntity();
            goToPool.Add(goToEntity).Destination = raycastPosition;
        }
    }
}
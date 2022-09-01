using LeoEcsPhysics;
using Leopotam.EcsLite;
using TestEcs.Api;
using TestEcs.Client.Systems;
using TestEcs.Systems;
using Voody.UniLeo.Lite;

namespace TestEcs.Startup
{
    public sealed class GameStartup
    {
        private EcsWorld _world;
        private EcsSystems _systems;
        private EcsSystems _fixedSystems;

        private readonly ITimeService _timeService;
        private readonly IMouseInputService _inputService;
        private readonly ICameraRaycastService _cameraService;
        private readonly ISystemFactory _systemFactory;
        
        public GameStartup(ITimeService timeService, IMouseInputService mouseInputService, ICameraRaycastService cameraRaycastService, ISystemFactory systemFactory)
        {
            _timeService = timeService;
            _inputService = mouseInputService;
            _cameraService = cameraRaycastService;
            _systemFactory = systemFactory;
            
            Init();
        }
        
        private void Init()
        {
            _world = new EcsWorld ();
            _fixedSystems = new EcsSystems(_world);
            _systems = new EcsSystems (_world);
            EcsPhysicsEvents.ecsWorld = _world;
            
            _fixedSystems
                .Add(_systemFactory.AddSystem<MovementSystem>())
                .Add(_systemFactory.AddSystem<CameraFollowSystem>())
                .Add(_systemFactory.AddSystem<DoorButtonTriggerSystem>())
                .Add(_systemFactory.AddSystem<OpenDoorSystem>())
                .Init();

            _systems.Add(_systemFactory.AddSystem<MouseInputSystem>()); // new MouseInputSystem(_inputService, _cameraService))
            _systems.Add(_systemFactory.AddSystem<PlayerAnimationSystem>()); // new MouseInputSystem(_inputService, _cameraService))
            _systems.ConvertScene().Init();
        }

        public void Update () {
            _systems.Run ();
        }

        public void FixedUpdate()
        {
            _fixedSystems.Run();
        }

        public void OnDestroy () {
            
            if(_systems == null) return;
            
            _systems.Destroy();
            _systems = null;
            
            _world.Destroy();
            _world = null;
        }
    }
}

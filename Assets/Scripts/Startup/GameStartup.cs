using LeoEcsPhysics;
using Leopotam.EcsLite;
using TestEcs.Api;
using TestEcs.Services;
using TestEcs.Systems;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace TestEcs.Startup
{
    public sealed class GameStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;
        private EcsSystems _fixedSystems;

        private void Start ()
        {
            ITimeService timeService = new UnityTimeService();
            IMouseInputService mouseInputService = new UnityMouseInputService();
            ICameraRaycastService cameraService = new UnityCameraService();
            
            _world = new EcsWorld ();
            _fixedSystems = new EcsSystems(_world);
            _systems = new EcsSystems (_world);
            EcsPhysicsEvents.ecsWorld = _world;
            _systems
                .Add(new MouseInputSystem(mouseInputService, cameraService))
                .Add(new PlayerAnimationSystem());

            _systems
                .ConvertScene()
                .Init();
            
            _fixedSystems
                .Add(new MovementSystem(timeService))
                .Add(new CameraFollowSystem())
                .Add(new DoorButtonCollisionSystem())
                .Add(new OpenDoorSystem(timeService))
                .Init();
        }

        private void Update () {
            _systems.Run ();
        }

        private void FixedUpdate()
        {
            _fixedSystems.Run();
        }

        private void OnDestroy () {
            
            if(_systems == null) return;
            
            _systems.Destroy();
            _systems = null;
            
            _world.Destroy();
            _world = null;
        }
    }
}

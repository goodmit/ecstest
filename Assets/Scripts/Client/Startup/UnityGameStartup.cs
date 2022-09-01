using TestEcs.Api;
using TestEcs.Startup;
using UnityEngine;
using Zenject;

namespace TestEcs.Client.Startup
{
    public class UnityGameStartup : MonoBehaviour
    {
        private GameStartup _startup;
        
        private ITimeService _timeService;
        private IMouseInputService _inputService; 
        private ICameraRaycastService _cameraService;
        private ISystemFactory _systemFactory;
        
        [Inject]
        public void Construct(ITimeService timeService, IMouseInputService mouseInputService, ICameraRaycastService cameraRaycastService, ISystemFactory systemFactory)
        {
            _timeService = timeService;
            _inputService = mouseInputService;
            _cameraService = cameraRaycastService;
            _systemFactory = systemFactory;
        }

        private void Start()
        {
            _startup = new GameStartup(_timeService, _inputService, _cameraService, _systemFactory);
        }

        private void Update()
        {
            _startup.Update();
        }

        private void FixedUpdate()
        {
            _startup.FixedUpdate();
        }

        private void OnDestroy()
        {
            _startup.OnDestroy();
        }
    }
}
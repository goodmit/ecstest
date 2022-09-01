using TestEcs.Api;
using TestEcs.Client.Factories;
using TestEcs.Client.Services;
using TestEcs.Client.Startup;
using TestEcs.Data;
using TestEcs.Startup;
using Zenject;

namespace TestEcs.Client.Installers
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindCacheData();
            BindServices();
            BindEngine();
        }
        
        private void BindCacheData()
        {
            Container.Bind<CachedData>().FromNew().AsSingle().NonLazy();
        }

        private void BindServices()
        {
            Container.Bind<ICameraRaycastService>().To<UnityCameraService>().AsSingle().NonLazy();
            Container.Bind<IMouseInputService>().To<UnityMouseInputService>().AsSingle().NonLazy();
            Container.Bind<ITimeService>().To<UnityTimeService>().AsSingle().NonLazy();
        }

        private void BindEngine()
        {
            Container.BindInterfacesTo<SystemFactory>().FromNew().AsSingle().NonLazy();
            Container.Bind<UnityGameStartup>().FromNewComponentOn(transform.gameObject).AsSingle().NonLazy();
            Container.Bind<GameStartup>().AsSingle().Lazy();
        }
    }
}
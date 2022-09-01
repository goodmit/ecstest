using Leopotam.EcsLite;
using TestEcs.Api;
using Zenject;

namespace TestEcs.Client.Factories
{
    public class SystemFactory : ISystemFactory
    {
        private readonly DiContainer _container;
        
        public SystemFactory(DiContainer diContainer)
        {
            _container = diContainer;
        }

        public T AddSystem<T>() where T : IEcsSystem
        {
            return _container.Instantiate<T>();
        }
    }
}
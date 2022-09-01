using Leopotam.EcsLite;

namespace TestEcs.Api
{
    public interface ISystemFactory
    {
        public T AddSystem<T>() where T : IEcsSystem;
    }
}
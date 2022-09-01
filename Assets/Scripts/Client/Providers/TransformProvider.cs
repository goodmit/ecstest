using Leopotam.EcsLite;
using TestEcs.Client.Components;
using Voody.UniLeo.Lite;

namespace TestEcs.Client.Providers
{
    public class TransformProvider : BaseMonoProvider, IConvertToEntity
    {
        private EcsPool<TransformComponent> _transformPool;
        
        public void Convert(int entity, EcsWorld world)
        {
            _transformPool = world.GetPool<TransformComponent>();
            _transformPool.Add(entity).transform = transform;
        }
    }
}
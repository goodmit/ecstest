using Leopotam.EcsLite;
using TestEcs.Components;
using Voody.UniLeo.Lite;

namespace TestEcs.Providers
{
    public class PositionProvider : BaseMonoProvider, IConvertToEntity
    {
        private EcsPool<Position> _positionPool;
        
        public void Convert(int entity, EcsWorld world)
        {
            _positionPool = world.GetPool<Position>();
            _positionPool.Add(entity).position = transform.position;
        }
    }
}
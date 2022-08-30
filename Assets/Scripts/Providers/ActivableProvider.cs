using Leopotam.EcsLite;
using TestEcs.Components;
using Voody.UniLeo.Lite;

namespace TestEcs.Providers
{
    public class ActivableProvider : BaseMonoProvider, IConvertToEntity
    {
        public void Convert(int entity, EcsWorld world)
        {
            var activablePool = world.GetPool<Activable>();
            if (!activablePool.Has(entity)) activablePool.Add(entity);
        }
    }
}
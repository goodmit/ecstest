using Leopotam.EcsLite;
using TestEcs.Components.Tag;
using Voody.UniLeo.Lite;

namespace TestEcs.Providers
{
    public class CameraProvider : BaseMonoProvider, IConvertToEntity
    {
        public void Convert(int entity, EcsWorld world)
        {
            var cameraComponentPool = world.GetPool<Camera>();

            if(!cameraComponentPool.Has(entity)) cameraComponentPool.Add(entity);
        }
    }
}
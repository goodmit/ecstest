using Leopotam.EcsLite;
using TestEcs.Client.View;
using TestEcs.Components;
using TestEcs.Components.Tag;
using TestEcs.Data;
using Voody.UniLeo.Lite;
using Zenject;

namespace TestEcs.Client.Providers
{
    public class DoorButtonProvider : BaseMonoProvider, IConvertToEntity
    {
        [Inject] private CachedData _cachedData;
        
        public void Convert(int entity, EcsWorld world)
        {
            var doorButtonsPool = world.GetPool<DoorButton>();
            var idComponentPool = world.GetPool<Id>();

            if (!doorButtonsPool.Has(entity)) doorButtonsPool.Add(entity);
            if (!idComponentPool.Has(entity)) idComponentPool.Add(entity);

            var doorButtonView = gameObject.GetComponent<DoorButtonView>();

            ref var idComponent = ref idComponentPool.Get(entity);
            idComponent.id = doorButtonView.DoorButtonId;
            
            _cachedData.AddButtonEntity(entity, doorButtonView);
        }
    }
}
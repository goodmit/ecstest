using Leopotam.EcsLite;
using TestEcs.Components;
using TestEcs.Components.Tag;
using TestEcs.View;
using Voody.UniLeo.Lite;

namespace TestEcs.Providers
{
    public class DoorButtonProvider : BaseMonoProvider, IConvertToEntity
    {
        public void Convert(int entity, EcsWorld world)
        {
            var doorButtonsPool = world.GetPool<DoorButton>();
            var idComponentPool = world.GetPool<Id>();

            if (!doorButtonsPool.Has(entity)) doorButtonsPool.Add(entity);
            if (!idComponentPool.Has(entity)) idComponentPool.Add(entity);

            var doorView = gameObject.GetComponent<DoorButtonView>();

            ref var idComponent = ref idComponentPool.Get(entity);
            idComponent.id = doorView.DoorButtonId;
        }
    }
}
using Leopotam.EcsLite;
using TestEcs.Components;
using TestEcs.Components.Tag;
using TestEcs.View;
using Voody.UniLeo.Lite;

namespace TestEcs.Providers
{
    public class DoorProvider : BaseMonoProvider, IConvertToEntity
    {
        public void Convert(int entity, EcsWorld world)
        {
            var doorComponentPool = world.GetPool<Door>();
            var idComponentPool = world.GetPool<Id>();
            var progressComponentPool = world.GetPool<Progress>();

            if (!doorComponentPool.Has(entity)) doorComponentPool.Add(entity);
            if (!idComponentPool.Has(entity)) idComponentPool.Add(entity);
            if (!progressComponentPool.Has(entity)) progressComponentPool.Add(entity);

            var doorView = gameObject.GetComponent<DoorView>();

            ref var idComponent = ref idComponentPool.Get(entity);
            idComponent.id = doorView.DoorId;

            ref var progressComponent = ref progressComponentPool.Get(entity);
            progressComponent.progress = 0;
        }
    }
}
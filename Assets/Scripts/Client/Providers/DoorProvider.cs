using Leopotam.EcsLite;
using TestEcs.Client.View;
using TestEcs.Components;
using TestEcs.Components.Tag;
using TestEcs.Data;
using Voody.UniLeo.Lite;
using Zenject;

namespace TestEcs.Client.Providers
{
    public class DoorProvider : BaseMonoProvider, IConvertToEntity
    {
        [Inject] private CachedData _cachedData;
        
        public void Convert(int entity, EcsWorld world)
        {
            var doorComponentPool = world.GetPool<Door>();
            var idComponentPool = world.GetPool<Id>();
            var progressComponentPool = world.GetPool<Progress>();
            var speedComponentPool = world.GetPool<Speed>();

            if (!doorComponentPool.Has(entity)) doorComponentPool.Add(entity);
            if (!idComponentPool.Has(entity)) idComponentPool.Add(entity);
            if (!progressComponentPool.Has(entity)) progressComponentPool.Add(entity);
            if (!speedComponentPool.Has(entity)) speedComponentPool.Add(entity);

            var doorView = gameObject.GetComponent<DoorView>();

            ref var idComponent = ref idComponentPool.Get(entity);
            idComponent.id = doorView.DoorId;

            ref var progressComponent = ref progressComponentPool.Get(entity);
            progressComponent.progress = 0;

            ref var speedComponent = ref speedComponentPool.Get(entity);
            speedComponent.speed = doorView.OpenSpeed;
            
            _cachedData.AddDoorEntity(entity, doorView);
        }
    }
}
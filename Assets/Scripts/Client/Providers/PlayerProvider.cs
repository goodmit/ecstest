using Leopotam.EcsLite;
using TestEcs.Client.Components;
using TestEcs.Client.View;
using TestEcs.Components;
using TestEcs.Components.Tag;
using TestEcs.Data;
using UnityEngine;
using Voody.UniLeo.Lite;
using Zenject;

namespace TestEcs.Client.Providers
{
    public class PlayerProvider : BaseMonoProvider, IConvertToEntity
    {
        [Inject] private CachedData _cachedData;
        
        public void Convert(int entity, EcsWorld world)
        {
            var playerComponentPool = world.GetPool<Player>();
            var animationComponentPool = world.GetPool<PlayerAnimation>();
            var speedComponentPool = world.GetPool<Speed>();
            
            if(!playerComponentPool.Has(entity)) playerComponentPool.Add(entity);
            if(!animationComponentPool.Has(entity)) animationComponentPool.Add(entity);
            if(!speedComponentPool.Has(entity)) speedComponentPool.Add(entity);

            var playerView = gameObject.GetComponent<PlayerView>();
            
            ref var speedComponent = ref speedComponentPool.Get(entity);
            speedComponent.speed = playerView.MoveSpeed; 
                
            ref var animationComponent = ref animationComponentPool.Get(entity);
            animationComponent.Animator = transform.gameObject.GetComponent<Animator>();

            _cachedData.AddMovableEntity(entity, playerView);
        }
    }
}
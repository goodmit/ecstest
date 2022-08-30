using Leopotam.EcsLite;
using TestEcs.Components;
using TestEcs.Components.Tag;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace TestEcs.Providers
{
    public class PlayerProvider : BaseMonoProvider, IConvertToEntity
    {
        public void Convert(int entity, EcsWorld world)
        {
            var playerComponentPool = world.GetPool<Player>();
            var animationComponentPool = world.GetPool<PlayerAnimation>();
            
            if(!playerComponentPool.Has(entity)) playerComponentPool.Add(entity);
            if(!animationComponentPool.Has(entity)) animationComponentPool.Add(entity);

            ref var animationComponent = ref animationComponentPool.Get(entity);
            animationComponent.Animator = transform.gameObject.GetComponent<Animator>();
        }
    }
}
using Leopotam.EcsLite;
using TestEcs.Client.Components;
using TestEcs.Components;
using TestEcs.Components.Tag;

namespace TestEcs.Client.Systems
{
    public class PlayerAnimationSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var playersFilter = world.Filter<Player>().Inc<PlayerAnimation>().End();
            var goToFilter = world.Filter<GoToCommand>().End();
            
            foreach (var entity in playersFilter)
            {
                var playerAnimator = world.GetPool<PlayerAnimation>().Get(entity);
                playerAnimator.Animator.SetBool("Moving", goToFilter.GetEntitiesCount() > 0);
            }
        }
    }
}
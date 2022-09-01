using LeoEcsPhysics;

namespace TestEcs.Api
{
    public interface IDoorButton
    {
        public bool HasEnterTrigger(OnTriggerEnterEvent eventData);
        public bool HasExitTrigger(OnTriggerExitEvent eventData);
    }
}
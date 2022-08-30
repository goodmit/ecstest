namespace TestEcs.Api
{
    public interface ITimeService
    {
        public float deltaTime { get; }
        public float fixedDeltaTime { get; }
    }
}
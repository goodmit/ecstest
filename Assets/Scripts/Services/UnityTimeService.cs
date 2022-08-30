using TestEcs.Api;
using UnityEngine;

namespace TestEcs.Services
{
    public class UnityTimeService : ITimeService
    {
        public float deltaTime => Time.deltaTime;
        public float fixedDeltaTime => Time.fixedDeltaTime;
    }
}
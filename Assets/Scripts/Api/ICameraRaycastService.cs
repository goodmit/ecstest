using UnityEngine;

namespace TestEcs.Api
{
    public interface ICameraRaycastService
    {
        public bool HasRaycastHit { get; }
        public Vector3 GetRaycastPosition(Vector3 mousePosition);
    }
}
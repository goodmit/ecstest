using UnityEngine;

namespace TestEcs.Api
{
    public interface IMouseInputService
    {
        public Vector3 GetMousePosition();
        public bool GetLeftMouseButtonDown();
    }
}
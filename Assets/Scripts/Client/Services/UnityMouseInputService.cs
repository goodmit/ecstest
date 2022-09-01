using TestEcs.Api;
using UnityEngine;

namespace TestEcs.Client.Services
{
    public class UnityMouseInputService : IMouseInputService
    {
        public Vector3 GetMousePosition()
        {
            return Input.mousePosition;
        }
        
        public bool GetLeftMouseButtonDown()
        {
            return Input.GetMouseButtonDown(0);
        }
    }
}
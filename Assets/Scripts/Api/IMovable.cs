using UnityEngine;

namespace TestEcs.Api
{
    public interface IMovable
    {
        public Vector3 GetPosition();
        public void MoveTo(Vector3 target);
        public void LookAt(Vector3 target);
    }
}
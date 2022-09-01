using TestEcs.Api;
using UnityEngine;

namespace TestEcs.Client.View
{
    public class DoorView : MonoBehaviour, IDoor
    {
        [SerializeField] private string _doorId;
        [SerializeField] private float _openSpeed = 0.4f;

        public string DoorId => _doorId;
        public float OpenSpeed => _openSpeed;
        
        public void Open(float progress)
        {
            var newPos = transform.localPosition;
            newPos.y = Constants.ClosedDoorY + (Constants.OpenDoorY - Constants.ClosedDoorY) * progress;
            transform.localPosition = newPos;
        }
    }
}
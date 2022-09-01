using LeoEcsPhysics;
using TestEcs.Api;
using UnityEngine;

namespace TestEcs.Client.View
{
    public class DoorButtonView : MonoBehaviour, IDoorButton
    {
        [SerializeField] private string doorButtonId;

        public string DoorButtonId => doorButtonId;
        public bool HasEnterTrigger(OnTriggerEnterEvent eventData)
        {
            return transform == eventData.senderGameObject.transform;
        }
        
        public bool HasExitTrigger(OnTriggerExitEvent eventData)
        {
            return transform == eventData.senderGameObject.transform;
        }
    }
}
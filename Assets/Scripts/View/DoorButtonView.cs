using UnityEngine;

namespace TestEcs.View
{
    public class DoorButtonView : MonoBehaviour
    {
        [SerializeField] private string doorButtonId;

        public string DoorButtonId => doorButtonId;
    }
}
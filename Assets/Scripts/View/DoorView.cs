using UnityEngine;

namespace TestEcs.View
{
    public class DoorView : MonoBehaviour
    {
        [SerializeField] private string _doorId;

        public string DoorId => _doorId;
    }
}
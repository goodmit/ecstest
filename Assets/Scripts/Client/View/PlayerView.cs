using TestEcs.Api;
using UnityEngine;

namespace TestEcs.Client.View
{
    public class PlayerView : MonoBehaviour, IMovable
    {
        [SerializeField] private CharacterController _character;
        [SerializeField] private float _moveSpeed = 2;

        public float MoveSpeed => _moveSpeed;
        
        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void MoveTo(Vector3 target)
        {
            _character.Move(target);
        }

        public void LookAt(Vector3 target)
        {
            transform.LookAt(target);
        }
    }
}
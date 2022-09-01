using TestEcs.Api;
using UnityEngine;

namespace TestEcs.Client.Services
{
    public class UnityCameraService : ICameraRaycastService
    {
        private bool _hasRaycastHit;
        
        public bool HasRaycastHit => _hasRaycastHit;

        public Vector3 GetRaycastPosition(Vector3 mousePosition)
        {
            var targetPosition = Vector3.zero;
            var ray = ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out var hit, 100, LayerMask.GetMask("Ground")))
            {
                _hasRaycastHit = true;
                targetPosition = hit.point;
            }
            else
            {
                _hasRaycastHit = false;
            }

            return targetPosition;
        }
        
        private Ray ScreenPointToRay(Vector3 position)
        {
            return Camera.main!.ScreenPointToRay(position);
        }
    }
}
using UnityEngine;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Inputs.Detectors
{
    [RequireComponent(typeof(Collider))]
    public class MouseDown:MonoBehaviour
    {
        public event Delegates.Vector3Target onMouseDown;

        private LayerMask LayerMask { get; set; }

        public void Set(LayerMask layerMask, Delegates.Vector3Target onMouseDown)
        {
            LayerMask = layerMask;
            this.onMouseDown += onMouseDown;
        }

        private void OnMouseDown()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit, float.MaxValue, LayerMask.value))
            {
                if(onMouseDown != null)
                    onMouseDown.Invoke(raycastHit.point);

    
            }
        }
    }
}

using UnityEngine;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Inputs
{
    [RequireComponent(typeof(Collider))]
    class WorldInput: MonoBehaviour
    {
        public static event Delegates.Vector3Target OnClick;

        [SerializeField]
        private LayerMask LayerMask;

        private void OnMouseDown()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit, float.MaxValue, LayerMask.value))
            {
                if(OnClick != null)
                    OnClick.Invoke(raycastHit.point);
            }
        }
    }
}

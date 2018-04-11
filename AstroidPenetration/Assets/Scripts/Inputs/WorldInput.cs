using UnityEngine;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using Assets.Scripts.Inputs.Detectors;

namespace Assets.Scripts.Inputs
{
    class WorldInput: BaseInput
    {
        public static event Delegates.Vector3Target OnClickTarget;

        public static event Delegates.Vector3NormalTarget OnClickTargetNormal;

        [SerializeField]
        private LayerMask LayerMask;

        public override void Click()
        {
            if(IsInputEnabled)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit raycastHit;
                if(Physics.Raycast(ray, out raycastHit, float.MaxValue, LayerMask.value))
                {
                    if(OnClickTarget != null)
                        OnClickTarget.Invoke(raycastHit.point);

                    if(OnClickTargetNormal != null)
                        OnClickTargetNormal.Invoke(raycastHit.point, raycastHit.normal);
                }
            }
        }
    }
}

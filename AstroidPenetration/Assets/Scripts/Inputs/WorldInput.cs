using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Inputs
{
    [RequireComponent(typeof(Collider))]
    class WorldInput: MonoBehaviour
    {
        public delegate void OnClick(Vector2 position);

        public static event OnClick onClick;

        [SerializeField]
        private LayerMask LayerMask;

        private void OnMouseDown()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit, float.MaxValue, LayerMask.value))
            {
                Debug.Log(raycastHit.point);
                onClick.Invoke(raycastHit.point);
            }
        }
    }
}

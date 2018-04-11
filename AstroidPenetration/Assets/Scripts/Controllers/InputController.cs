using Assets.Scripts.Inputs;
using Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Controllers
{
    public class InputController:BaseController
    {
        private LayerMask MoveLayerMask { get; set; }

        #region Events
        public event Delegates.Vector3Target OnClickTarget;
        public event Delegates.Vector3NormalTarget OnClickTargetNormal;
        #endregion


        protected override IEnumerator Init()
        {
            MoveLayerMask = LayerMask.GetMask("World");

            GUIInput.onClick += BasicInput;
            yield return null;
        }

        private void BasicInput(InputType type, int index, PointerEventData eventData)
        {
            switch(type)
            {
                case InputType.MajorAction:
                    {
                        MajorAction(index, eventData);
                    }break;
                case InputType.MinorAction:
                    {
                        MinorAction(index);
                    }break;
                case InputType.Options:
                    {
                        OptionAction(index);
                    }break;
            }
        }

        private void MajorAction(int index, PointerEventData eventData)
        {
            switch(index)
            {
                case 0:
                    {
                        OnWorldClick(eventData);
                    }
                    break;
                default:
                    {
                        throw new System.NotImplementedException("MajorAction: " + index);
                    }
            }
        }

        private void MinorAction(int index)
        {
            switch(index)
            {
                default:
                    {
                        throw new System.NotImplementedException("MinorAction: " + index);
                    }
            }
        }

        private void OptionAction(int index)
        {
            switch(index)
            {
                default:
                    {
                        throw new System.NotImplementedException("OptionAction: " + index);
                    }
            }
        }

        private void OnWorldClick(PointerEventData eventData)
        {
            Ray ray = Camera.main.ScreenPointToRay(eventData.position);
            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit, float.MaxValue, MoveLayerMask.value))
            {
                if(OnClickTarget != null)
                    OnClickTarget.Invoke(raycastHit.point);

                if(OnClickTargetNormal != null)
                    OnClickTargetNormal.Invoke(raycastHit.point, raycastHit.normal);
            }
        }
    }
}

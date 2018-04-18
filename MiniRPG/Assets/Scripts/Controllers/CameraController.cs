using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    [RequireComponent(typeof(Camera))]
    public class CameraController: BaseController
    {
        private Func<Vector3> PlayerPosition;
        private Func<Vector3> PlayerForward;
        private Func<bool> PlayerIsMoving;


        private Vector3 distanceVelocity;
        private Vector3 positionVelocity;
        private Vector3 lookAtVelocity;
 
        [SerializeField]
        private Vector3 offSet = Vector3.up;
        [SerializeField]
        private float maxDistance = 5f;
        [SerializeField]
        private float minDistance = 2f;
        [SerializeField]
        private float distanceSmoothTime = .5f;
        [SerializeField]
        private float lookAtSmoothTime = .5f;
        [SerializeField]
        private float positionSmoothTime = .5f;
        [SerializeField]
        private LayerMask groudLayerMask;


        private Vector3 CurrentLookAt { get; set; }
        private Vector3 CurrentPosition { get; set; }
        private Vector3 TargetOffset;
        private bool CameraRotation { get; set; }


        protected override IEnumerator Init()
        {
            PlayerController playerController = GetController<PlayerController>();
            PlayerPosition = () =>
            {
                return playerController.transform.position;
            };

            PlayerForward = () =>
            {
                return playerController.transform.forward;
            };

            PlayerIsMoving = () =>
            {
                return playerController.IsMoving;
            };

            InputController inputController = GetController<InputController>();
            inputController.OnDrag += InputController_OnDrag;
            inputController.OnPlayerStartDrag += InputController_OnPlayerStartDrag;
            inputController.OnEndDrag += InputController_OnEndDrag;

            CurrentLookAt = PlayerPosition();
            CurrentPosition = transform.position;
            CameraRotation = false;
            yield return null;
        }

        private void InputController_OnEndDrag(Vector3 target)
        {
            CameraRotation = false;
        }

        private void InputController_OnPlayerStartDrag(Vector3 target)
        {
            CameraRotation = !PlayerIsMoving();
        }

        private void InputController_OnDrag(Vector2 target)
        {
            if(CameraRotation)
            {
                Vector3 playerPosition = PlayerPosition();

                transform.RotateAround(playerPosition, Vector3.up, target.x);
                transform.RotateAround(playerPosition, transform.right, target.y);
            }
        }

        private void LateUpdate()
        {
            Vector3 playerPosition = PlayerPosition();

            CurrentLookAt = Vector3.SmoothDamp(CurrentLookAt, playerPosition + PlayerForward(), ref lookAtVelocity, lookAtSmoothTime);

            if(!CameraRotation)
            {
                float distance = Vector3.Distance(CurrentPosition, playerPosition);
                if(distance > maxDistance)
                {
                    CurrentPosition = Vector3.SmoothDamp(CurrentPosition, playerPosition - (new Vector3(transform.forward.x, 0, transform.forward.z) * maxDistance),
                        ref distanceVelocity, distanceSmoothTime);
                }
                else if(distance < minDistance)
                {
                    CurrentPosition = Vector3.SmoothDamp(CurrentPosition, playerPosition - (new Vector3(transform.forward.x, 0, transform.forward.z) * minDistance),
                        ref distanceVelocity, distanceSmoothTime);
                }
                transform.position = Vector3.SmoothDamp(transform.position, CurrentPosition + TargetOffset, ref positionVelocity, positionSmoothTime);
            }
            else
            {
                CurrentPosition = transform.position - TargetOffset;
            }

            transform.LookAt(CurrentLookAt);
        }

        private void FixedUpdate()
        {
            TargetOffset = offSet;

            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit, offSet.y, groudLayerMask.value))
                TargetOffset.y += offSet.y -(transform.position.y - raycastHit.point.y);
        }
    }
}

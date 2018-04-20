using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    [RequireComponent(typeof(Camera))]
    public class CameraController: BaseController, WorldInputController.IWorldDrag, WorldInputController.IBeginPlayerDrag, WorldInputController.IEndWorldDrag
    {
        private Func<Vector3> PlayerPosition;
        private Func<Vector3> PlayerForward;
        private Func<bool> PlayerIsMoving;
        private Func<float> RotationSensitive;


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
        private float maxRotationX= 80f;
        [SerializeField]
        private float minRotationX = 5f;
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

            SettingsController settingsController = GetController<SettingsController>();

            RotationSensitive = () =>
            {
                return settingsController.Instance.CameraRotationSensitive;
            };

            GetController<WorldInputController>().AddObserver(this);

            CurrentLookAt = PlayerPosition();
            CurrentPosition = transform.position;
            CameraRotation = false;
            yield return null;
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

        protected override IEnumerator InitObservers()
        {
            yield return null;
        }

        public void OnWorldDrag(Vector2 position)
        {
            if(CameraRotation)
            {
                Vector3 playerPosition = PlayerPosition();
                position *= RotationSensitive();
                transform.RotateAround(playerPosition, Vector3.up, position.x);
                transform.RotateAround(playerPosition, transform.right, position.y);

                if(transform.eulerAngles.x > maxRotationX || transform.eulerAngles.x < minRotationX)
                    transform.RotateAround(playerPosition, transform.right, -position.y);
            }
        }

        public void OnBeginPlayerDrag(Vector3 position)
        {
            CameraRotation = !PlayerIsMoving();
        }

        public void OnEndWorldDrag(Vector2 position)
        {
            CameraRotation = false;
        }
    }
}

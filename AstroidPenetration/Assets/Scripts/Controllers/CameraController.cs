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

        private Vector3 distanceVelocity;
        private Vector3 positionVelocity;
        private Vector3 rotationVelocity;
        private Vector3 offsetVelocity;

        [SerializeField]
        private Vector3 offSet = Vector3.up;
        [SerializeField]
        private float maxDistance = 5f;
        [SerializeField]
        private float minDistance = 2f;
        [SerializeField]
        private float distanceSmoothTime = .5f;
        [SerializeField]
        private float rotationSmoothTime = .5f;
        [SerializeField]
        private float positionSmoothTime = .5f;
        [SerializeField]
        private float offsetSmoothTime = .1f;
        [SerializeField]
        private LayerMask groudLayerMask;


        private Vector3 CurrentLookAt { get; set; }
        private Vector3 CurrentPosition { get; set; }
        private Vector3 CurrentOffset { get; set; }
        private Vector3 TargetOffset;

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

            CurrentLookAt = PlayerPosition();
            CurrentPosition = transform.position;
            yield return null;
        }

        private void LateUpdate()
        {
            Vector3 playerPosition = PlayerPosition();

            CurrentLookAt = Vector3.SmoothDamp(CurrentLookAt, playerPosition+ PlayerForward(), ref rotationVelocity, rotationSmoothTime);
            
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


            CurrentOffset = Vector3.SmoothDamp(CurrentOffset, TargetOffset, ref offsetVelocity, offsetSmoothTime);

            transform.position = Vector3.SmoothDamp(transform.position, CurrentPosition + CurrentOffset, ref positionVelocity, positionSmoothTime);
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

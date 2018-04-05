using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    [RequireComponent(typeof(Camera))]
    public class CameraController: BaseController
    {
        private Func<Vector3> PlayerPosition;
        private Func<Vector3> PlayerForward;

        private Vector3 distanceVelocity;
        private Vector3 rotationVelocity;

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

        private Vector3 CurrentLookAt { get; set; }
        private Vector3 CurrentPosition { get; set; }

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

            CurrentLookAt = Vector3.SmoothDamp(CurrentLookAt, playerPosition, ref rotationVelocity, rotationSmoothTime);
            transform.LookAt(CurrentLookAt);


            float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(playerPosition.x, playerPosition.z));

            if(distance > maxDistance)
                CurrentPosition = Vector3.SmoothDamp(CurrentPosition, playerPosition, ref distanceVelocity, distanceSmoothTime);

            if(distance < minDistance)
            {
                Debug.Log(PlayerForward());
             //   CurrentPosition = Vector3.SmoothDamp(CurrentPosition, playerPosition, ref distanceVelocity, distanceSmoothTime);
            }

            transform.position = CurrentPosition+ offSet;
        }
    }
}

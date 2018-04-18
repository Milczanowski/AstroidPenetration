using Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController: BaseController, IEditorSerializable
    {
        [SerializeField]
        private CharacterController CharacterController = null;

        [SerializeField]
        private float moveSpeed = .5f;
        [SerializeField]
        private float rotationSmoothTime = .5f;

        private float angleVelocity = 0;

        private Vector3 TartgetPosition { get; set; }
        private float CurrnetAngle { get; set; }

        private float TartgetAngle { get; set; }
        public bool IsMoving { get; private set; }
             

        private void Move(Vector3 position)
        {
            TartgetPosition = position;
            TartgetAngle = Mathf.Rad2Deg * Mathf.Atan2(TartgetPosition.x - transform.position.x, TartgetPosition.z - transform.position.z);
        }

        private void Update()
        {
            CurrnetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TartgetAngle, ref angleVelocity, rotationSmoothTime);
            transform.eulerAngles = new Vector3(0, CurrnetAngle, 0);

            CharacterController.SimpleMove((TartgetPosition - transform.position).normalized * moveSpeed);
            IsMoving = CharacterController.velocity.magnitude > 1;
            CharacterController.Move(Vector3.down);
        }

        public void InitReference()
        {
            CharacterController = GetComponent<CharacterController>();
        }

        protected override IEnumerator Init()
        {
            GetController<GameplayController>().OnMove += Move;
            TartgetPosition = transform.position;
            TartgetAngle = CurrnetAngle = Mathf.Rad2Deg * Mathf.Atan2(TartgetPosition.x, TartgetPosition.z);
            yield return null;
        }
    }
}

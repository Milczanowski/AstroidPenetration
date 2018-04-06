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

        private Vector3 rotationVelocity = Vector3.zero;

        private Vector3 TartgetPosition { get; set; }
        private Vector3 CurrnetLookAt { get; set; }

        private void Move(Vector3 position)
        {
            TartgetPosition = position;
        }

        private void Update()
        {
            CurrnetLookAt = Vector3.SmoothDamp(CurrnetLookAt, new Vector3(TartgetPosition.x, transform.position.y, TartgetPosition.z), ref rotationVelocity, rotationSmoothTime);
            transform.LookAt(CurrnetLookAt + transform.forward);

            if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(TartgetPosition.x, TartgetPosition.z)) > .1f)
                CharacterController.Move((TartgetPosition - transform.position).normalized * moveSpeed * Time.deltaTime);
        }

        public void InitReference()
        {
            CharacterController = GetComponent<CharacterController>();
        }

        protected override IEnumerator Init()
        {
            GetController<GameplayController>().onMove += Move;
            CurrnetLookAt = transform.position;
            yield return null;
        }
    }
}

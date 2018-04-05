using System.Collections;
using Assets.Scripts.Inputs;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController: BaseController, IEditorSerializable
    {
        [SerializeField]
        private CharacterController CharacterController = null;

        [SerializeField]
        private float speed = .5f;

        private Vector3 TartgetPosition { get; set; }

        private void Move(Vector3 position)
        {
            TartgetPosition = position;
        }

        private void Update()
        {
            if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(TartgetPosition.x, TartgetPosition.z)) > .1f)
                CharacterController.Move((TartgetPosition - transform.position).normalized * speed);
        }

        public void InitReference()
        {
            CharacterController = GetComponent<CharacterController>();
        }

        protected override IEnumerator Init()
        {
            WorldInput.onClick += Move;
            yield return null;
        }
    }
}

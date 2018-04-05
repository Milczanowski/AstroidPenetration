using Assets.Scripts.Inputs;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController: MonoBehaviour, IEditorSerializable
    {
        [SerializeField]
        private CharacterController CharacterController = null;

        private Vector3 TartgetPosition { get; set; }


        private void Start()
        {
            WorldInput.onClick += Move;
        }

        private void Move(Vector3 position)
        {
            TartgetPosition = position;
        }

        private void Update()
        {
            if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z),new Vector2(TartgetPosition.x, TartgetPosition.z)) > .1f)
            {
                Debug.DrawLine(transform.position, TartgetPosition, Color.blue);
                CharacterController.Move((TartgetPosition -transform.position).normalized);
            }
        }

        public void InitReference()
        {
            CharacterController = GetComponent<CharacterController>();
        }
    }
}

using Assets.Scripts.Utils;
using System;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    [RequireComponent(typeof(Animator))]
    public class AnimatedEffect:Effect, IEditorSerializable
    {
        [SerializeField]
        Animator animator = null;

        public override void Show()
        {
            gameObject.SetActive(true);
            animator.Play("");
        }

        public override void Show(Vector3 position)
        {
            transform.position = position;
            Show();
        }
        public void InitReference()
        {
            animator = GetComponent<Animator>();
        }

    }
}

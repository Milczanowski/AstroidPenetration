using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    class SimpleArrowEffect:Effect
    {
        [SerializeField]
        private Transform arrowsParrent = null;

        [SerializeField]
        private float time = 0.5f;

        [SerializeField]
        private float speed = 1;

        public override void Show()
        {
            StopAllCoroutines();
            gameObject.SetActive(true);
            StartCoroutine(Rotate());
        }

        public override void Show(Vector3 position)
        {
            transform.position = position;
            Show();
        }

        private IEnumerator Rotate()
        {
            float startTime = 0;

            while(startTime <time)
            {
                startTime += Time.deltaTime;
                arrowsParrent.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
                yield return null;
            }

            gameObject.SetActive(false);

            End();
        }
    }
}

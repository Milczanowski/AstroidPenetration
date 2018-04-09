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

        public override void Show(Vector3 position, Vector3 rotation)
        {
            Debug.Log(rotation);
            transform.up = rotation;
        }

        private IEnumerator Rotate()
        {
            float startTime = 0;

            arrowsParrent.localScale = Vector3.one;
            float s = Random.Range(0, 10) > 5f ? speed : -speed;

            while(startTime <time)
            {
                startTime += Time.deltaTime;
                arrowsParrent.Rotate(new Vector3(0, s * Time.deltaTime, 0));

                float scale = 1 - (startTime / time);
                arrowsParrent.localScale = new Vector3(scale, scale, scale);

                yield return null;
            }

            gameObject.SetActive(false);

            End();
        }
    }
}

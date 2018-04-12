using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class BaseGUI:MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup CanvasGroup = null;
        [SerializeField]
        private CanvasScaler CanvasScaler = null;

        private float visibleVelocity = 0;

        public bool Interactable
        {
            get
            {
                return CanvasGroup.interactable;
            }
            set
            {
                CanvasGroup.interactable = value;
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
            StartCoroutine(SetVisible(1, 0.05f,()=>{ }));
        }

        public void Hide()
        {
            StartCoroutine(SetVisible(0, 0.05f, ()=>
            {
                gameObject.SetActive(false);
            }));
        }

        public void Scale(float value)
        {
            CanvasScaler.referenceResolution = new Vector2(1920, 1080) * value;
        }

        private IEnumerator SetVisible(float value, float time, Action onEnd)
        {
            CanvasGroup.alpha = value;
            yield return null;
            //while(CanvasGroup.alpha!=value)
            //{
            //    CanvasGroup.alpha = Mathf.SmoothDamp(CanvasGroup.alpha, value, ref visibleVelocity, time);
            //    yield return null;
            //}
            if(onEnd != null)
                onEnd.Invoke();
        }

    }
}

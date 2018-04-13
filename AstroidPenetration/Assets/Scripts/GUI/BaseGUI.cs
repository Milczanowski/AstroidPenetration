using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class BaseGUI<T>:MonoBehaviour where T:BaseGUI<T>
    {
        public static T Instance { get; private set; }

        [SerializeField]
        private CanvasGroup CanvasGroup = null;
        [SerializeField]
        private CanvasScaler CanvasScaler = null;

        protected virtual void Awake()
        {
            if(Instance != null)
                throw new Exception("Buplicate GUI");

            Instance = (T)this;
        }

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

        public void Show(float alpha = 1, float time = .05f, bool enable = true)
        {
            gameObject.SetActive(enable);
            StartCoroutine(SetVisible(alpha, 0.05f,()=>{ }));
        }

        public void Hide(float alpha = 0, float time = .05f, bool enable = false)
        {
            StartCoroutine(SetVisible(alpha, 0.05f, ()=>
            {
                gameObject.SetActive(enable);
            }));
        }

        public void Scale(float value)
        {
            CanvasScaler.referenceResolution = new Vector2(1920, 1080) * value;
        }

        private IEnumerator SetVisible(float value, float time, Action onEnd)
        {
            CanvasGroup.alpha = value; // <- TODO animation? 
            yield return null;
            if(onEnd != null)
                onEnd.Invoke();
        }

    }
}

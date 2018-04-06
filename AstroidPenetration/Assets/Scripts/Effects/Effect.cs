using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public abstract class Effect: MonoBehaviour
    {
        private static Dictionary<EffectType, Effect> Effects = new Dictionary<EffectType, Effect>();

        public event Delegates.OnEnd OnEnd;

        [SerializeField]
        private EffectType Type = EffectType.None;

        protected virtual void Awake()
        {
            if(!Effects.ContainsKey(Type))
                Effects.Add(Type, this);
            else
            {
                Debug.LogError("Doubled effect: " + Type + " " + name);
                Destroy(gameObject);
            }

            gameObject.SetActive(false);
        }

        protected void End()
        {
            if(OnEnd != null)
                OnEnd.Invoke();
        }

        protected virtual void OnDestroy()
        {
            Effects.Remove(Type);
        }

        public abstract void Show();

        public abstract void Show(Vector3 position);

        public static Effect GetEffect(EffectType type)
        {
            if(Effects.ContainsKey(type))
                return Effects[type];

            return null;
        }
    }
}

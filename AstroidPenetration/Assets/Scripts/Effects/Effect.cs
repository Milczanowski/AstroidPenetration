using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public abstract class Effect: MonoBehaviour
    {
        private static Dictionary<EffectType, Effect> Effects = new Dictionary<EffectType, Effect>();

        [SerializeField]
        private EffectType Type;

        protected virtual void Awake()
        {
            if(!Effects.ContainsKey(Type))
                Effects.Add(Type, this);
            else
            {
                Debug.LogError("Doubled effect: " + Type + " " + name);
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            Effects.Remove(Type);
        }

        public abstract void Show();

        public abstract void Show(Vector3 position);
    }
}

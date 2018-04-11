using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inputs
{
    public class BaseInput:MonoBehaviour
    {
        private static Dictionary<Type, Func<bool>> isEnabled = new Dictionary<Type, Func<bool>>();

        private Type Type { get; set; }

        protected bool IsInputEnabled
        {
            get
            {
                return isEnabled[Type]();
            }
        }

        protected virtual void Awake()
        {
            Type = GetType();

            if(!isEnabled.ContainsKey(Type))
                isEnabled.Add(Type, () => { return false; });
        }

        public static void SetEnabledCondition<T>(Func<bool> condition) where T : BaseInput
        {
            if(isEnabled.ContainsKey(typeof(T)))
                isEnabled.Remove(typeof(T));

            isEnabled.Add(typeof(T), condition);
        }
    }
}

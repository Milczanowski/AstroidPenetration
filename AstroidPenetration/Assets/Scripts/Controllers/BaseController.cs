using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public abstract class BaseController: MonoBehaviour
    {
        private static Dictionary<Type, BaseController> Controllers = new Dictionary<Type, BaseController>();

        protected virtual void Awake()
        {
            if(Controllers.ContainsKey(GetType()))
                throw new Exception("Doubled controller: " + GetType());

            Controllers.Add(GetType(), this);
            enabled = false;
        }

        protected abstract IEnumerator Init();

        protected virtual void Start()
        {
        }

        protected virtual void OnDestroy()
        {
            Controllers.Remove(GetType());
        }

        protected T GetController<T>() where T: BaseController
        {
            if(Controllers.ContainsKey(typeof(T)))
                return Controllers[typeof(T)] as T;

            return default(T);
        }

        public static IEnumerator InitAll()
        {
            foreach(Type key in Controllers.Keys)
                yield return Controllers[key].Init();

            foreach(Type key in Controllers.Keys)
                Controllers[key].enabled = true;
        }

        public static void Clear()
        {
            Controllers.Clear();
        }       
    }
}

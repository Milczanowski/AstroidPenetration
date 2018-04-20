using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Assets.Scripts.Obserwers;

namespace Assets.Scripts.Controllers
{
    public abstract class BaseController: MonoBehaviour
    {
        private static Dictionary<Type, BaseController> Controllers = new Dictionary<Type, BaseController>();

        private ObserverController Observer { get; set; }

        protected virtual void Awake()
        {
            if(Controllers.ContainsKey(GetType()))
                throw new Exception("Doubled controller: " + GetType());

            Observer = new ObserverController(this);
            Controllers.Add(GetType(), this);
            enabled = false;
        }

        protected abstract IEnumerator Init();

        protected virtual IEnumerator InitObservers()
        {
            yield return Observer.Bind();
        }

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
                yield return Controllers[key].InitObservers();

            foreach(Type key in Controllers.Keys)
                Controllers[key].enabled = true;
        }

        public static void Clear()
        {
            Controllers.Clear();
        }       

        public void AddObserver(IObserver observer)
        {
            Observer.AddObserver(observer);
        }
    } 
}

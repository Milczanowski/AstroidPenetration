using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GUI.Menu.Options
{
    public abstract class BaseOption<T>: MonoBehaviour where T: struct
    {
        public Delegates.MenuOptionInput<T> OnValueChange = (value)=>{ };

        protected void Invoke(T value)
        {
            if(OnValueChange != null)
                OnValueChange.Invoke(value);
        }

        protected virtual void Start()
        {
            Init();
        }

        public abstract void SetValue(T value);

        public abstract void Init();
    }
}

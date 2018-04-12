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
        [SerializeField]
        public string OptionName = null;

        public Delegates.MenuOptionInput<T> OnValueChange = (value)=>{ };

        protected void Invoke(T value)
        {
            if(OnValueChange != null)
                OnValueChange.Invoke(value);
        }

        public abstract void SetValue(T value);
    }
}

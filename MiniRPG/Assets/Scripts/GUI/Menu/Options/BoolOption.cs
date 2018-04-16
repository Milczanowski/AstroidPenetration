using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.GUI.Menu.Options
{
    class BoolOption:BaseOption<bool>
    {
        [SerializeField]
        private Toggle Toggle = null;

        public override void Init()
        {
            Toggle.onValueChanged.AddListener(Invoke);
        }

        public override void SetValue(bool value)
        {
            Toggle.isOn = value;
        }
    }
}

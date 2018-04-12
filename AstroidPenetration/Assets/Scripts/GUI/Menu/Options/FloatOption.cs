using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Menu.Options
{
    class FloatOption:BaseOption<float>
    {
        [SerializeField]
        private Slider Slider= null;

        public override void SetValue(float value)
        {
            Slider.value = value;
        }

        public override void Init()
        {
            Slider.onValueChanged.AddListener(Invoke);
        }
    }
}

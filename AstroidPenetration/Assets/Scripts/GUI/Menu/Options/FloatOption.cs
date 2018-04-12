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

        private void Start()
        {
            Slider.onValueChanged.AddListener((value) =>
            {
                Invoke(value);
            });
        }
    }
}

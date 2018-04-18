using Assets.Scripts.GUI.Menu.Options;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Menu
{
    class MenuGUI:BaseGUI<MenuGUI>
    {
        [SerializeField]
        private Button backButton = null;
        [SerializeField]
        private Button exitButton = null;
        [SerializeField]
        private FloatOption GUISize = null;
        [SerializeField]
        private BoolOption Music = null;
        [SerializeField]
        private BoolOption Sound = null;
        [SerializeField]
        private FloatOption RotationSensitive = null;

        public event Delegates.Action onBackButton = delegate{};
        public event Delegates.Action onExitButton = ()=>{ };
        public event Delegates.GenericValue<float> OnGUISize = delegate{};
        public event Delegates.GenericValue<bool> OnMusic = delegate{};
        public event Delegates.GenericValue<bool> OnSound = delegate{};
        public event Delegates.GenericValue<float> OnRotationSensitive = delegate{};


        protected override void Awake()
        {
            base.Awake();
            backButton.onClick.AddListener(() => { onBackButton.Invoke(); });
            exitButton.onClick.AddListener(() => { onExitButton.Invoke(); });
            GUISize.OnValueChange += (value) => { OnGUISize.Invoke(value); };
            Music.OnValueChange += (value) => { OnMusic.Invoke(value); };
            Sound.OnValueChange += (value) => { OnSound.Invoke(value); };
            RotationSensitive.OnValueChange += (value) => { OnRotationSensitive.Invoke(value); };
        }

        public void SetGUISize(float value)
        {
            GUISize.SetValue(value);
        }

        public void SetMusic(bool value)
        {
            Music.SetValue(value);
        }

        public void SetSound(bool value)
        {
            Sound.SetValue(value);
        }

        public void SetRotationSensitive(float value)
        {
            RotationSensitive.SetValue(value);
        }

    }
}

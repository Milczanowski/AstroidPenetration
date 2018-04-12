using Assets.Scripts.GUI.Menu.Options;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Menu
{
    class MenuGUI:BaseGUI
    {
        [SerializeField]
        private Button backButton = null;
        [SerializeField]
        private Button exitButton = null;
        [SerializeField]
        private FloatOption GUISize = null;

        public event Delegates.MenuInput onBackButton = ()=>{ };
        public event Delegates.MenuInput onExitButton = ()=>{ };
        public event Delegates.MenuOptionInput<float> OnGUISize = (value)=>{ };

        private void Awake()
        {
            backButton.onClick.AddListener(() => { onBackButton.Invoke(); });
            exitButton.onClick.AddListener(() => { onExitButton.Invoke(); });
            GUISize.OnValueChange += (value) => { OnGUISize.Invoke(value); };
        }

        public void SetGUISize(float value)
        {
            GUISize.SetValue(value);
        }
    }
}

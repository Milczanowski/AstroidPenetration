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

        public event Delegates.MenuInput onBackButton = ()=>{ };
        public event Delegates.MenuInput onExitButton = ()=>{ };

        private void Awake()
        {
            backButton.onClick.AddListener(() => { onBackButton.Invoke(); });
            exitButton.onClick.AddListener(() => { onExitButton.Invoke(); });
        }


     
    }
}

﻿using Assets.Scripts.GUI.Menu.Options;
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
        [SerializeField]
        private BoolOption Music = null;
        [SerializeField]
        private BoolOption Sound = null;

        public event Delegates.MenuInput onBackButton = ()=>{ };
        public event Delegates.MenuInput onExitButton = ()=>{ };
        public event Delegates.MenuOptionInput<float> OnGUISize = (value)=>{ };
        public event Delegates.MenuOptionInput<bool> OnMusic = (value)=>{ };
        public event Delegates.MenuOptionInput<bool> OnSound = (value)=>{ };


        private void Awake()
        {
            backButton.onClick.AddListener(() => { onBackButton.Invoke(); });
            exitButton.onClick.AddListener(() => { onExitButton.Invoke(); });
            GUISize.OnValueChange += (value) => { OnGUISize.Invoke(value); };
            Music.OnValueChange += (value) => { OnMusic.Invoke(value); };
            Sound.OnValueChange += (value) => { OnSound.Invoke(value); };
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
    }
}
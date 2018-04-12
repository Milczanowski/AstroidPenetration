using Assets.Scripts.GUI.Menu;
using Assets.Scripts.Models;
using System;
using System.Collections;

namespace Assets.Scripts.Controllers
{
    class SettingsController:SerializeController<Settings>
    {
        private bool ValueChanged { get; set; }

        protected override IEnumerator Init()
        {
            ValueChanged = false;
            FileName = "settings.data";

            yield return Load(() =>
            {
                Instance = new Settings();
                StartCoroutine(base.Save());
            });
        }

        private new void Save()
        {
            if(ValueChanged)
            {
                ValueChanged = false;
                StartCoroutine(base.Save());
            }
        }

        private void LoadValues(MenuGUI menu)
        {
            menu.SetGUISize(Instance.GUISize);
        }

        public void InitSettings(MenuGUI menu)
        {
            LoadValues(menu);

            menu.onBackButton += Save;
            menu.onExitButton += Save;

            menu.OnGUISize += Menu_OnGUISize;
            ValueChanged = false;
        }

        private void Menu_OnGUISize(float value)
        {
            ValueChanged = true;
            Instance.GUISize = value;
        }
    }
}

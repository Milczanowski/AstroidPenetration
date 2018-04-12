using Assets.Scripts.Models;
using System;
using System.Collections;

namespace Assets.Scripts.Controllers
{
    class SettingsController:SerializeController<Settings>
    {
        protected override IEnumerator Init()
        {
            yield return Load(() =>
            {
                Instance = new Settings();
                StartCoroutine(Save());
            });
        }
    }
}

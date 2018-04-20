using System.Collections;
using Assets.Scripts.Dev;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class DevController:BaseController
    {
        protected override IEnumerator Init()
        {
            GameObject dev = new GameObject("DEV");

            dev.AddComponent<FPSCounter>();
            yield return null;
        }

        protected override IEnumerator InitObservers()
        {
            yield return null;
        }
    }
}

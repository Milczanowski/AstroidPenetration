using System.Collections;
using Assets.Scripts.Dev;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class DevController:BaseController
    {
        protected override IEnumerator Init()
        {
            gameObject.AddComponent<FPSCounter>();
            yield return null;
        }
    }
}

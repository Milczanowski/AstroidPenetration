using Assets.Scripts.Inputs;
using Assets.Scripts.Utils;
using System.Collections;

namespace Assets.Scripts.Controllers
{
    class GameplayController:BaseController
    {
        public event Delegates.Vector3Target onMove;

        protected override IEnumerator Init()
        {
            WorldInput.onClick += onMove;
            yield return null;
        }
    }
}

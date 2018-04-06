using Assets.Scripts.Inputs;
using Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class GameplayController:BaseController
    {
        public event Delegates.Vector3Target OnMove;

        protected override IEnumerator Init()
        {
            WorldInput.OnClick += onMove;
            yield return null;
        }

        private void onMove(Vector3 target)
        {
            if(OnMove != null)
                OnMove.Invoke(target);
        }
    }
}

using Assets.Scripts.Inputs;
using Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class GameplayController:BaseController
    {
        public event Delegates.Vector3Target OnMove;
        public event Delegates.Vector3NormalTarget OnShowMark;

        protected override IEnumerator Init()
        {
            WorldInput.OnClickTarget += onMove;
            WorldInput.OnClickTargetNormal += onShowMark;
            yield return null;
        }

        private void onMove(Vector3 target)
        {
            if(OnMove != null)
                OnMove.Invoke(target);
        }

        private void onShowMark(Vector3 target, Vector3 normal)
        {
            if(OnShowMark != null)
                OnShowMark.Invoke(target, normal);
        }
    }
}

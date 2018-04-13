using Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class GameplayController:BaseController
    {
        public event Delegates.OnEnd OnStop;
        public event Delegates.Vector3Target OnMove;
        public event Delegates.Vector3NormalTarget OnShowMark;
        public event Delegates.Vector3Target OnMeleAtack;
        public event Delegates.Vector3Target OnRangeAttack;

        protected override IEnumerator Init()
        {
            InputController inputController = GetController<InputController>();

            inputController.OnClickTarget += onMove;
            inputController.OnClickTargetNormal += onShowMark;
            inputController.OnInventory += OnInventory;
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

        private void OnInventory(int index)
        {

        }
    }
}

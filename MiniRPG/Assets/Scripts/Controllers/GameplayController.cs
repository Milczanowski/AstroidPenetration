using Assets.Scripts.GUI.Game;
using Assets.Scripts.Players;
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

        private PlayerInventory PlayerInventory { get; set; }

        protected override IEnumerator Init()
        {
            InputController inputController = GetController<InputController>();

            inputController.OnClickTarget += onMove;
            inputController.OnClickTargetNormal += onShowMark;
            inputController.OnInventory += OnInventory;

            PlayerInventory = new PlayerInventory(10, 10);// <- TODO should be set by any setup
            PlayerInventory.OnSet += GameGUI.Instance.SetIcon;
            PlayerInventory.InitItem(GetComponent<SaveController>().Instance.Player.Inventory);
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

using Assets.Scripts.GUI.Game;
using Assets.Scripts.Players;
using Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class GameplayController:BaseController
    {
        public event Delegates.Action OnStop;
        public event Delegates.Vector3Target OnMove;
        public event Delegates.Vector3NormalTarget OnShowMark;
        public event Delegates.Vector3Target OnMeleAtack;
        public event Delegates.Vector3Target OnRangeAttack;


        private Player Player { get; set; }

        protected override IEnumerator Init()
        {
            InputController inputController = GetController<InputController>();

            inputController.OnClickTarget += onMove;
            inputController.OnClickTargetNormal += onShowMark;
            inputController.OnInventory += OnInventory;

            Player = new Player();

            Player.Inventory.OnSet += GameGUI.Instance.SetIcon;
            Player.Inventory.OnSetCount += GameGUI.Instance.SetCount;
            Player.Load(GetComponent<SaveController>().Instance.Player);
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

using Assets.Scripts.GUI.Game;
using Assets.Scripts.Players;
using Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class GameplayController:BaseController
    {
        public event Delegates.Action OnStop = delegate{};
        public event Delegates.Vector3Target OnMove= delegate{};
        public event Delegates.Vector3NormalTarget OnShowMark= delegate{};
        public event Delegates.Vector3Target OnMeleAtack= delegate{};
        public event Delegates.Vector3Target OnRangeAttack= delegate{};


        private Player Player { get; set; }

        protected override IEnumerator Init()
        {
            InputController inputController = GetController<InputController>();

            inputController.OnClickTarget += onMove;
            inputController.OnClickTargetNormal += onShowMark;

            Player = new Player();

            inputController.OnInventory += Player.Inventory.OnInventory;
            Player.Inventory.OnSet += GameGUI.Instance.SetInventoryIcon;
            Player.Inventory.OnSetCount += GameGUI.Instance.SetInventoryCount;
            Player.OnHealthChange += GameGUI.Instance.SetHealtValue;
            Player.OnManaChange += GameGUI.Instance.SetManaValue;
            Player.OnExperienceChange += GameGUI.Instance.SetExperienceValue;
            Player.Load(GetComponent<SaveController>().Instance.Player);
            yield return null;
        }

        private void onMove(Vector3 target)
        {
            OnMove.Invoke(target);
        }

        private void onShowMark(Vector3 target, Vector3 normal)
        {
            OnShowMark.Invoke(target, normal);
        }
    }
}

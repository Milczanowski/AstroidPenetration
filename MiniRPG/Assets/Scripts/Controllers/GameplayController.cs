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

        private bool PlayerDrag { get; set; }


        private Player Player { get; set; }

        protected override IEnumerator Init()
        {
            PlayerDrag = false;

            InputController inputController = GetController<InputController>();

            inputController.OnClickTarget += onMove;
            inputController.OnClickTargetNormal += onShowMark;
            inputController.OnPlayerStartDrag += OnPlayerStartDrag;
            inputController.OnEndDrag += OnEndDrag;

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

        private void OnEndDrag(Vector3 target)
        {
            PlayerDrag = false;
        }

        private void OnPlayerStartDrag(Vector3 target)
        {
            PlayerDrag = true;
        }

        private void onMove(Vector3 target)
        {
            if(!PlayerDrag)
                OnMove.Invoke(target);
        }

        private void onShowMark(Vector3 target, Vector3 normal)
        {
            if(!PlayerDrag)
                OnShowMark.Invoke(target, normal);
        }
    }
}

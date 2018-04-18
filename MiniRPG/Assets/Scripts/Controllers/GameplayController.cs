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
            inputController.OnDropItemClick += OnDropItemClick;
            inputController.OnDropItemStartDrag += OnDropItemStartDrag;

            Player = new Player();

            inputController.OnInventory += Player.Inventory.OnInventory;
            Player.Inventory.OnSet += GameGUI.Instance.SetInventoryIcon;
            Player.Inventory.OnSetCount += GameGUI.Instance.SetInventoryCount;
            Player.Inventory.OnInaccessibleHighlight += GameGUI.Instance.SetInaccessibleHighlight;
            Player.Inventory.OnEmptyHighlight += GameGUI.Instance.SetEmptyHighlight;
            Player.Inventory.OnAvailableHighlight += GameGUI.Instance.SetAvailableHighlight;
            Player.Inventory.OnOffHighlight += GameGUI.Instance.OffHighlight;

            Player.OnHealthChange += GameGUI.Instance.SetHealtValue;
            Player.OnManaChange += GameGUI.Instance.SetManaValue;
            Player.OnExperienceChange += GameGUI.Instance.SetExperienceValue;
            Player.Load(GetComponent<SaveController>().Instance.Player);
            yield return null;
        }

        private void OnDropItemStartDrag(Worlds.Items.DropItem dropItem)
        {
            Player.Inventory.Highlight(dropItem.ID);
        }

        private void OnDropItemClick(Worlds.Items.DropItem dropItem)
        {
            if(Player.Inventory.AddItems(dropItem.ID))
            {
                Destroy(dropItem.gameObject);
            }
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

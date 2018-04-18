using Assets.Scripts.GUI.Game;
using Assets.Scripts.Players;
using Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class GameplayController:BaseController
    {
        enum Drag
        {
            None,
            Player,
            Item
        }

        public event Delegates.Action OnStop = delegate{};
        public event Delegates.Vector3Target OnMove= delegate{};
        public event Delegates.Vector3NormalTarget OnShowMark= delegate{};
        public event Delegates.Vector3Target OnMeleAtack= delegate{};
        public event Delegates.Vector3Target OnRangeAttack= delegate{};

        private Player Player { get; set; }

        private Drag CurrentDrag { get; set; }

        protected override IEnumerator Init()
        {
            CurrentDrag = Drag.None;

            InputController inputController = GetController<InputController>();

            inputController.OnClickTarget += onMove;
            inputController.OnClickTargetNormal += onShowMark;
            inputController.OnPlayerStartDrag += OnPlayerStartDrag;
            inputController.OnEndDrag += OnEndDrag;
            inputController.OnDropItemClick += OnDropItemClick;
            inputController.OnDropItemStartDrag += OnDropItemStartDrag;

            Player = new Player();

            inputController.OnInventory += Player.Inventory.OnInventory;

            Player.Inventory.AddEvents(GameGUI.Instance);
            Player.AddEvents(GameGUI.Instance);
            Player.Load(GetComponent<SaveController>().Instance.Player);
            yield return null;
        }

        private void OnDropItemStartDrag(Worlds.Items.DropItem dropItem)
        {
            CurrentDrag = Drag.Item;
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
            switch(CurrentDrag)
            {
                case Drag.Item:
                    {
                        Player.Inventory.OffHighlight(string.Empty);
                    }
                    break;
            }
            CurrentDrag = Drag.None;
        }

        private void OnPlayerStartDrag(Vector3 target)
        {
            CurrentDrag = Drag.Player;
        }

        private void onMove(Vector3 target)
        {
            if(CurrentDrag == Drag.None)
                OnMove.Invoke(target);
        }

        private void onShowMark(Vector3 target, Vector3 normal)
        {
            if(CurrentDrag == Drag.None)
                OnShowMark.Invoke(target, normal);
        }
    }
}

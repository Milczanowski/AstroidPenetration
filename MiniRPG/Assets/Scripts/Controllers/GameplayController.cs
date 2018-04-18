using Assets.Scripts.GUI.Game;
using Assets.Scripts.Players;
using Assets.Scripts.Utils;
using Assets.Scripts.Worlds.Items;
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
            Item, 
            Inventory
        }

        public event Delegates.Action OnStop = delegate{};
        public event Delegates.Vector3Target OnMove= delegate{};
        public event Delegates.Vector3NormalTarget OnShowMark= delegate{};
        public event Delegates.Vector3Target OnMeleAtack= delegate{};
        public event Delegates.Vector3Target OnRangeAttack= delegate{};

        private Player Player { get; set; }

        private Drag CurrentDrag { get; set; }

        private int CurrentInventoryIndex { get; set; }

        public DropItem CurrentDropItem { get; set; }

        protected override IEnumerator Init()
        {
            CurrentDrag = Drag.None;
            CurrentInventoryIndex = -1;
            CurrentDropItem = null;

            InputController inputController = GetController<InputController>();

            inputController.OnClickTarget += onMove;
            inputController.OnClickTargetNormal += onShowMark;
            inputController.OnPlayerStartDrag += OnPlayerStartDrag;
            inputController.OnEndDrag += OnEndDrag;
            inputController.OnDropItemClick += OnDropItemClick;
            inputController.OnDropItemStartDrag += OnDropItemStartDrag;
            inputController.OnInventoryEnter += OnInventoryEnter;
            inputController.OnInventoryExit += OnInventoryExit;
            inputController.OnInventoryStartDrag += OnInventoryStartDrag;
            inputController.OnInventoryDrag += OnInventoryDrag;
            inputController.OnInventoryEndDrag += OnInventoryEndDrag;

            Player = new Player();

            inputController.OnInventory += Player.Inventory.OnInventory;

            Player.Inventory.AddEvents(GameGUI.Instance);
            Player.AddEvents(GameGUI.Instance);
            Player.Load(GetComponent<SaveController>().Instance.Player);
            yield return null;
        }

        private void OnInventoryEndDrag(int index)
        {
            CurrentDrag = Drag.None;
            Player.Inventory.EndDrag(index);
        }

        private void OnInventoryDrag(Vector2 target)
        {
        }

        private void OnInventoryStartDrag(int index)
        {
            CurrentDrag = Drag.Inventory;
            Player.Inventory.StartDrag(index);
        }

        private void OnInventoryExit(int index)
        {
            switch(CurrentDrag)
            {
                case Drag.Item:
                    {
                        CurrentInventoryIndex = -1;
                    }
                    break;
            }
        }

        private void OnInventoryEnter(int index)
        {
            switch(CurrentDrag)
            {
                case Drag.Item:
                    {
                        CurrentInventoryIndex = index;
                    }                
                    break;
                case Drag.Inventory:
                    {
                        Player.Inventory.SetSelected(index);
                    }break;
            }
        }

        private void OnDropItemStartDrag(Worlds.Items.DropItem dropItem)
        {
            CurrentDrag = Drag.Item;
            CurrentDropItem = dropItem;
            Player.Inventory.Highlight(dropItem.ID);
        }

        private void OnDropItemClick(Worlds.Items.DropItem dropItem)
        {
            if(Player.Inventory.AddItem(dropItem.ID))
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
                        if(CurrentDropItem!=null &&CurrentInventoryIndex >=0)
                            if(Player.Inventory.AddItem(CurrentDropItem.ID, CurrentInventoryIndex,1))                            
                                Destroy(CurrentDropItem.gameObject);

                        CurrentDropItem = null;
                        CurrentInventoryIndex = -1;

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

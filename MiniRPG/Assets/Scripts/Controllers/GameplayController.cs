using Assets.Scripts.GUI.Game;
using Assets.Scripts.Obserwers;
using Assets.Scripts.Players;
using Assets.Scripts.Utils;
using Assets.Scripts.Worlds.Items;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class GameplayController:BaseController, WorldInputController.IWorldClick, WorldInputController.IBeginPlayerDrag, WorldInputController.IEndWorldDrag, WorldInputController.IItemClick,
        WorldInputController.IBeginItemDrag, InventoryController.IInventoryEnter, InventoryController.IInventoryExit, InventoryController.IInventoryBeginDrag, InventoryController.IInventoryEndDrag, WorldInputController.IWorldEnter, InventoryController.IInventory
    {
        public interface IStopMove:IObserver { void OnStopMove(); }
        public interface IMove    :IObserver { void OnMove(Vector3 position); }
        public interface IShowMark:IObserver { void OnShowMark(Vector3 position, Vector3 normal); }

        enum Drag
        {
            None,
            Player,
            Item, 
            Inventory
        }

        [Obserable(typeof(IStopMove))]
        private event Delegates.Action OnStopMove = delegate{};
        [Obserable(typeof(IMove))]
        private event Delegates.Vector3Target OnMove= delegate{};
        [Obserable(typeof(IShowMark))]
        private event Delegates.Vector3NormalTarget OnShowMark= delegate{};
        private event Delegates.Vector3Target OnMeleAtack= delegate{};
        private event Delegates.Vector3Target OnRangeAttack= delegate{};

        private Player Player { get; set; }

        private Drag CurrentDrag { get; set; }
        private int CurrentInventoryIndex { get; set; }
        private DropItem CurrentDropItem { get; set; }
        protected override IEnumerator Init()
        {
            CurrentDrag = Drag.None;
            CurrentInventoryIndex = -1;
            CurrentDropItem = null;

            Player = new Player();


            GetComponent<WorldInputController>().AddObserver(this);

            InventoryController inventoryController = GetComponent<InventoryController>();
            inventoryController.AddObserver(this);
            Player.Inventory.AddObserver(inventoryController);
            Player.AddObserver(GameGUI.Instance);
            yield return  Player.Bind();


            Player.Load(GetComponent<SaveController>().Instance.Player);
            yield return null;
        }
     
        public void OnWorldClick(Vector3 position, Vector3 normal)
        {
            if(CurrentDrag == Drag.None)
            {
                OnMove.Invoke(position);
                OnShowMark.Invoke(position, normal);
            }
        }
        public void OnBeginPlayerDrag(Vector3 position)
        {
            CurrentDrag = Drag.Player;
        }
        public void OnEndWorldDrag(Vector2 position)
        {
            switch(CurrentDrag)
            {
                case Drag.Item:
                    {
                        if(CurrentDropItem != null && CurrentInventoryIndex >= 0)
                            if(Player.Inventory.AddItem(CurrentDropItem.ID, CurrentInventoryIndex, 1))
                                Destroy(CurrentDropItem.gameObject);

                        CurrentDropItem = null;
                        CurrentInventoryIndex = -1;

                        Player.Inventory.OffHighlight(string.Empty);
                    }
                    break;
            }
            CurrentDrag = Drag.None;
        }
        public void OnItemClick(DropItem item)
        {
            if(Player.Inventory.AddItem(item.ID))
            {
                Destroy(item.gameObject);
            }
        }
        public void OnBeginItemDrag(DropItem item)
        {
            CurrentDrag = Drag.Item;
            CurrentDropItem = item;
            Player.Inventory.Highlight(item.ID);
        }
        public void OnInventoryEnter(int index)
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
                    }
                    break;
            }
        }
        public void OnInventoryExit(int index)
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
        public void OnInventoryBeginDrag(int index)
        {
            CurrentDrag = Drag.Inventory;
            Player.Inventory.StartDrag(index);
        }
        public void OnInventoryEndDrag(int index)
        {
            CurrentDrag = Drag.None;
            Player.Inventory.EndDrag(index);
        }
        public void OnWorldEnter(Vector2 position)
        {
            switch(CurrentDrag)
            {
                case Drag.Inventory:
                    {
                        Player.Inventory.SetSelected(-1);
                    }
                    break;
            }
        }
        public void OnInventory(int index)
        {
            Player.Inventory.OnInventory(index);
        }
    }
}

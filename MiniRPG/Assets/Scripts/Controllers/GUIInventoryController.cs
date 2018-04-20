using Assets.Scripts.Controllers.Observers;
using Assets.Scripts.GUI.Game;
using Assets.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class GUIInventoryController:BaseController
    {
        [SerializeField]
        private List <InventoryButton> InventoryButtons = new List<InventoryButton>();

        [SerializeField]
        private Color EmptyColor;
        [SerializeField]
        private Color AvailableColor;
        [SerializeField]
        private Color InaccessibleColor;


        private event Delegates.Index OnInventory= delegate{};
        private event Delegates.Index OnInventoryEnter= delegate{};
        private event Delegates.Index OnInventoryExit= delegate{};
        private event Delegates.Index OnInventoryStartDrag= delegate{};
        private event Delegates.Index OnInventoryEndDrag= delegate{};
        private event Delegates.Vector2Target OnInventoryDrag= delegate{};

        protected override IEnumerator Init()
        {
            InventoryButtons = new List<InventoryButton>(GetComponentsInChildren<InventoryButton>());
            yield return null;
        }

        protected override IEnumerable InitObservers()
        {
            foreach(var observer in Observers)
            {
                if(observer is IInventory)
                    OnInventory += (observer as IInventory).OnInventory;

                if(observer is IInventoryEnter)
                    OnInventoryEnter += (observer as IInventoryEnter).OnInventoryEnter;

                if(observer is IInventoryExit)
                    OnInventoryExit += (observer as IInventoryExit).OnInventoryExit;

                if(observer is IInventoryStartDrag)
                    OnInventoryStartDrag += (observer as IInventoryStartDrag).OnInventoryStartDrag;

                if(observer is IInventoryEndDrag)
                    OnInventoryEndDrag += (observer as IInventoryEndDrag).OnInventoryEndDrag;

                if(observer is IInventoryDrag)
                    OnInventoryDrag += (observer as IInventoryDrag).OnInventoryDrag;
            }

            yield return null;
        }

        public interface IInventory:IObserver { void OnInventory(int index); }
        public interface IInventoryEnter:IObserver { void OnInventoryEnter(int index); }
        public interface IInventoryExit:IObserver { void OnInventoryExit(int index); }
        public interface IInventoryStartDrag:IObserver { void OnInventoryStartDrag(int index); }
        public interface IInventoryEndDrag:IObserver { void OnInventoryEndDrag(int index); }
        public interface IInventoryDrag:IObserver { void OnInventoryDrag(Vector2 position); }


    }
}
/
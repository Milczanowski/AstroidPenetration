﻿using Assets.Scripts.GUI.Game;
using Assets.Scripts.Models.Basics;
using Assets.Scripts.Obserwers;
using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class InventoryController:BaseController, IInventory
    {
        private Dictionary<int, InventoryButton>  Inventory { get; set; }

        public interface IInventory:IObserver { void OnInventory(int index); }
        public interface IInventoryEnter:IObserver { void OnInventoryEnter(int index); }
        public interface IInventoryExit:IObserver { void OnInventoryExit(int index); }
        public interface IInventoryBeginDrag:IObserver { void OnInventoryBeginDrag(int index); }
        public interface IInventoryEndDrag:IObserver { void OnInventoryEndDrag(int index); }
        public interface IInventoryDrag:IObserver { void OnInventoryDrag(Vector2 position); }

        [Obserable(typeof(IInventory))]
        private event Delegates.Index OnInventory= delegate{};
        [Obserable(typeof(IInventoryEnter))]
        private event Delegates.Index OnInventoryEnter= delegate{};
        [Obserable(typeof(IInventoryExit))]
        private event Delegates.Index OnInventoryExit= delegate{};
        [Obserable(typeof(IInventoryBeginDrag))]
        private event Delegates.Index OnInventoryBeginDrag= delegate{};
        [Obserable(typeof(IInventoryEndDrag))]
        private event Delegates.Index OnInventoryEndDrag= delegate{};
        [Obserable(typeof(IInventoryDrag))]
        private event Delegates.Vector2Target OnInventoryDrag= delegate{};

        public void OffHighlight(int index)
        {
            //Inventory[index].OffHighlight();
        }

        public void SetAvailableHighlight(int index)
        {
           // Inventory[index].SetHighlight(Color.green);//<-TODO
        }

        public void SetEmptyHighlight(int index)
        {
           // Inventory[index].SetHighlight(Color.gray);//<-TODO
        }

        public void SetInaccessibleHighlight(int index)
        {
           // Inventory[index].SetHighlight(Color.green);//<-TODO
        }

        public void SetInventoryCount(int index, int count)
        {
            //Inventory[index].SetCount(count);
        }

        public void SetInventoryIcon(int index, PrefabInfo info)
        {
          //  Inventory[index].SetIcon(info != null ? ObjectManager.Load<Image>(info.Name, info.BundleID) : null);
        }

        protected override IEnumerator Init()
        {
            Inventory = new Dictionary<int, InventoryButton>();

            foreach(var button in FindObjectsOfType<InventoryButton>())
            {
                button.onPointerClick += (index, data) => { OnInventory.Invoke(index); };
                button.onPointerEnter += (index, data) => { OnInventoryEnter.Invoke(index); };
                button.onPointerExit  += (index, data) => { OnInventoryExit.Invoke(index); };
                button.onBeginDrag    += (index, data) => { OnInventoryBeginDrag.Invoke(index); };
                button.onEndDrag      += (index, data) => { OnInventoryEndDrag.Invoke(index); };
                button.onDrag         += (index, data) => { OnInventoryDrag.Invoke(data.position); };

                Inventory.Add(button.Index, button);
                yield return null;
            }
        }


    }
}

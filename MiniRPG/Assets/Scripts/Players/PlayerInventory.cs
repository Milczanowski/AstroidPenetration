using Assets.Scripts.Spawners;
using Assets.Scripts.Utils;
using System.Collections.Generic;

namespace Assets.Scripts.Players
{
    class PlayerInventory
    {
        public event Delegates.InventoryInput OnEmpty;
        public event Delegates.InventoryInput OnFull;
        public event Delegates.ItemSet OnSet;
        public event Delegates.ItemSetCount OnSetCount;

        private Dictionary<int, InventorySlot> Slots { get; set; }

        public PlayerInventory(int slotCount, int maxWeight)
        {
            Slots = new Dictionary<int, InventorySlot>();
            for(int i = 0; i < slotCount; ++i)
            {
                InventorySlot inventorySlot = new InventorySlot(i, maxWeight);
                inventorySlot.OnSet = (id, item) => { if(OnSet != null) OnSet.Invoke(id, item); };
                inventorySlot.OnSetCount = (id, count) => { if(OnSetCount != null) OnSetCount.Invoke(id, count); };
                Slots.Add(i, inventorySlot);
            }
        }

        public void InitItem(List<Models.Saves.Item> items)
        {
            foreach(var item in items)
            {
                if(Slots.ContainsKey(item.Index))
                    Slots[item.Index].SetItem(ItemSpawner.GetItem(item.ID), item.Count);
                else
                    UnityEngine.Debug.LogError("Inventory not contains slot: " + item.Index);
            }
        }

    }
}

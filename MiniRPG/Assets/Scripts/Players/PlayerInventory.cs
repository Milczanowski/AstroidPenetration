using Assets.Scripts.Models.Saves;
using Assets.Scripts.Spawners;
using Assets.Scripts.Utils;
using System.Collections.Generic;

namespace Assets.Scripts.Players
{
    public class PlayerInventory
    {
        public event Delegates.Index OnEmpty;
        public event Delegates.Index OnFull;
        public event Delegates.IndexPrefabInfo OnSet;
        public event Delegates.IndexCount OnSetCount;
        public event Delegates.InventoryItem OnItemUse; 

        private Dictionary<int, InventorySlot> Slots { get; set; }

        public PlayerInventory(int slotCount, int maxWeight)
        {
            Slots = new Dictionary<int, InventorySlot>();
            for(int i = 0; i < slotCount; ++i)
            {
                InventorySlot inventorySlot = new InventorySlot(i, maxWeight);
                inventorySlot.OnSet = (id, item) => { if(OnSet != null) OnSet.Invoke(id, item); };
                inventorySlot.OnSetCount = (id, count) => { if(OnSetCount != null) OnSetCount.Invoke(id, count); };
                inventorySlot.OnUse = (item) => { if(OnItemUse != null) OnItemUse.Invoke(item); };
                Slots.Add(i, inventorySlot);
            }
        }

        public void InitItem(List<SaveItem> items)
        {
            foreach(var item in items)
            {
                if(Slots.ContainsKey(item.Index))
                    Slots[item.Index].SetItem(ItemSpawner.GetItem(item.ID), item.Count);
                else
                    UnityEngine.Debug.LogError("Inventory not contains slot: " + item.Index);
            }
        }

        public void OnInventory(int index)
        {
            if(Slots.ContainsKey(index))
                Slots[index].Use();
            else
                UnityEngine.Debug.LogError("Inventory not contains slot: " + index);
        }

        public List<SaveItem> GetSave()
        {
            List<SaveItem> saveItems = new List<SaveItem>();
            foreach(int index in Slots.Keys)
            {
                var slot = Slots[index];
                if(!slot.IsEmpty)
                    saveItems.Add(new SaveItem(slot.Item.ID, slot.Count, index));
            }

            return saveItems;
        }
    }
}

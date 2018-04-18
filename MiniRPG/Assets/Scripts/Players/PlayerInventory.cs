using Assets.Scripts.GUI.Game;
using Assets.Scripts.Models.Saves;
using Assets.Scripts.Spawners;
using Assets.Scripts.Utils;
using System.Collections.Generic;

namespace Assets.Scripts.Players
{
    public class PlayerInventory
    {
        public event Delegates.InventoryItem OnItemUse = delegate{};

        private event Delegates.Index OnEmpty = delegate{};
        private event Delegates.Index OnFull= delegate{};
        private event Delegates.IndexPrefabInfo OnSet= delegate{};
        private event Delegates.IndexCount OnSetCount = delegate{};
        private event Delegates.Index OnEmptyHighlight= delegate { };
        private event Delegates.Index OnAvailableHighlight= delegate { };
        private event Delegates.Index OnInaccessibleHighlight= delegate { };
        private event Delegates.Index OnOffHighlight= delegate { };

        private event Delegates.ID HighlightSlot = delegate{};
        private event Delegates.ID OffHighlightSlot = delegate{};


        private Dictionary<int, InventorySlot> Slots { get; set; }

        public PlayerInventory(int slotCount, int maxWeight)
        {
            Slots = new Dictionary<int, InventorySlot>();
            for(int i = 0; i < slotCount; ++i)
            {
                InventorySlot inventorySlot = new InventorySlot(i, maxWeight);
                inventorySlot.OnSet = (id, item) => { OnSet.Invoke(id, item); };
                inventorySlot.OnSetCount = (id, count) => { OnSetCount.Invoke(id, count); };
                inventorySlot.OnUse = (item) => { OnItemUse.Invoke(item); };
                inventorySlot.OnEmptyHighlight += (id) => { OnEmptyHighlight.Invoke(id); };
                inventorySlot.OnAvailableHighlight += (id) => { OnAvailableHighlight.Invoke(id); };
                inventorySlot.OnInaccessibleHighlight += (id) => { OnInaccessibleHighlight.Invoke(id); };
                inventorySlot.OnOffHighlight += (id) => { OnOffHighlight.Invoke(id); };

                HighlightSlot += inventorySlot.Highlight;
                OffHighlightSlot += inventorySlot.OffHighlight;

                Slots.Add(i, inventorySlot);
            }
        }
        
        public void AddEvents(IInventory inventory)
        {
            OnSet += inventory.SetInventoryIcon;
            OnSetCount += inventory.SetInventoryCount;
            OnInaccessibleHighlight += inventory.SetInaccessibleHighlight;
            OnEmptyHighlight += inventory.SetEmptyHighlight;
            OnAvailableHighlight += inventory.SetAvailableHighlight;
            OnOffHighlight += inventory.OffHighlight;
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

        public bool AddItems(string id, int count = 1)
        {       
            foreach(var slot in Slots.Values)
            {
                if(slot.ItemID == id)
                {
                    if(slot.AddItem(count))
                        return true;
                }
            }

            foreach(var slot in Slots.Values)
            {
                if(slot.IsEmpty)
                {
                    slot.SetItem(ItemSpawner.GetItem(id), count);
                    return true;
                }
            }

            return false;
        }

        public void Highlight(string id)
        {
            HighlightSlot.Invoke(id);
        }

        public void OffHighlight(string id)
        {
            OffHighlightSlot.Invoke(id);
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

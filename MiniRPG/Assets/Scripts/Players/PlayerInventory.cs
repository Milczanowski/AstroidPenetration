using Assets.Scripts.GUI.Game;
using Assets.Scripts.Models.Basics;
using Assets.Scripts.Models.Saves;
using Assets.Scripts.Models.World.Items;
using Assets.Scripts.Obserwers;
using Assets.Scripts.Spawners;
using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Players
{
    public class PlayerInventory: IBindable, IObserable
    {
        public interface IUserItem:IObserver { void OnUseItem(BaseItem baseItem); }

        [Obserable(typeof(IUserItem))]
        private event Delegates.InventoryItem OnUseItem = delegate{};

        public interface ISlotEmpty:IObserver { void OnSlotEmpty(int index); }
        public interface ISlotFull:IObserver { void OnSlotFull(int index); }
        public interface ISlotEmptyHighlight:IObserver { void OnSlotEmptyHighlight(int index); }
        public interface ISlotAvailableHighlight:IObserver { void OnSlotAvailableHighlight(int index); }
        public interface ISlotInaccessibleHighlight:IObserver { void OnSlotInaccessibleHighlight(int index); }
        public interface ISlotOffHighlight:IObserver { void OnSlotOffHighlight(int index); }

        [Obserable(typeof(ISlotEmpty))]
        private event Delegates.Index OnSlotEmpty = delegate{};
        [Obserable(typeof(ISlotFull))]
        private event Delegates.Index OnSlotFull= delegate{};
        [Obserable(typeof(ISlotEmptyHighlight))]
        private event Delegates.Index OnSlotEmptyHighlight= delegate { };
        [Obserable(typeof(ISlotAvailableHighlight))]
        private event Delegates.Index OnSlotAvailableHighlight= delegate { };
        [Obserable(typeof(ISlotInaccessibleHighlight))]
        private event Delegates.Index OnSlotInaccessibleHighlight= delegate { };
        [Obserable(typeof(ISlotOffHighlight))]
        private event Delegates.Index OnSlotOffHighlight= delegate { };

        public interface ISlotSetCount:IObserver { void OnSlotSetCount(int index, int count); }
        public interface ISlotSet:IObserver { void OnSlotSet(int index, PrefabInfo info); }
        public interface ISlotRemove:IObserver { void OnSlotremove(int index, PrefabInfo info); }

        [Obserable(typeof(ISlotSetCount))]
        private event Delegates.IndexCount OnSlotSetCount = delegate{};
        [Obserable(typeof(ISlotSet))]
        private event Delegates.IndexPrefabInfo OnSlotSet= delegate{};
        [Obserable(typeof(ISlotRemove))]
        private event Delegates.IndexPrefabInfo OnSlotRemove= delegate{};

        public interface IHighlightSlot { void OnHighlightSlot(string id); }
        public interface IOffHighlightSlot { void OnOffHighlightSlot(string id); }

        [Obserable(typeof(IHighlightSlot))]
        private event Delegates.ID OnHighlightSlot = delegate{};
        [Obserable(typeof(IOffHighlightSlot))]
        private event Delegates.ID OnOffHighlightSlot = delegate{};

        private Dictionary<int, InventorySlot> Slots { get; set; }

        private int CurrentSelected { get; set; }

        public Observer Observer { get; private set; }


        public PlayerInventory(int slotCount, int maxWeight)
        {

            CurrentSelected = -1;
            Slots = new Dictionary<int, InventorySlot>();
            for(int i = 0; i < slotCount; ++i)
            {
                InventorySlot inventorySlot = new InventorySlot(i, maxWeight);
                inventorySlot.OnSet = (id, item) => { OnSlotSet.Invoke(id, item); };
                inventorySlot.OnSetCount = (id, count) => { OnSlotSetCount.Invoke(id, count); };
                inventorySlot.OnUse = (item) => { OnUseItem.Invoke(item); };
                inventorySlot.OnEmptyHighlight += (id) => { OnSlotEmptyHighlight.Invoke(id); };
                inventorySlot.OnAvailableHighlight += (id) => { OnSlotAvailableHighlight.Invoke(id); };
                inventorySlot.OnInaccessibleHighlight += (id) => { OnSlotInaccessibleHighlight.Invoke(id); };
                inventorySlot.OnOffHighlight += (id) => { OnSlotOffHighlight.Invoke(id); };

                OnHighlightSlot += inventorySlot.Highlight;
                OnOffHighlightSlot += inventorySlot.OffHighlight;

                Slots.Add(i, inventorySlot);
            }

            Observer = new Observer(this);
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
            if(index != CurrentSelected)
            {
                if(Slots.ContainsKey(index))
                    Slots[index].Use();
                else
                    UnityEngine.Debug.LogError("Inventory not contains slot: " + index);
            }
        }

        public bool AddItem(string id, int count = 1)
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

        public bool AddItem(string id, int index, int count = 1)
        {
            if(Slots.ContainsKey(index))
            {
                if(Slots[index].IsEmpty)
                {
                    Slots[index].SetItem(ItemSpawner.GetItem(id), count);
                    return true;
                }
                else if(Slots[index].ItemID == id)
                    return Slots[index].AddItem(count);
            }
            return false;
        }

        public void Highlight(string id)
        {
            OnHighlightSlot.Invoke(id);
        }

        public void OffHighlight(string id)
        {
            OnOffHighlightSlot.Invoke(id);
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

        public void StartDrag(int index)
        {
            if(Slots.ContainsKey(index))
                Slots[index].Highlight();
        }

        public void EndDrag(int index)
        {
            if(index == CurrentSelected)
            {
                if(Slots.ContainsKey(index))
                    Slots[index].OffHighlight(string.Empty);
                CurrentSelected = -1;
                return;
            }

            if(Slots.ContainsKey(index))
            {
                Slots[index].OffHighlight(string.Empty);

                if(Slots.ContainsKey(CurrentSelected))
                {
                    Slots[CurrentSelected].OffHighlight(string.Empty);

                    if(Slots[CurrentSelected].IsEmpty)
                    {
                        Slots[CurrentSelected].SetItem(Slots[index].Item, Slots[index].Count);
                        Slots[index].SetItem(null, 0);
                    }else
                    {
                        if(Slots[index].ItemID == Slots[CurrentSelected].ItemID)
                        {
                            if(Slots[CurrentSelected].AddItem(Slots[index].Count))
                            {
                                OnSlotRemove.Invoke(index, Slots[index].Item.Model);
                                Slots[index].SetItem(null, 0);
                            }
                        }
                    }
                }else if(CurrentSelected== -1)
                {
                    Slots[index].SetItem(null, 0);

                }
            }
            else if(Slots.ContainsKey(CurrentSelected))
                Slots[CurrentSelected].OffHighlight(string.Empty);

            CurrentSelected = -1;
        }

        public void SetSelected(int index)
        {
            CurrentSelected = index;
        }

        public IEnumerator Bind()
        {
            yield return Observer.Bind();
        }

        public void AddObserver(IObserver tartget)
        {
            Observer.AddObserver(tartget);
        }
    }
}

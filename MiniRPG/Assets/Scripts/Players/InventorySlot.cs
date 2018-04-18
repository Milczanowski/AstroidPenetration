using Assets.Scripts.Models.World.Items;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Players
{
    class InventorySlot
    {
        public int ID { get; private set; }
        public int MaxWeight { get; private set; }
        public int Count { get; private set; }
        public BaseItem Item { get; private set; }

        public Delegates.IndexPrefabInfo OnSet = delegate { };
        public Delegates.IndexCount OnSetCount= delegate { };
        public Delegates.Index OnEmpty= delegate { };
        public Delegates.InventoryItem OnUse= delegate { };

        public Delegates.Index OnEmptyHighlight= delegate { };
        public Delegates.Index OnAvailableHighlight= delegate { };
        public Delegates.Index OnInaccessibleHighlight= delegate { };
        public Delegates.Index OnOffHighlight= delegate { };


        public string ItemID
        {
            get
            {
                if(Item != null)
                    return Item.ID;
                return string.Empty;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return Count == 0 || Item == null;
            }
        }

        public InventorySlot(int id, int maxWeight)
        {
            ID = id;
            MaxWeight = maxWeight;
        }

        public void SetItem(BaseItem item, int count = 1)
        {
            Item = item;
            Count = count;

            OnSetInvoke();
            OnSetCountInvoke();
        }

        public bool AddItem(int count =1)
        {
            if((Count+count) <= MaxWeight)
            {
                Count += count;
                OnSetCount.Invoke(ID, Count);
                return true;
            }
            return false;
        }

        public void Use()
        {
            if(Item == null)
                OnEmptyInvoke();
            else
            {
                InvokeOnUse();

                if(Item.RunsOut)
                {
                    --Count;
                    OnSetCountInvoke();

                    if(Count == 0)
                    {
                        Item = null;
                        OnSetInvoke();
                    }
                }
                     
            }
        }

        public void Highlight(string id)
        {
            if(IsEmpty)
            {
                OnEmptyHighlight.Invoke(ID);
                return;
            }
            if(id == ItemID)
                OnAvailableHighlight.Invoke(ID);
            else
                OnInaccessibleHighlight.Invoke(ID);
        }

        public void Highlight()
        {
            OnEmptyHighlight.Invoke(ID);
        }

        public void OffHighlight(string id)
        {
            OnOffHighlight.Invoke(ID);
        }

        private void OnSetInvoke()
        {
            OnSet.Invoke(ID, Item != null ? Item.Icon : null);
        }

        private void OnSetCountInvoke()
        {
            OnSetCount.Invoke(ID, Count);
        }

        private void OnEmptyInvoke()
        {
            OnEmpty.Invoke(ID);
        }

        private void InvokeOnUse()
        {
            OnUse.Invoke(Item);
        }
    }
}

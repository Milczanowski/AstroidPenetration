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

        public Delegates.IndexPrefabInfo OnSet;
        public Delegates.IndexCount OnSetCount;
        public Delegates.Index OnEmpty;

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
            if(count > 0)
            {
                Item = item;
                Count = count;

                OnSetInvoke();
                OnSetCountInvoke();
            }
        }

        public void Use()
        {
            if(Item == null)
                OnEmptyInvoke();
            else
            {
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

        private void OnSetInvoke()
        {
            if(OnSet != null)
                OnSet.Invoke(ID, Item != null ? Item.Icon : null);
        }

        private void OnSetCountInvoke()
        {
            if(OnSetCount != null)
                OnSetCount.Invoke(ID, Count);
        }

        private void OnEmptyInvoke()
        {
            if(OnEmpty != null)
                OnEmpty.Invoke(ID);
        }


    }
}

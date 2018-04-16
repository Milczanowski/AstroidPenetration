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

        public Delegates.ItemSet OnSet;
        public Delegates.ItemSetCount OnSetCount;

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

        private void OnSetInvoke()
        {
            if(OnSet != null)
                OnSet.Invoke(ID, Item.Icon);
        }

        private void OnSetCountInvoke()
        {
            if(OnSetCount != null)
                OnSetCount.Invoke(ID, Count);
        }
    }
}

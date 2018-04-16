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
        
        public void InitItem(List<Models.Saves.Item> items)
        {
            foreach(var item in items)
                OnSet(item.Index, ItemSpawner.GetItem(item.ID).Icon);
        }
    }
}

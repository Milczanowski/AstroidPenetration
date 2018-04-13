using Assets.Scripts.Models.Setups;
using Assets.Scripts.Models.World.Items;
using Assets.Scripts.ResourcesManagers;
using Assets.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Spawners
{
    class ItemSpawner:SetupSpawner<ItemSetups>
    {
        private static Dictionary<string, BaseItem> Items { get; set; }

        public ItemSpawner(ItemSetups setup):base(setup)
        {
            Items = new Dictionary<string, BaseItem>();
        }

        protected override IEnumerator LoadSetup(Delegates.OnProgress onProgress, Delegates.OnEnd onLoaded)
        {
            foreach(var item in Setup.Items)
            {
                BaseItem baseItem = ObjectManager.Load<BaseItem>(item.Prefab.Name, item.Prefab.BundleID);
                if(baseItem != null)
                    Items.Add(item.ID, baseItem);
            }

            yield return null;
        }

        public static BaseItem GetItem(string id)
        {
            if(Items.ContainsKey(id))
                return Items[id];

            throw new NullReferenceException("Item not found: " + id);
        }

    }
}

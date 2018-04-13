using Assets.Scripts.Models.Basics;
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

        public override IEnumerator Load(Delegates.OnProgress onProgress, Delegates.OnEnd onLoaded)
        {
            if(onProgress != null)
                onProgress.Invoke(0);

            foreach(var item in Setup.Items)
            {
                BaseItem baseItem = ObjectManager.Load<BaseItem>(item.Prefab.Name, item.Prefab.BundleID);
                if(baseItem != null)
                    Items.Add(item.ID, baseItem);
            }

            if(onProgress != null)
                onProgress.Invoke(1);

            if(onLoaded != null)
                onLoaded.Invoke();

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

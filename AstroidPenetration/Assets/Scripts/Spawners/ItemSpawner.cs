using Assets.Scripts.Models.Basics;
using Assets.Scripts.Models.Setups;
using Assets.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Spawners
{
    class ItemSpawner:BaseSpawner
    {
        private ItemSetups ItemSetups { get; set; }

        private static Dictionary<string, PrefabInfo> Items { get; set; }

        public ItemSpawner(ItemSetups itemsSetups)
        {
            ItemSetups = itemsSetups;
            Items = new Dictionary<string, PrefabInfo>();
        }

        public override IEnumerator Load(Delegates.OnProgress onProgress, Delegates.OnEnd onLoaded)
        {
            if(onProgress != null)
                onProgress.Invoke(0);

            foreach(var item in ItemSetups.Items)
                Items.Add(item.ID, item.Prefab);

            if(onProgress != null)
                onProgress.Invoke(1);

            if(onLoaded != null)
                onLoaded.Invoke();

            yield return null;
        }

        public static PrefabInfo GetItem(string id)
        {
            if(Items.ContainsKey(id))
                return Items[id];

            return null;
        }

    }
}

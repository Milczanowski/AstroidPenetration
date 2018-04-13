using Assets.Scripts.Models.Setups;
using Assets.Scripts.Utils;
using System;
using System.Collections;

namespace Assets.Scripts.Spawners
{
    class ItemSpawner:BaseSpawner
    {
        private ItemSetups ItemSetups { get; set; }

        public ItemSpawner(ItemSetups itemsSetups)
        {
            ItemSetups = itemsSetups;
        }

        public override IEnumerator Load(Delegates.OnProgress onProgress, Delegates.OnEnd onLoaded)
        {
            throw new NotImplementedException();
        }
    }
}

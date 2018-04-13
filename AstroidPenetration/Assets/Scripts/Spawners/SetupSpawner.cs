using UnityEngine;

namespace Assets.Scripts.Spawners
{
    public abstract class SetupSpawner<T>:BaseSpawner where T: ScriptableObject
    {
        protected T Setup { get; set; }

        public SetupSpawner(T setup)
        {
            Setup = setup;
        }
    }
}

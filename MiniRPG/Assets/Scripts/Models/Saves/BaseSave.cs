using Assets.Scripts.Utils;
using System;

namespace Assets.Scripts.Models.Saves
{
    [Serializable]
    public class BaseSave: ISaveable
    {
        public event Delegates.Action OnSave = delegate{};

        protected internal void InvokeOnSave()
        {
            OnSave.Invoke();
        }
    }
}

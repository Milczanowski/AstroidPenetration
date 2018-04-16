using Assets.Scripts.Utils;
using System;

namespace Assets.Scripts.Models.Saves
{
    [Serializable]
    public class BaseSave: ISaveable
    {
        public event Delegates.Action OnSave;

        protected internal void InvokeOnSave()
        {
            if(OnSave != null)
                OnSave.Invoke();
        }
    }
}

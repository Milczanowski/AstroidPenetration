using Assets.Scripts.Serializers;
using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    class BaseSerializeModel<T>:IStreamSerializable, ISaveable, ILoadable where T : BaseSerializeModel<T>
    {
        public event Delegates.Action OnSave;
        public event Delegates.Action OnLoad;

        public void Deserialzie(Stream stream)
        {
            FieldsSerializer<T>.Deserialzie(this as T, stream);
            InvokeOnLoad();
        }

        public Stream Serialize()
        {
            InvokeOnSave();
            return FieldsSerializer<T>.Serialize(this as T);
        }

        private void InvokeOnSave()
        {
            if(OnSave != null)
                OnSave.Invoke();
        }

        private void InvokeOnLoad()
        {
            if(OnLoad != null)
                OnLoad.Invoke();
        }
    }
}

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
        public event Delegates.Action OnSave = delegate{};
        public event Delegates.Action OnLoad = delegate{};

        public void Deserialzie(Stream stream)
        {
            FieldsSerializer<T>.Deserialzie(this as T, stream);
            OnLoad.Invoke();
        }

        public Stream Serialize()
        {
            OnSave.Invoke();
            return FieldsSerializer<T>.Serialize(this as T);
        }
    }
}

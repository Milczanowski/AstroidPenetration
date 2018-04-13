using Assets.Scripts.Serializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    class BaseSerializeModel<T>:IStreamSerializable where T : BaseSerializeModel<T>
    {
        public void Deserialzie(Stream stream)
        {
            FieldSerializer<T>.Deserialzie(this as T, stream);
        }

        public Stream Serialize()
        {
            return FieldSerializer<T>.Serialize(this as T);
        }
    }
}

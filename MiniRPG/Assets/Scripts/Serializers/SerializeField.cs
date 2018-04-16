using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Serializers
{
    class SerializeField: Attribute
    {
        Type Converter { get; set; }
        public SerializeField()
        {
            Converter = typeof(BaseStringConverter);
        }

        public SerializeField(Type converter)
        {
            Converter = converter;
        }

        public virtual string Serialize(object value)
        {
            return ((IFieldConverter)Activator.CreateInstance(Converter)).ToString(value);
        }

        public virtual object Deserialize(string value, Type type)
        {
            return ((IFieldConverter)Activator.CreateInstance(Converter)).FromString(value, type);
        }
    }
}

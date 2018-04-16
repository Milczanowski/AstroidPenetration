using System;
using System.ComponentModel;

namespace Assets.Scripts.Serializers
{
    class BaseStringConverter:IFieldConverter
    {
        public object FromString(string value, Type type)
        {
            return TypeDescriptor.GetConverter(type).ConvertFrom(value);
        }

        public string ToString(object value)
        {
            return value.ToString();
        }
    }
}

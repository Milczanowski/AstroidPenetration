using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Serializers
{
    interface IFieldConverter
    {
        string ToString(object value);
        object FromString(string value, Type type);
    }
}

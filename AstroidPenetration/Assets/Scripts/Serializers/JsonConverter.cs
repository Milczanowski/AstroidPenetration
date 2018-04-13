using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Serializers
{
    class JsonConverter<T>:IFieldConverter
    {
        public object FromString(string value, Type type)
        {
            return JsonUtility.FromJson<T>(value);
        }

        public string ToString(object value)
        {
            return JsonUtility.ToJson(value);
        }
    }
}

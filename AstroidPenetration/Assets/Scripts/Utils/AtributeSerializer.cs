﻿using System.IO;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    class FieldSerializer<T> where T : ScriptableObject
    {
        public static Stream Serialize(T obj)
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);

            var fields = typeof(T).GetFields();
            foreach(var field in fields)
            {
                streamWriter.WriteLine(field.Name);
                streamWriter.WriteLine(field.GetValue(obj));
            }

            return memoryStream;
        }

        public static void Deserialzie(T obj, Stream stream)
        {
            StreamReader streamReader = new StreamReader(stream);
            while(!streamReader.EndOfStream)
            {
                string name = streamReader.ReadLine();
                FieldInfo info = typeof(T).GetField(name);

                if(info!=null)
                {
                    info.SetValue(obj, streamReader.ReadLine());
                }
            }
        }

    }
}

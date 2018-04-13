using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace Assets.Scripts.Serializers
{
    class FieldSerializer<T> where T : IStreamSerializable
    {
        public static Stream Serialize(T obj)
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);

            var fields = typeof(T).GetFields();
            foreach(var field in fields)
            {
                if(field.IsDefined(typeof(SerializeField), true))
                {
                    SerializeField serializeField = field.GetCustomAttributes(typeof(SerializeField), true)[0] as SerializeField;
                    streamWriter.WriteLine(field.Name);
                    streamWriter.WriteLine(serializeField.Serialize(field.GetValue(obj)));
                }
            }
            streamWriter.Flush();
            memoryStream.Position = 0;
            return memoryStream;
        }

        public static void Deserialzie(T obj, Stream stream)
        {
            StreamReader streamReader = new StreamReader(stream);
            while(!streamReader.EndOfStream)
            {
                string name = streamReader.ReadLine();
                string value = streamReader.ReadLine();
                FieldInfo field = typeof(T).GetField(name);

                if(field!=null)
                {
                    if(field.IsDefined(typeof(SerializeField), true))
                    {
                        SerializeField serializeField = field.GetCustomAttributes(typeof(SerializeField), true)[0] as SerializeField;
                        field.SetValue(obj, serializeField.Deserialize(value, field.FieldType));
                    }
                }
            }
        }

    }
}

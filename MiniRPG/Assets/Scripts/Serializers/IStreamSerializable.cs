using System.IO;

namespace Assets.Scripts.Serializers
{
    interface IStreamSerializable
    {
        Stream Serialize();
        void Deserialzie(Stream streamReader);
    }
}

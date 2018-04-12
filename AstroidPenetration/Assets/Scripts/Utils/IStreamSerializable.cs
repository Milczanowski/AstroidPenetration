using System.IO;

namespace Assets.Scripts.Utils
{
    interface IStreamSerializable
    {
        Stream Serialize();
        void Deserialzie(Stream streamReader);
    }
}

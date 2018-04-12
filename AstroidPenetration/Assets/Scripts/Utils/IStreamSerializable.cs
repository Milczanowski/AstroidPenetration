using System.IO;

namespace Assets.Scripts.Utils
{
    interface IStreamSerializable
    {
        MemoryStream Serialize();
        void Deserialzie(MemoryStream streamReader);
    }
}

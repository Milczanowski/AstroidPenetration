using Assets.Scripts.Serializers;
using System.IO;

namespace Assets.Scripts.Models
{
    public class Settings: IStreamSerializable
    {
        [SerializeField]
        public float GUISize = 1;
        [SerializeField]
        public bool MusicEnable = true;
        [SerializeField]
        public bool SoundEndable = true;
        [SerializeField]
        public string LanguageCode = "EN";

        public void Deserialzie(Stream stream)
        {
            FieldSerializer<Settings>.Deserialzie(this, stream);
        }

        public Stream Serialize()
        {
            return FieldSerializer<Settings>.Serialize(this);
        }
    }
}

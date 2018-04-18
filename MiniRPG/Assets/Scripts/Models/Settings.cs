using Assets.Scripts.Serializers;
using System.IO;

namespace Assets.Scripts.Models
{
    class Settings: BaseSerializeModel<Settings>
    {
        [SerializeField]
        public float GUISize = 1;
        [SerializeField]
        public bool MusicEnable = true;
        [SerializeField]
        public bool SoundEndable = true;
        [SerializeField]
        public string LanguageCode = "EN";
        [SerializeField]
        public float CameraRotationSensitive = 1;
    }
}

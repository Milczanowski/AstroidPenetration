using System.IO;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class Settings: ScriptableObject, IStreamSerializable
    {
        [SerializeField]
        public float _GUISize = 1;
        [SerializeField]
        public bool _MusicEnable = true;
        [SerializeField]
        public bool _SoundEndable = true;
        [SerializeField]
        public string _LanguageCode = "EN";

        public void Deserialzie(MemoryStream memoryStream)
        {
            FieldSerializer<Settings>.Deserialzie(this, memoryStream);
        }

        public MemoryStream Serialize()
        {
            return FieldSerializer<Settings>.Serialize(this);
        }
    }
}

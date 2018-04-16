using Assets.Scripts.Models.Saves;
using Assets.Scripts.Serializers;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Models
{
    class Save: BaseSerializeModel<Save>, ISaveable
    {
        [SerializeField(typeof(JsonConverter<SavePlayer>))]
        public SavePlayer Player = new SavePlayer();

        public Save()
        {
            OnLoad += Save_OnLoad;
        }

        private void Save_OnLoad()
        {
            OnSave += Player.InvokeOnSave;
        }
    }
}

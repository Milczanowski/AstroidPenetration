using Assets.Scripts.Models.Saves;
using Assets.Scripts.Serializers;

namespace Assets.Scripts.Models
{
    class Save: BaseSerializeModel<Save>
    {
        [SerializeField(typeof(JsonConverter<SavePlayer>))]
        public SavePlayer Player = new SavePlayer();
    }
}

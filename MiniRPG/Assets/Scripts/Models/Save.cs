using Assets.Scripts.Models.Saves;
using Assets.Scripts.Serializers;

namespace Assets.Scripts.Models
{
    class Save: BaseSerializeModel<Save>
    {
        [SerializeField(typeof(JsonConverter<Player>))]
        public Player Player = new Player();
    }
}

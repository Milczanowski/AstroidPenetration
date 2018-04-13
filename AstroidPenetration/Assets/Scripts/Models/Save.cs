using Assets.Scripts.Models.Saves;
using Assets.Scripts.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    class Save
    {
        [SerializeField(typeof(JsonConverter<Player>))]
        public Player Player = new Player();
    }
}

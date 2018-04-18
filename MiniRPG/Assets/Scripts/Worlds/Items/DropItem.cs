using Assets.Scripts.Models.World.Items;
using UnityEngine;

namespace Assets.Scripts.Worlds.Items
{
    public class DropItem:MonoBehaviour
    {
        [SerializeField]
        private string id = string.Empty;


        public string ID
        {
            get
            {
                return id;
            }
        }
    }
}

using System.IO;
using UnityEngine;

namespace Assets.Scripts.ResourcesManagers
{
    public class ObjectManager
    {
        public static T Load<T>(string name, string bundelID = "")where T: UnityEngine.Object
        {
            return Resources.Load<T>(Path.Combine(bundelID, name));
        }
    }
}

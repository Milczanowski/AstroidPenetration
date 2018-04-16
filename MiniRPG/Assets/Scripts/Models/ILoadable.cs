using Assets.Scripts.Utils;

namespace Assets.Scripts.Models
{
    interface ILoadable
    {
        event Delegates.Action OnLoad;
    }
}

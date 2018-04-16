using Assets.Scripts.Utils;

namespace Assets.Scripts.Models
{
    interface ISaveable
    {
        event Delegates.Action OnSave;
    }
}

using Assets.Scripts.Models.Basics;

namespace Assets.Scripts.GUI.Game
{
    public interface IInventory
    {
        void SetInventoryIcon(int index, PrefabInfo info);
        void SetInventoryCount(int index, int count);
        void SetEmptyHighlight(int index);
        void SetAvailableHighlight(int index);
        void SetInaccessibleHighlight(int index);
        void OffHighlight(int index);
    }
}

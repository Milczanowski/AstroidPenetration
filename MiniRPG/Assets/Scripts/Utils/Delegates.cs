using Assets.Scripts.Inputs;
using Assets.Scripts.Models.Basics;
using Assets.Scripts.Models.World.Items;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Utils
{
    public class Delegates
    {
        public delegate void Vector3Target(Vector3 target);
        public delegate void Vector2Target(Vector2 target);
        public delegate void Vector3NormalTarget(Vector3 target, Vector3 normal);

        public delegate void FloatValue(float value);
        public delegate void Action();
        public delegate void GUIInput(InputType type, int index, PointerEventData eventData);
        public delegate void GenericValue<T>(T value) where T : struct;
        public delegate void Index(int index);
        public delegate void IndexPrefabInfo(int index, PrefabInfo info);
        public delegate void IndexCount(int index, int Count);
        public delegate void IntValue(int value);
        public delegate void InventoryItem(BaseItem baseItem);
    }
}

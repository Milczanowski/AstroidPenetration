using Assets.Scripts.Inputs;
using Assets.Scripts.Models.Basics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Utils
{
    public class Delegates
    {
        public delegate void Vector3Target(Vector3 target);
        public delegate void Vector3NormalTarget(Vector3 target, Vector3 normal);

        public delegate void OnProgress(float progress);
        public delegate void OnEnd();
        public delegate void GUIInput(InputType type, int index, PointerEventData eventData);
        public delegate void MenuInput();
        public delegate void MenuOptionInput<T>(T value) where T : struct;
        public delegate void InventoryInput(int index);
        public delegate void ItemSet(int index, PrefabInfo info);
        public delegate void ItemSetCount(int index, int Count);
    }
}

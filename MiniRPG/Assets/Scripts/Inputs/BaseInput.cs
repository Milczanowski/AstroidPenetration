using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Inputs
{
    public abstract class BaseInput:MonoBehaviour
    {
        [SerializeField]
        public InputType type = InputType.Inventory;

        [SerializeField]
        public int index = 0;
    }
}

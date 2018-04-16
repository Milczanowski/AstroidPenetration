using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Inputs
{
    public abstract class BaseInput:MonoBehaviour
    {
        [SerializeField]
        protected InputType type = InputType.Inventory;

        [SerializeField]
        protected int index = 0;

        public int Index
        {
            get
            {
                return index;
            }
        }
    }
}

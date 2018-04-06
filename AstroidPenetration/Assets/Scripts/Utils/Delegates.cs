﻿using System;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class Delegates
    {
        public delegate void Vector3Target(Vector3 target);
        public delegate void OnProgress(float progress);
        public delegate void OnEnd();
    }
}

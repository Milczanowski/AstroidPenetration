using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using Assets.Scripts.Effects;

namespace Assets.Editor.Effects
{
    [CustomEditor(typeof(AnimatedEffect))]
    class AnimatedEffectEditor: UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            AnimatedEffect animatedEffect = target as AnimatedEffect;
            animatedEffect.InitReference();
            base.OnInspectorGUI();
        }
    }
}

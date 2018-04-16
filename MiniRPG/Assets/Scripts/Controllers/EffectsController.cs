using Assets.Scripts.Effects;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class EffectsController:BaseController
    {
        private Effect Arrows { get; set; }

        protected override IEnumerator Init()
        {
            Arrows = Effect.GetEffect(EffectType.Arrows);

            GetController<GameplayController>().OnShowMark += ShowMark;
            yield return null;
        }

        private void ShowMark(Vector3 position, Vector3 normal)
        {
            Arrows.Show(position, normal);
        }
    }
}

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

            GetController<GameplayController>().OnMove += ShowArrows;
            yield return null;
        }

        private void ShowArrows(Vector3 position)
        {
            Arrows.Show(position);
        }
    }
}

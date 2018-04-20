using Assets.Scripts.Effects;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class EffectsController:BaseController, GameplayController.IShowMark
    {
        private Effect Arrows { get; set; }

        public void OnShowMark(Vector3 position, Vector3 normal)
        {
            Arrows.Show(position, normal);
        }

        protected override IEnumerator Init()
        {
            Arrows = Effect.GetEffect(EffectType.Arrows);

            GetController<GameplayController>().AddObserver(this);
            yield return null;
        }

        protected override IEnumerator InitObservers()
        {
            yield return null;
        }
    }
}

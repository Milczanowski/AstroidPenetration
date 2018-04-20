using System.Collections;
using Assets.Scripts.Models;

namespace Assets.Scripts.Controllers
{
    class SaveController:SerializeController<Save>
    {
        protected override IEnumerator Init()
        {
            FileName = "save.data";

            yield return Load(() =>
            {
                Instance = new  Save();
                StartCoroutine(base.Save());
            });

            yield return null;
        }

        public new void Save()
        {
            StartCoroutine(base.Save());
        }

        protected override IEnumerable InitObservers()
        {
            yield return null;
        }
    }
}

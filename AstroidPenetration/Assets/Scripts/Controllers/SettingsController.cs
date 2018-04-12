using Assets.Scripts.Models;
using System;
using System.Collections;

namespace Assets.Scripts.Controllers
{
    class SettingsController:BaseController
    {
        private string settings

        public Settings Settings { get; private set; }




        protected override IEnumerator Init()
        {
            throw new NotImplementedException();
        }
    }
}

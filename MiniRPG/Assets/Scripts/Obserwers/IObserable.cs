using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Obserwers
{
    public interface IObserable
    {
        void AddObserver(IObserver tartget);
    }
}

using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Obserwers
{
    public interface IBindable
    {
        IEnumerator Bind();
    }
}

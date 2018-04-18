using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.GUI.Game
{
    public interface IGUI
    {
        void SetHealtValue(int value);
        void SetManaValue(int value);
        void SetExperienceValue(int value);
        void SetLevelValue(int value);
    }
}

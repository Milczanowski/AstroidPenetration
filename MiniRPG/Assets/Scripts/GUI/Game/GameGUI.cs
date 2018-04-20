using Assets.Scripts.Players;
using Assets.Scripts.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Game
{
    public class GameGUI:BaseGUI<GameGUI>, IEditorSerializable, Player.IHealthChange, Player.IManaChange, Player.IExperienceChange, Player.ILevelChange
    {
        [SerializeField]
        private Text Health = null;
        [SerializeField]
        private Text Mana = null;
        [SerializeField]
        private Text Experience = null;
        [SerializeField]
        private Text Level = null;

        public void InitReference()
        {

        }

        public void OnHealthChange(int value)
        {
            Health.text = string.Format("Health: {0}", value);
        }

        public void OnManaChange(int value)
        {
            Mana.text = string.Format("Mana: {0}", value);
        }

        public void OnExperienceChange(int value)
        {
            Experience.text = string.Format("Exp: {0}", value);
        }

        public void OnLevelChange(int value)
        {
            Level.text = string.Format("Level: {0}", value);
        }
    }
}

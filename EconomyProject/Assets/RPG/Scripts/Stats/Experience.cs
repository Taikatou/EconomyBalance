using Assets.RPG.Scripts.Saving;
using System;
using UnityEngine;

namespace Assets.RPG.Scripts.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float _experiencePoints = 0;

        public event Action OnExperienceGained;

        public void GainExperience(float experience)
        {
            _experiencePoints += experience;
            OnExperienceGained?.Invoke();
        }

        public float GetPoints()
        {
            return _experiencePoints;
        }

        public object CaptureState()
        {
            return _experiencePoints;
        }

        public void RestoreState(object state)
        {
            _experiencePoints = (float)state;
        }
    }
}
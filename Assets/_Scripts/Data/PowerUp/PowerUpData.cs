using System;
using UnityEngine;

namespace JustGame.Scripts.Data
{
    public class PowerUpData : ScriptableObject
    {
        [Header("Base")]
        public string Name;
        public string Description;
        public Sprite Icon;

        public bool IsActive;
        public Action OnApplyPowerUp;

        protected virtual void OnEnable()
        {
            IsActive = false;
        }

        public virtual void ApplyPowerUp()
        {
            OnApplyPowerUp?.Invoke();
        }
    }
}


using System;
using JustGame.Scripts.RuntimeSet;
using UnityEngine;

namespace JustGame.Scripts.Data
{
    public enum PowerUpType
    {
        PASSIVE,
        ACTIVE,
    }
    public class PowerUpData : ScriptableObject
    {
        [Header("Base")] 
        [SerializeField] protected PowerUpType m_type;
        [SerializeField] protected RuntimeWorldSet m_runtimeWorldSet;
        public string Name;
        public string Description;
        public Sprite Icon;

        public bool RemoveOnActive = true;
        public bool IsActive;
        public PowerUpType Type => m_type;
        
        public Action OnApplyPowerUp;
        
        protected virtual void OnEnable()
        {
            IsActive = false;
            RemoveOnActive = true;
        }

        public virtual void ApplyPowerUp()
        {
            Debug.Log($"Applied power up {Name}");
            OnApplyPowerUp?.Invoke();
            if (RemoveOnActive)
            {
                m_runtimeWorldSet.PowerUpManager.RemovePowerUp(this);
            }
        }
    }
}


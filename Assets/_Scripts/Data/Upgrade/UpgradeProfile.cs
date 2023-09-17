using System;
using UnityEngine;

namespace JustGame.Scripts.Data
{
    public class UpgradeProfile : ScriptableObject
    {
        [Header("General info")]
        public Sprite Icon;
        public string Title;
        public string Description;
        public int XPToUnlock;
        public int CurrentXP;
        public bool IsUnlocked;
        
        public Action OnApplyAction;
        
        private void OnEnable()
        {
            ResetProfile();
        }

        public void SpentXP(int amount)
        {
            if (IsUnlocked) return;
            CurrentXP += amount;
            if (CurrentXP >= XPToUnlock)
            {
                IsUnlocked = true;
                OnApply();
            }
        }
        
        public virtual void OnApply()
        {
            OnApplyAction?.Invoke();
        }
        
        public virtual void ResetProfile()
        {
            CurrentXP = 0;
            IsUnlocked = false;
        }
    }
}

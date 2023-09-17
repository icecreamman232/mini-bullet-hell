using System;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Scriptable Event/Cooldown PowerUp Event")]
    public class CooldownActivePowerUpEvent : FloatEvent
    {
        public Action<float> TriggerCoolDownAction;

        public void SetCoolDown(float value)
        {
            TriggerCoolDownAction?.Invoke(value);
        }
    }
}


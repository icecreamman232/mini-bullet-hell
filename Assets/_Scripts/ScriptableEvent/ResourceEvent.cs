using System;
using UnityEngine;

namespace JustGame.Scripts.ScriptableEvent
{
    [CreateAssetMenu(menuName = "JustGame/Scriptable Event/Resource Event")]
    public class ResourceEvent : ScriptableObject
    {
        [SerializeField] private int m_derbisAmount;
        public int DerbisAmount => m_derbisAmount;

        public Action<int> OnChangeDerbisAmount;

        public Action<int> OnCollectDerbis;

        private void OnEnable()
        {
            m_derbisAmount = 0;
        }

        public void AddDerbis(int value)
        {
            m_derbisAmount += value;
            OnCollectDerbis?.Invoke(value);
            OnChangeDerbisAmount?.Invoke(m_derbisAmount);
        }
    }
}


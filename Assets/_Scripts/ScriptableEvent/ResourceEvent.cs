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

        public void AddDerbis(int value)
        {
            m_derbisAmount += value;
            OnChangeDerbisAmount?.Invoke(m_derbisAmount);
        }
    }
}


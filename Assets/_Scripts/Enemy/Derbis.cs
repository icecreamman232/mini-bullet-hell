using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Items
{
    public class Derbis : Item
    {
        [SerializeField] private float m_liveDuration;
        [SerializeField] private IntEvent m_collectEvent;
        
        private void OnEnable()
        {
            Invoke(nameof(OnSetDestroy),m_liveDuration);
        }

        protected override void OnSetDestroy()
        {
            m_collectEvent.Raise(1);
            base.OnSetDestroy();
        }
    }
}


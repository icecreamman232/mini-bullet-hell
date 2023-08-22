using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Items
{
    public class Derbis : Item
    {
        [SerializeField] private float m_liveDuration;
        [SerializeField] private ResourceEvent m_resourceEvent;
        
        private void OnEnable()
        {
            Invoke(nameof(OnSetDestroy),m_liveDuration);
        }

        protected override void OnSetDestroy()
        {
            m_resourceEvent.AddDerbis(1);
            base.OnSetDestroy();
        }
    }
}


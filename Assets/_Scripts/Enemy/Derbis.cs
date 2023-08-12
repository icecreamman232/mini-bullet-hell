using UnityEngine;

namespace JustGame.Scripts.Items
{
    public class Derbis : Item
    {
        [SerializeField] private float m_liveDuration;

        private void OnEnable()
        {
            Invoke(nameof(OnSetDestroy),m_liveDuration);
        }
    }
}


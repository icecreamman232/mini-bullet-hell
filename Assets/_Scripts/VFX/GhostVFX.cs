using UnityEngine;

namespace JustGame.Scripts.VFX
{
    public class GhostVFX : MonoBehaviour
    {
        [SerializeField] private GameObject m_vfx;
        [SerializeField] private float m_delayPerGhost;
        private bool m_canPlay;
        private float m_delayTimer;
        private Transform m_owner;
        
        public void PlayVFX(Transform owner)
        {
            m_canPlay = true;
            m_delayTimer = m_delayPerGhost;
            m_owner = owner;
            Instantiate(m_vfx, owner.transform.position, owner.rotation, null);
        }

        public void StopVFX()
        {
            m_canPlay = false;
            m_owner = null;
        }

        private void Update()
        {
            if (!m_canPlay
                || m_owner == null) return;
            
            m_delayTimer -= Time.deltaTime;
            if (m_delayTimer <= 0)
            {
                m_delayTimer = m_delayPerGhost;
                Instantiate(m_vfx, m_owner.transform.position, m_owner.rotation, null);
            }
        }
    }
}


using JustGame.Scripts.VFX;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyBrain m_brain;
        [SerializeField] private SpawnVFX m_spawnVFX;
        [SerializeField] private SpriteRenderer m_sprite;
        [SerializeField] private Collider2D m_collider2D;
        
        protected virtual void Start()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            if (m_brain == null)
            {
                m_brain = GetComponentInChildren<EnemyBrain>();
            }

            m_sprite.enabled = false;
            m_collider2D.enabled = false;
            m_spawnVFX.OnFinishVFX += Spawn;
            m_spawnVFX.PlayVFX();
        }

        private void Spawn()
        {
            m_sprite.enabled = true;
            m_collider2D.enabled = true;
            
            if (m_brain != null)
            {
                m_brain.BrainActive = true;
            }
        }
    }
}

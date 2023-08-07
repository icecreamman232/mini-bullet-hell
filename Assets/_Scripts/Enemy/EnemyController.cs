using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyBrain m_brain;

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
            m_brain.BrainActive = true;
        }
    }
}

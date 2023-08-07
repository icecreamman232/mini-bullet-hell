using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] protected float m_moveSpeed;
        [SerializeField] protected Vector2 m_movingDirection;
        [SerializeField] protected bool m_canMove = true;

        protected float m_curSpeed;

        private void Start()
        {
            m_curSpeed = m_moveSpeed;
        }

        public virtual void StartMoving()
        {
            m_canMove = true;
        }

        public virtual void StopMoving()
        {
            m_canMove = false;
        }
        
        public virtual void SetDirection(Vector2 newDirection)
        {
            m_movingDirection = newDirection;
        }

        public virtual void SetOverrideSpeed(float newSpeed)
        {
            m_curSpeed = newSpeed;
        }

        public virtual void ResetSpeed()
        {
            m_curSpeed = m_moveSpeed;
        }
        
        protected virtual void Update()
        {
            if (!m_canMove) return;
            Movement();
        }
        
        protected virtual void Movement()
        {
            transform.Translate(m_movingDirection * (Time.deltaTime * m_curSpeed/10));
        }
    }
}

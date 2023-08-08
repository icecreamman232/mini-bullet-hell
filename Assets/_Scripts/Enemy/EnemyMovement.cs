using System.Collections;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public enum MovementState
    {
        MOVING,
        KNOCK_BACK,
    }
    public class EnemyMovement : MonoBehaviour
    {
        [Header("Common")]
        [SerializeField] protected MovementState m_movementState;
        [SerializeField] protected float m_moveSpeed;
        [SerializeField] protected Vector2 m_movingDirection;
        [SerializeField] protected bool m_canMove = true;
        [Header("KnockBack")]
        [SerializeField] protected bool m_immuneKnockBack;
        [SerializeField] protected float m_deceleration;
        
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
            if (m_movementState == MovementState.KNOCK_BACK) return;
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
            ComputeSpeed();
            Movement();
        }

        protected virtual void ComputeSpeed()
        {
            switch (m_movementState)
            {
                case MovementState.MOVING:
                    break;
                case MovementState.KNOCK_BACK:
                    if (m_curSpeed > 0)
                    {
                        m_curSpeed -= m_deceleration * Time.deltaTime;
                        m_curSpeed = Mathf.Clamp(m_curSpeed, 0, m_moveSpeed);
                    }
                    break;
            }
        }
        
        protected virtual void Movement()
        {
            transform.Translate(m_movingDirection * (Time.deltaTime * m_curSpeed/10));
        }
        
        public void ApplyKnockBack(float knockBackForce, float knockBackDuration)
        {
            if (m_immuneKnockBack) return;

            if (!gameObject.activeSelf) return;
            
            var knockBackDir = m_movingDirection * -1;
            StartCoroutine(KnockBackCoroutine(knockBackForce, knockBackDir, knockBackDuration));
        }

        protected virtual IEnumerator KnockBackCoroutine(float force, Vector2 knockBackDir, float duration)
        {
            if (m_movementState == MovementState.KNOCK_BACK)
            {
                yield break;
            }

            m_movementState = MovementState.KNOCK_BACK;
            m_movingDirection = knockBackDir;
            m_curSpeed = force;
            float timer = 0;
            
            while (timer < duration)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            m_movementState = MovementState.MOVING;
            m_curSpeed = m_moveSpeed;
        }
    }
}
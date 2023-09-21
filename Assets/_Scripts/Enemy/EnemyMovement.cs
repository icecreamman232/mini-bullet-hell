using System.Collections;
using JustGame.Scripts.RuntimeSet;
using JustGame.Scripts.Weapons;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public enum MovementState
    {
        MOVING,
        MOVING_TO_POINT,
        KNOCK_BACK,
    }
    public class EnemyMovement : MonoBehaviour
    {
        [Header("Common")] 
        [SerializeField] protected RuntimeWorldSet m_worldSet;
        [SerializeField] protected MovementState m_movementState;
        [SerializeField] protected float m_moveSpeed;
        [SerializeField] protected Vector2 m_movingDirection;
        [SerializeField] protected bool m_canMove = true;
        [SerializeField] protected bool m_forbiddenMoving;
        [Header("KnockBack")]
        [SerializeField] protected bool m_immuneKnockBack;
        [SerializeField] protected float m_deceleration;
        
        protected float m_curSpeed;
        protected Health m_health;
        protected Vector2 m_movingTarget;

        public float CurrentSpeed => m_curSpeed;
        
        private void Start()
        {
            m_curSpeed = m_moveSpeed;
            m_health = GetComponent<Health>();
            m_health.OnDeath += StopMoving;
        }

        public virtual void StartMoving()
        {
            m_canMove = true;
            m_forbiddenMoving = false;
        }

        public virtual void PauseMoving()
        {
            m_canMove = false;
        }

        public virtual void StopMoving()
        {
            m_canMove = false;
            m_forbiddenMoving = true;
        }
        
        
        public virtual void SetDirection(Vector2 newDirection)
        {
            if (m_movementState == MovementState.KNOCK_BACK) return;
            m_movingDirection = newDirection;
        }

        public virtual void SetTarget(Vector2 newTarget)
        {
            if (m_movementState == MovementState.KNOCK_BACK) return;
            m_movingTarget = newTarget;
            m_movingDirection = (m_movingTarget - (Vector2)transform.position).normalized;
            m_movementState = MovementState.MOVING_TO_POINT;
        }
        public virtual void SetOverrideSpeed(float newSpeed)
        {
            if (newSpeed < 0)
            {
                newSpeed = 0;
            }
            m_curSpeed = newSpeed;
            Debug.Log($"Set speed {m_curSpeed}");
        }

        public virtual void ResetSpeed()
        {
            m_curSpeed = m_moveSpeed;
        }
        
        protected virtual void Update()
        {
            if (m_forbiddenMoving) return;
            if (!m_canMove && m_movementState != MovementState.KNOCK_BACK) return;
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
            switch (m_movementState)
            {
                case MovementState.MOVING_TO_POINT:
                    transform.position = Vector2.MoveTowards(transform.position, m_movingTarget,
                        (Time.deltaTime * m_curSpeed / 10));
                    break;
                case MovementState.MOVING:
                case MovementState.KNOCK_BACK:
                    transform.Translate(m_movingDirection * (Time.deltaTime * m_curSpeed/10));
                    break;
            }

            if (!(m_worldSet.LevelBounds.IsInBounds(transform.position)))
            {
                transform.position = m_worldSet.LevelBounds.InversedPoint(transform.position);
            }
        }
        
        public void ApplyKnockBack(Vector2 knockBackDirection, float knockBackForce, float knockBackDuration)
        {
            if (m_immuneKnockBack) return;

            if (!gameObject.activeSelf) return;

            var knockBackDir = knockBackDirection;//m_movingDirection * -1;
            StartCoroutine(KnockBackCoroutine(knockBackForce, knockBackDir, knockBackDuration));
        }

        protected virtual IEnumerator KnockBackCoroutine(float force, Vector2 knockBackDir, float duration)
        {
            if (m_movementState == MovementState.KNOCK_BACK)
            {
                yield break;
            }

            var initSpeed = m_curSpeed;
            m_movementState = MovementState.KNOCK_BACK;
            var prevDir = m_movingDirection;
            m_movingDirection = knockBackDir;
            m_curSpeed = force;
            float timer = 0;
            
            while (timer < duration)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            m_movingDirection = prevDir;
            m_movementState = MovementState.MOVING;
            m_curSpeed = initSpeed;
        }
    }
}

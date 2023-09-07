using System;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class DroneMovementFlyAround : MonoBehaviour
    {
        [SerializeField] private Transform m_hostObject;
        [SerializeField] private float m_distanceToHost;
        [SerializeField] private float m_moveSpeed;

        private float m_totalAngle;
        private Vector3 m_movingVector;

        private void Start()
        {
            m_movingVector = Vector3.zero;
        }

        public void SetOwner(Transform host)
        {
            m_hostObject = host;
        }

        private void Update()
        {
            if (m_hostObject == null) return;

            m_totalAngle += m_moveSpeed * Time.deltaTime;
            if (m_totalAngle >= 180)
            {
                m_totalAngle = 0;
            }

            m_movingVector.x = Mathf.Cos(m_totalAngle) * m_distanceToHost;
            m_movingVector.y = Mathf.Sin(m_totalAngle) * m_distanceToHost;
            m_movingVector.z = 0;
            
            transform.position = m_hostObject.position + m_movingVector;
        }
    }
}


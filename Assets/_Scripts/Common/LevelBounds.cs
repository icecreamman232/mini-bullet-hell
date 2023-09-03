using JustGame.Scripts.RuntimeSet;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.Common
{
    public class LevelBounds : MonoBehaviour
    {
        [SerializeField] private RuntimeWorldSet m_worldSet;
        [SerializeField] private Vector2 m_topLeft;
        [SerializeField] private Vector2 m_topRight;
        [SerializeField] private Vector2 m_botLeft;
        [SerializeField] private Vector2 m_botRight;
        
        private void Start()
        {
            m_worldSet.SetLevelBounds(this);
        }

        public bool IsInBounds(Vector2 point)
        {
            return ((point.x > m_topLeft.x && point.x < m_botRight.x)
                    && point.y < m_topLeft.y && point.y > m_botRight.y);
        }

        public Vector2 InversedPoint(Vector2 point)
        {
            return new Vector2(-point.x, -point.y);
        }
        
        /// <summary>
        /// Get random point which is within the bounds
        /// </summary>
        /// <returns></returns>
        public Vector2 GetRandomPoint()
        {
            var xCoord = Random.Range(m_topLeft.x, m_topRight.x);
            var yCoord = Random.Range(m_topLeft.y, m_botLeft.y);
            return new Vector2(xCoord, yCoord);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color= Color.blue;
            Gizmos.DrawLine(m_topLeft,m_topRight);
            Gizmos.DrawLine(m_topLeft,m_botLeft);
            Gizmos.DrawLine(m_topRight,m_botRight);
            Gizmos.DrawLine(m_botLeft,m_botRight);
        }
    }
}


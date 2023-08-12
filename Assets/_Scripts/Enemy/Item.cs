using JustGame.Scripts.RuntimeSet;
using UnityEngine;

namespace JustGame.Scripts.Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] protected float m_movingSpeed;
        [SerializeField] protected PlayerComponentSet m_playerComponentSet;
        protected bool m_canMove;

        public virtual void SetCollectByPlayer()
        {
            m_canMove = true;
        }

        protected virtual void Update()
        {
            if (!m_canMove) return;
            transform.position = Vector2.MoveTowards(
                transform.position, 
                m_playerComponentSet.Player.transform.position, 
                Time.deltaTime * m_movingSpeed / 10);

            if (Vector2.Distance(transform.position, m_playerComponentSet.Player.transform.position) <= 0.1f)
            {
                OnSetDestroy();
            }
        }

        protected virtual void OnSetDestroy()
        {
            m_canMove = false;
            Destroy(this.gameObject);
        }
    }
}


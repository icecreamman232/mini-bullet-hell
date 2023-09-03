using System.Collections;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class CanParalyze : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_spriteRenderer;
        private EnemyMovement m_movement;
        private bool m_hasProcess;
        private void Start()
        {
            m_movement = GetComponent<EnemyMovement>();
        }

        public void TriggerParalyze(float duration, Color paralyzeColor)
        {
            Debug.Log($"<color=yellow>Trigger paralyze</color>");
            StartCoroutine(OnTriggerParalyze(duration, paralyzeColor));
        }

        private IEnumerator OnTriggerParalyze(float duration, Color paralyzeColor)
        {
            if (m_hasProcess)
            {
                yield break;
            }

            m_hasProcess = true;
            m_spriteRenderer.color = paralyzeColor;
            m_movement.StopMoving();
            yield return new WaitForSeconds(duration);
            m_spriteRenderer.color = Color.white;
            m_movement.StartMoving();
            m_hasProcess = false;
        }
    }
}


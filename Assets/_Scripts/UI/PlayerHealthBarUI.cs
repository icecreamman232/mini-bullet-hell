using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class PlayerHealthBarUI : MonoBehaviour
    {
        [SerializeField] [Min(0)] private float m_reduceSpeed;
        [SerializeField] private FloatEvent m_healthEvent;
        [SerializeField] private Image m_damageImg;
        [SerializeField] private Image m_currentImg;

        private float m_target;

        private void Start()
        {
            m_healthEvent.AddListener(UpdateHealthBar);
            m_target = 1;
            m_damageImg.fillAmount = 1;
            m_currentImg.fillAmount = 1;
        }

        private void Update()
        {
            if (m_damageImg.fillAmount <= m_target)
            {
                return;
            }

            m_damageImg.fillAmount -= Time.deltaTime * m_reduceSpeed;

            if (m_damageImg.fillAmount <= 0)
            {
                m_damageImg.fillAmount = 0;
            }
        }

        private void UpdateHealthBar(float percent)
        {
            m_target = percent;
            m_currentImg.fillAmount = percent;
        }

        private void OnDestroy()
        {
            m_healthEvent.RemoveListener(UpdateHealthBar);
        }
    }
}


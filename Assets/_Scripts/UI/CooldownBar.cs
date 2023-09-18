using JustGame.Scripts.Data;
using JustGame.Scripts.Managers;
using JustGame.Scripts.ScriptableEvent;
using TMPro;
using UnityEngine;

namespace JustGame.Scripts.UI
{
    public class CooldownBar : MonoBehaviour
    {
        [SerializeField] private CooldownActivePowerUpEvent m_cooldownEvent;
        [SerializeField] private BoolEvent m_cooldownDoneEvent;
        [SerializeField] private TextMeshProUGUI m_abilityName;
        [SerializeField] private Transform m_barTransform;
        [SerializeField] private float m_cooldown;
        private float m_timer;
        private bool m_isCooldown;
        private Vector3 m_scalarVector;
        private readonly string m_coolDownText = "Cooldown";
        private void Awake()
        {
            m_cooldownEvent.TriggerCoolDownAction += SetCoolDown;
            m_cooldownEvent.AddListener(SetTimer);
        }

        private void Update()
        {
            if (!m_isCooldown) return;
            
            m_scalarVector.x = MathHelpers.Remap(m_timer, 0, m_cooldown, 0, 1);
            m_barTransform.localScale = m_scalarVector;
            if (m_timer <= 0)
            {
                m_barTransform.localScale = Vector3.zero;
                m_timer = 0;
                m_abilityName.gameObject.SetActive(false);
                m_cooldownDoneEvent.Raise(true);
                m_isCooldown = false;
            }
        }
        
        [ContextMenu("Test")]
        private void SetCoolDown(float coolDown)
        {
            m_cooldown = coolDown;
            m_barTransform.localScale = Vector3.one;
            m_scalarVector = m_barTransform.localScale;
            m_timer = m_cooldown;
            m_abilityName.gameObject.SetActive(true);
            m_abilityName.text = m_coolDownText;
            m_isCooldown = true;
        }

        private void SetTimer(float value)
        {
            m_timer = value;
        }
        private void OnDestroy()
        {
            m_cooldownEvent.TriggerCoolDownAction -= SetCoolDown;
            m_cooldownEvent.RemoveListener(SetTimer);
        }
    }
}


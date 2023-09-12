using TMPro;
using UnityEngine;

namespace  JustGame.Scripts.Common
{
    public class DamageNumber : MonoBehaviour
    {
        public Animator Animator;
        public TextMeshPro DamageText;
        public float Duration;

        private int m_showAnim = Animator.StringToHash("trigger_Show");
        private int m_showCritAnim = Animator.StringToHash("trigger_ShowCritical");
        
        public void Show(int damageNumber, bool isCritical = false)
        {
            if (isCritical)
            {
                DamageText.text = $"{damageNumber.ToString()}!";
                Animator.SetTrigger(m_showCritAnim);
            }
            else
            {
                DamageText.text = damageNumber.ToString();
                Animator.SetTrigger(m_showAnim);
            }
            Invoke("OnDestroy",Duration);
        }

        public void OnDestroy()
        {
            this.transform.localPosition = Vector3.zero;
            this.gameObject.SetActive(false);
        }
    } 
}


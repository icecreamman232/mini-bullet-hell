using JustGame.Scripts.ScriptableEvent;
using TMPro;
using UnityEngine;

namespace JustGame.Scripts.UI
{
    public class DerbisCollectUI : MonoBehaviour
    {
        [SerializeField] private ResourceEvent m_resourceEvent;
        [SerializeField] private TextMeshProUGUI m_collectAmount;

        private void Start()
        {
            m_collectAmount.text = "0";
            m_resourceEvent.OnChangeDerbisAmount += UpdateAmount;
        }

        private void UpdateAmount(int amount)
        {
            m_collectAmount.text = amount.ToString();
        }
    }
}


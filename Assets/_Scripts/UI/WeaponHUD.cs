using JustGame.Scripts.ScriptableEvent;
using TMPro;
using UnityEngine;

namespace JustGame.Scripts.UI
{
    public class WeaponHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_magazineSizeTxt;
        [SerializeField] private IntEvent m_magazineSizeEvent;

        private void Start()
        {
            m_magazineSizeEvent.AddListener(UpdateMagazineHUD);
        }

        private void UpdateMagazineHUD(int amount)
        {
            m_magazineSizeTxt.text = $"x {amount.ToString()}";
        }
    } 
}


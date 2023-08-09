using JustGame.Scripts.Data;
using JustGame.Scripts.Managers;
using JustGame.Scripts.ScriptableEvent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class KillScoreUI : MonoBehaviour
    {
        [SerializeField] private KillScoreEvent m_killScoreEvent;
        [SerializeField] private LevelDataSO m_levelData;
        [SerializeField] private Image m_curBarImg;
        [SerializeField] private TextMeshProUGUI m_killNumberTxt;

        private void Start()
        {
            m_killScoreEvent.AddListener(UpdateBar);
            m_curBarImg.fillAmount = 0;
            Initialize();
        }

        private void Initialize()
        {
            m_killNumberTxt.text = $"X {m_levelData.CurrentLvlData.KillRequires.ToString()}";
        }
        
        private void UpdateBar(int score)
        {
            m_killNumberTxt.text =  $"X {(m_levelData.CurrentLvlData.KillRequires - score).ToString()}";
            m_curBarImg.fillAmount = MathHelpers.Remap(score, 0, m_levelData.CurrentLvlData.KillRequires, 0, 1);
        }

        private void OnDestroy()
        {
            m_killScoreEvent.RemoveListener(UpdateBar);
        }
    }
}

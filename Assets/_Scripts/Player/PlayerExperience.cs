using JustGame.Scripts.Data;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerExperience : MonoBehaviour
    {
        [SerializeField] private int m_curXP;
        [SerializeField] private int m_curLevel;
        [SerializeField] private LevelDataSO m_lvlData;
        [SerializeField] private IntEvent m_gainXPEvent;
        [SerializeField] private IntEvent m_levelUpEvent;

        private bool IsMaxLvl => m_curXP >= m_lvlData.Levels[m_curLevel].ExpRequire;
        
        private void Awake()
        {
            m_gainXPEvent.AddListener(OnGainXP);
        }

        private void OnGainXP(int xp)
        {
            if (IsMaxLvl) return;
            
            m_curXP += xp;
            
            //Level up
            if (m_curXP >= m_lvlData.Levels[m_curLevel].ExpRequire)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            m_curLevel++;
            m_levelUpEvent.Raise(m_curLevel);
        }

        private void OnDestroy()
        {
            m_gainXPEvent.RemoveListener(OnGainXP);
        }
    }
}

using System;
using UnityEngine;

namespace JustGame.Scripts.Data
{
    [Serializable]
    public class LevelData
    {
        public int Level;
        public int KillRequires;
    }
    
    [CreateAssetMenu(menuName = "JustGame/Data/Level data")]
    public class LevelDataSO : ScriptableObject
    {
        [SerializeField] private int m_curLevel;
        [SerializeField] private int m_maxLevel;
        [SerializeField] private LevelData[] m_levels;

        public int CurrentLvl => m_curLevel;
        public LevelData CurrentLvlData => m_levels[m_curLevel-1];

        
        private void OnEnable()
        {
            //TODO:Store level to save game. Here is just reset level every time we press Play Button
            m_curLevel = 1;
            if (m_maxLevel != m_levels.Length)
            {
                Debug.LogError("Level array not match MaxLevel");
            }
        }

        public void LevelUp()
        {
            m_curLevel++;
        }
        
    } 
}


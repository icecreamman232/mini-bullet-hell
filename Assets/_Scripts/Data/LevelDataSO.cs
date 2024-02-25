using System;
using UnityEngine;

namespace JustGame.Scripts.Data
{
    [Serializable]
    public class LevelData
    {
        public int Level;
        public int ExpRequire;
    }
    
    [CreateAssetMenu(menuName = "JustGame/Data/Level data")]
    public class LevelDataSO : ScriptableObject
    {
#if UNITY_EDITOR
        [Header("Auto Fill Value (Only for Editor)")]
        public int XPIncreaseRate;
        public int MaxLevel;
        public int StartXP;
        
        [ContextMenu("Fill XP")]
        private void FillXP()
        {
            Levels = new LevelData[MaxLevel];
            
            for (int i = 0; i < MaxLevel; i++)
            {
                Levels[i] = new LevelData();
                Levels[i].Level = i;
                if (i == 0)
                {
                    Levels[i].ExpRequire = StartXP;
                }
                else
                {
                    Levels[i].ExpRequire = Mathf.RoundToInt(Levels[i - 1].ExpRequire * (1 + XPIncreaseRate/100f));
                }
            }
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
        
        
        public LevelData[] Levels;
    } 
}


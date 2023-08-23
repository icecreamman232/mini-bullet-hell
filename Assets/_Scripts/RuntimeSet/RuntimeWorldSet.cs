using JustGame.Scripts.Common;
using JustGame.Scripts.Data;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.RuntimeSet
{
    public enum Grade
    {
        Common,
        Uncommon,
        Rare,
        Legend,
    }
    
    [CreateAssetMenu(menuName = "JustGame/Runtime Set/World Set")]
    public class RuntimeWorldSet : ScriptableObject
    {
        [SerializeField] private GameManager m_gameManager;
        [SerializeField] private PowerUpManager m_powerUpManager;
        [SerializeField] private LevelBounds m_levelBounds;
        
        
        [Header("Code Color")] 
        [SerializeField] private Color m_commonColor;
        [SerializeField] private Color m_uncommonColor;
        [SerializeField] private Color m_rareColor;
        [SerializeField] private Color m_legendColor;
        
        public GameManager GameManager => m_gameManager;
        public PowerUpManager PowerUpManager => m_powerUpManager;
        public LevelBounds LevelBounds => m_levelBounds;
       
        
        public Color CommonColor => m_commonColor;
        public Color UncommonColor => m_uncommonColor;
        public Color RareColor => m_rareColor;
        public Color LegendColor => m_legendColor;
        
        public void SetLevelBounds(LevelBounds bounds)
        {
            m_levelBounds = bounds;
        }

        public void SetPowerUpManager(PowerUpManager powerUpManager)
        {
            m_powerUpManager = powerUpManager;
        }
        public void SetGameManager(GameManager gm)
        {
            m_gameManager = gm;
        }
    }
}


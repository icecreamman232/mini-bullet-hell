using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Data/Ship profile")]
    public class ShipProfile : ScriptableObject
    {
        [Header("Attack")]
        [SerializeField] private int m_baseMinAtkDamage;
        [SerializeField] private int m_baseMaxAtkDamage;
        [SerializeField] private int m_baseAtkSpd; //100 pts = 1s (delay time between 2 shot)
        [SerializeField] private float m_baseCritChance;
        [SerializeField] private float m_baseCritDamageMultiplier;
        
        [SerializeField] private int m_baseKnockBack;
        
        [Header("Movement")]
        [SerializeField] private int m_baseMoveSpeed;

        [Header("Health")] 
        [SerializeField] private int m_baseArmor;
        [SerializeField] private int m_baseHealth;

        [Header("Extra")] 
        [SerializeField] private int m_baseCooldownReduce;
    }  
}


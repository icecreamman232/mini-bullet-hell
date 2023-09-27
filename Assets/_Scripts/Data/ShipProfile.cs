using UnityEngine;
using UnityEngine.Serialization;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Data/Ship profile")]
    public class ShipProfile : ScriptableObject
    {
        [Header("Identiciation")] 
        public string ShipName;
        [Header("Attack")]
        public int BaseMinAtkDamage;
        public int BaseMaxAtkDamage;
        public int BaseAtkSpd; //100 pts = 1s (delay time between 2 shot)
        public float BaseCritChance;
        public float BaseCritDamageMultiplier;
        
        public int BaseKnockBack;
        
        [Header("Movement")]
        public int BaseMoveSpeed;

        [Header("Health")] 
        public int BaseArmor;
        public int BaseHealth;

        [Header("Extra")] 
        public int BaseCooldownReduce;
    }  
}


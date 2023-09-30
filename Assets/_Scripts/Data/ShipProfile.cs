using UnityEngine;

namespace JustGame.Scripts.Data
{
    public enum SHIP_ID
    {
        BLUE_SHIP,
        GREEN_SHIP,
        RED_SHIP,
    }
    [CreateAssetMenu(menuName = "JustGame/Data/Ship profile")]
    public class ShipProfile : ScriptableObject
    {
        [Header("Identiciation")] 
        public SHIP_ID ShipID;
        public string ShipName;
        public GameObject ShipPrefab;
        [Header("Special Ability")] 
        public PowerUpData SpecialAbility;
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
        public int BaseHPRegeneration;
        public int BaseHealth;

        [Header("Extra")] 
        public int BaseCooldownReduce;
    }  
}


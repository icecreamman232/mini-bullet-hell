using System;
using JustGame.Scripts.Data;
using UnityEngine;

namespace JustGame.Scripts.RuntimeSet
{
    [CreateAssetMenu(menuName = "JustGame/Runtime Set/Ship Attribute")]
    public class ShipAttributeRuntime : ScriptableObject
    {
        [Header("Attack")]
        public int MinAtkDamage;
        public int MaxAtkDamage;
        public int AtkSpd; //100 pts = 1s (delay time between 2 shot)
        public float CritChance;
        public float CritDamageMultiplier;
        
        public int KnockBack;
        
        [Header("Movement")]
        public int MoveSpeed;

        [Header("Health")] 
        public int Armor;
        public int HPRegeneration;
        public int Health;

        [Header("Extra")] 
        public int CooldownReduce;

        public void CopyData(ShipAttribute attribute)
        {
            MinAtkDamage = attribute.BaseMinAtkDamage;
            MaxAtkDamage = attribute.BaseMaxAtkDamage;
            AtkSpd = attribute.BaseAtkSpd;
            CritChance = attribute.BaseCritChance;
            CritDamageMultiplier = attribute.BaseCritDamageMultiplier;
            KnockBack = attribute.BaseKnockBack;
            MoveSpeed = attribute.BaseMoveSpeed;
            Armor = attribute.BaseArmor;
            HPRegeneration = attribute.BaseHPRegeneration;
            Health = attribute.BaseHealth;
            CooldownReduce = attribute.BaseCooldownReduce;
        }
        
        public void ResetData()
        {
            MinAtkDamage = 0;
            MaxAtkDamage = 0;
            AtkSpd = 0;
            CritChance = 0;
            CritDamageMultiplier = 0;
            KnockBack = 0;
            MoveSpeed = 0;
            Armor = 0;
            HPRegeneration = 0;
            Health = 0;
            CooldownReduce = 0;
        }
    }
}


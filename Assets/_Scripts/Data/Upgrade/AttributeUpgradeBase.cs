using UnityEngine;

namespace JustGame.Scripts.Data
{
    public enum UpgradeRank
    {
        BRONZE,
        SILVER,
        GOLD,
    }
    public class AttributeUpgradeBase : ScriptableObject
    {
        public string UpgradeName;
        public UpgradeRank Rank;

        public virtual void ApplyUpgrade()
        {
            
        }
    }
}

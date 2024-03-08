using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Data/Upgrade Float Data",order = 1)]
    public class AttributeUpgradeFloatData : AttributeUpgradeBase
    {
        public float UpgradeValue;
        public FloatEvent UpgradeEvent;

        public override void ApplyUpgrade()
        {
            UpgradeEvent.Raise(UpgradeValue);
            base.ApplyUpgrade();
        }
    } 
}


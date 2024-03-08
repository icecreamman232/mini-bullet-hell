using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Data/Upgrade Int Data",order = 0)]
    public class AttributeUpgradeIntData : AttributeUpgradeBase
    {
        public int UpgradeValue;
        public IntEvent UpgradeEvent;

        public override void ApplyUpgrade()
        {
            UpgradeEvent.Raise(UpgradeValue);
            base.ApplyUpgrade();
        }
    } 
}


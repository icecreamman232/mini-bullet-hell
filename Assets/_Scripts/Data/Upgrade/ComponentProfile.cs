using JustGame.Scripts.RuntimeSet;
using UnityEngine;

namespace JustGame.Scripts.Data
{
    public enum ComponentType
    {
        REACTOR,
        ENGINE,
        HULL,
    }
    
    [CreateAssetMenu(menuName = "JustGame/Data/Component profile")]
    public class ComponentProfile : ScriptableObject
    {
        public ComponentType ComponentType;
        public Grade Grade;
        public Sprite Icon;
        public string Title;
        public string Description;
        public int BonusValue;
    }
    
}

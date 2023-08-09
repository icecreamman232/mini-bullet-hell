using JustGame.Scripts.Common;
using UnityEngine;

namespace JustGame.Scripts.RuntimeSet
{
    [CreateAssetMenu(menuName = "JustGame/Runtime Set/World Set")]
    public class RuntimeWorldSet : ScriptableObject
    {
        [SerializeField] private LevelBounds m_levelBounds;

        public LevelBounds LevelBounds => m_levelBounds;
        
        public void SetLevelBounds(LevelBounds bounds)
        {
            m_levelBounds = bounds;
        }
    }
}


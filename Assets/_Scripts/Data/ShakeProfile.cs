using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Data/Shake Profile")]
    public class ShakeProfile : ScriptableObject
    {
        public float ShakeDuration;
        public float ShakePower;
        public float Frequency;
    }
}

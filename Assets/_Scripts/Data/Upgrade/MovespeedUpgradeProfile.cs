using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Upgrade/Movespeed")]
    public class MovespeedUpgradeProfile : UpgradeProfile
    {
        [Header("Parameters")] 
        [SerializeField] private float m_moveSpeedIncrease;

        public float MoveSpeedIncrease => m_moveSpeedIncrease;
    }
}


using System.Collections;
using System.Collections.Generic;
using JustGame.Scripts.Data;
using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/PowerUp/Triple Shot")]
    public class TripleShotPowerUp : PowerUpData
    {
        [ContextMenu("Trigger")]
        private void Test()
        {
            ApplyPowerUp();
        }
    }
}

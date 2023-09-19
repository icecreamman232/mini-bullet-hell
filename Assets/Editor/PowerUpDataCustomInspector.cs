using System;
using JustGame.Scripts.Data;
using UnityEditor;
using UnityEngine;

namespace JustGame.Scripts.EditorExtension
{
    [CustomEditor(typeof(PowerUpData))]
    public class PowerUpDataCustomInspector : Editor
    {
        private string m_buttonTxt = "Trigger";

        private PowerUpData m_powerUp;

        private void OnEnable()
        {
            m_powerUp = (PowerUpData)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button(m_buttonTxt))
            {
                m_powerUp.ApplyPowerUp();
            }
        }
    }

    [CustomEditor(typeof(PiercingShotPowerUp))]
    public class PiercingPowerUpInspector : PowerUpDataCustomInspector
    {
        
    }
    
    [CustomEditor(typeof(DoubleShotPowerUp))]
    public class DoubleShotInspector : PowerUpDataCustomInspector
    {
        
    }
    
    [CustomEditor(typeof(TripleShotPowerUp))]
    public class TripleShotInspector : PowerUpDataCustomInspector
    {
        
    }
    
    [CustomEditor(typeof(AutoCollectPowerUp))]
    public class AutoCollectInspector : PowerUpDataCustomInspector
    {
        
    }
    
    [CustomEditor(typeof(EnableCriticalDamagePowerUp))]
    public class EnableCriticalDamageInspector : PowerUpDataCustomInspector
    {
        
    }
    
    [CustomEditor(typeof(FlashProtectorPowerUp))]
    public class FlashProtectorInspector : PowerUpDataCustomInspector
    {
        
    }
    
    [CustomEditor(typeof(HealingPowerUp))]
    public class HealingInspector : PowerUpDataCustomInspector
    {
        
    }
    
    [CustomEditor(typeof(IncreaseAttackSpeedPowerUp))]
    public class IncreaseAttackSpeedInspector : PowerUpDataCustomInspector
    {
        
    }
    
    [CustomEditor(typeof(IncreaseBulletSizePowerUp))]
    public class IncreaseBulletSizeInspector : PowerUpDataCustomInspector
    {
        
    }
    
    [CustomEditor(typeof(IncreaseRangePowerUp))]
    public class IncreaseRangeInspector : PowerUpDataCustomInspector
    {
        
    }
    
    [CustomEditor(typeof(ParalyzeCoatingPowerUp))]
    public class ParalyzeCoatingInspector : PowerUpDataCustomInspector
    {
        
    }
    
    [CustomEditor(typeof(RecycleJunkPowerUp))]
    public class RecycleJunkInspector : PowerUpDataCustomInspector
    {
        
    }
    
    [CustomEditor(typeof(BulletBlockingDronePowerUp))]
    public class BulletBlockingDroneInspector : PowerUpDataCustomInspector
    {
        
    }
    
    [CustomEditor(typeof(SacrificeHPPowerUp))]
    public class SacrificeHPInspector : PowerUpDataCustomInspector
    {
        
    }
    
    [CustomEditor(typeof(MadnessPowerUp))]
    public class MadnessInspector : PowerUpDataCustomInspector
    {
        
    }
    
    [CustomEditor(typeof(DashPowerUp))]
    public class DashInspector : PowerUpDataCustomInspector
    {
        
    }
    
    [CustomEditor(typeof(ShieldPowerUp))]
    public class ShieldInspector : PowerUpDataCustomInspector
    {
        
    }
    
    [CustomEditor(typeof(BurningCoatingPowerUp))]
    public class BurningCoatingInspector : PowerUpDataCustomInspector
    {
        
    }
}


using System.Collections.Generic;
using System.Linq;
using JustGame.Scripts.Data;
using JustGame.Scripts.RuntimeSet;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public enum FacingDirection
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
    }
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        [SerializeField] private PlayerComponentSet m_componentSet;
        [SerializeField] private ShipAttributeRuntime m_attributeRuntime;
        [SerializeField] private ShipAttribute m_attributeData;

        [Header("Upgrade events")]
        [SerializeField] private IntEvent m_upgradeMinDamageEvent;
        [SerializeField] private IntEvent m_upgradeMaxDamageEvent;
        [SerializeField] private IntEvent m_upgradeAtkSpdEvent;
        [SerializeField] private FloatEvent m_upgradeCritChanceEvent;
        [SerializeField] private FloatEvent m_upgradeCritMultipleEvent;
        [SerializeField] private FloatEvent m_upgradeKnockBack;
        [SerializeField] private FloatEvent m_upgradeMoveSpeed;
        [SerializeField] private IntEvent m_upgradeArmor;
        [SerializeField] private FloatEvent m_upgradeHPRegeneration;
        [SerializeField] private FloatEvent m_upgradeHealth;
        [SerializeField] private IntEvent m_upgradeCooldownReduce;
        
        [Header("Abilities")]
        [SerializeField] private GameObject[] m_abilityNode;
        public FacingDirection FacingDirection;
        private List<PlayerAbility> m_cachedAbilities;

        public ShipAttributeRuntime AttributeRuntime => m_attributeRuntime;
        
        private void Awake()
        {
            m_componentSet.SetPlayer(this);
            m_gameCoreEvent.OnChangeStateCallback += OnChangeGameState;
            Initialize();
        }

        protected virtual void Initialize()
        {
            m_attributeRuntime.CopyData(m_attributeData);
            
            m_cachedAbilities ??= new List<PlayerAbility>();
            m_cachedAbilities = GetComponents<PlayerAbility>().ToList();

            for (int i = 0; i < m_abilityNode.Length; i++)
            {
                m_cachedAbilities.AddRange(m_abilityNode[i].GetComponents<PlayerAbility>());
            }
            
            foreach (var ability in m_cachedAbilities)
            {
                ability.Initialize();
            }
            
            m_upgradeMinDamageEvent.AddListener(UpgradeMinDamage);
            m_upgradeMaxDamageEvent.AddListener(UpgradeMaxDamage);
            m_upgradeAtkSpdEvent.AddListener(UpgradeAtkSpd);
            m_upgradeCritChanceEvent.AddListener(UpgradeCritChance);
            m_upgradeCritMultipleEvent.AddListener(UpgradeCritMultipleChance);
            m_upgradeKnockBack.AddListener(UpgradeKnockBack);
            m_upgradeMoveSpeed.AddListener(UpgradeMoveSpeed);
            m_upgradeArmor.AddListener(UpgradeArmor);
            m_upgradeHPRegeneration.AddListener(UpgradeHPRegeneration);
            m_upgradeHealth.AddListener(UpgradeHealth);
            m_upgradeCooldownReduce.AddListener(UpgradeCoolDownReduce);
        }

        private void OnChangeGameState(GameState prev, GameState current)
        {
            if (current == GameState.FIGHTING)
            {
                for (int i = 0; i < m_cachedAbilities.Count; i++)
                {
                    m_cachedAbilities[i].IsPermit = true;
                }
            }
        }
        
        public T FindAbility<T>() where T : PlayerAbility
        {
            foreach (var ability in m_cachedAbilities)
            {
                if (ability is T playerAbility)
                {
                    return playerAbility;
                }
            }

            return null;
        }
        
        #region Upgrades Event

        private void UpgradeMinDamage(int addValue)
        {
            m_attributeRuntime.MinAtkDamage += addValue;
            if (m_attributeRuntime.MinAtkDamage >= m_attributeRuntime.MaxAtkDamage)
            {
                m_attributeRuntime.MinAtkDamage = m_attributeRuntime.MaxAtkDamage;
            }
        }

        private void UpgradeMaxDamage(int addValue)
        {
            m_attributeRuntime.MaxAtkDamage += addValue;
        }

        private void UpgradeAtkSpd(int addValue)
        {
            m_attributeRuntime.AtkSpd += addValue;
        }

        private void UpgradeCritChance(float addValue)
        {
            m_attributeRuntime.CritChance += addValue;
        }
        
        private void UpgradeCritMultipleChance(float addValue)
        {
            m_attributeRuntime.CritDamageMultiplier += addValue;
        }

        private void UpgradeKnockBack(float addValue)
        {
            m_attributeRuntime.KnockBack += addValue;
        }

        private void UpgradeMoveSpeed(float addValue)
        {
            m_attributeRuntime.MoveSpeed += addValue;
        }

        private void UpgradeArmor(int addValue)
        {
            m_attributeRuntime.Armor += addValue;
        }

        private void UpgradeHPRegeneration(float addValue)
        {
            m_attributeRuntime.HPRegeneration += addValue;
        }

        private void UpgradeHealth(float addValue)
        {
            m_attributeRuntime.Health += addValue;
        }

        private void UpgradeCoolDownReduce(int addValue)
        {
            m_attributeRuntime.CooldownReduce += addValue;
        }
        
        #endregion
        
        
        private void OnDestroy()
        {
            m_gameCoreEvent.OnChangeStateCallback -= OnChangeGameState;
            m_upgradeMinDamageEvent.RemoveListener(UpgradeMinDamage);
            m_upgradeMaxDamageEvent.RemoveListener(UpgradeMaxDamage);
            m_upgradeAtkSpdEvent.RemoveListener(UpgradeAtkSpd);
            m_upgradeCritChanceEvent.RemoveListener(UpgradeCritChance);
            m_upgradeCritMultipleEvent.RemoveListener(UpgradeCritMultipleChance);
            m_upgradeKnockBack.RemoveListener(UpgradeKnockBack);
            m_upgradeMoveSpeed.RemoveListener(UpgradeMoveSpeed);
            m_upgradeArmor.RemoveListener(UpgradeArmor);
            m_upgradeHPRegeneration.RemoveListener(UpgradeHPRegeneration);
            m_upgradeHealth.RemoveListener(UpgradeHealth);
            m_upgradeCooldownReduce.RemoveListener(UpgradeCoolDownReduce);
        }
        
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private RuntimeWorldSet m_runtimeWorldSet;
        [SerializeField] private PlayerComponentSet m_componentSet;
        
        public FacingDirection FacingDirection;
        private List<PlayerAbility> m_cachedAbilities;

        private void Awake()
        {
            m_componentSet.SetPlayer(this);
            m_gameCoreEvent.OnChangeStateCallback += OnChangeGameState;
            Initialize();
        }

        protected virtual void Initialize()
        {
            m_cachedAbilities ??= new List<PlayerAbility>();
            m_cachedAbilities = GetComponents<PlayerAbility>().ToList();
            foreach (var ability in m_cachedAbilities)
            {
                ability.Initialize();
            }
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
    }
}


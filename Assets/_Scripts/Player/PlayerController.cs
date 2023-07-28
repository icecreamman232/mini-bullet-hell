using System;
using System.Collections.Generic;
using System.Linq;
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
        public FacingDirection FacingDirection;

        private List<PlayerAbility> m_cachedAbilities;

        protected virtual void Start()
        {
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


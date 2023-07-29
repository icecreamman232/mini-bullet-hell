using JustGame.Scripts.Player;
using JustGame.Scripts.Weapons;
using UnityEngine;

namespace JustGame.Scripts.RuntimeSet
{
    /// <summary>
    /// Store runtime reference to common components
    /// for quick access without using GetComponent calls
    /// </summary>
    [CreateAssetMenu(menuName = "JustGame/Runtime Set/Player Component Set")]
    public class PlayerComponentSet : ScriptableObject
    {
        [SerializeField] private PlayerController m_playerController;
        [SerializeField] private PlayerHealth m_health;
        
        public PlayerController Player => m_playerController;
        public PlayerHealth Health => m_health;
        
        
        public void SetPlayer(PlayerController controller)
        {
            m_playerController = controller;
        }

        public void SetHealth(PlayerHealth health)
        {
            m_health = health;
        }
    }
}


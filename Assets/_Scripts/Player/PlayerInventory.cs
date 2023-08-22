using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private IntEvent m_derbisCollectEvent;
        [SerializeField] private int m_derbisAmount;
        
        private void Start()
        {
            m_derbisCollectEvent.AddListener(AddDerbis);
        }

        private void AddDerbis(int amount)
        {
            m_derbisAmount += amount;
        }
    }
}


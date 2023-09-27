using UnityEngine;

namespace JustGame.Scripts.Data
{
    public enum SHIP_ID
    {
        BLUE_SHIP,
        RED_SHIP,
        GREEN_SHIP,
    }
    
    [CreateAssetMenu(menuName = "JustGame/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField] private SHIP_ID m_shipID;

        public SHIP_ID CurrentShipID => m_shipID;

        public void SaveShipID(SHIP_ID id)
        {
            m_shipID = id;
        }
    }
}


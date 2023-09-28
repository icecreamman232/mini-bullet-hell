using System;
using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        public ShipProfile ShipProfile;
    }
}


using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [FormerlySerializedAs("ShipProfile")] public ShipAttribute shipAttribute;
    }
}


using System;
using System.Collections.Generic;
using UnityEngine;

namespace JustGame.Scripts.RuntimeSet
{
    public enum SoundEffectType
    {
        PLAYER_GUN = 0,
        
        
        //======ENEMY BEGIN WITH 100===//
        ENEMY_EXPLOSION = 100,
        ENEMY_GET_HIT = 101,
    }
    
    [Serializable]
    public class SoundFX
    {
        public SoundEffectType Type;
        public AudioClip Audio;
    }
    
    [CreateAssetMenu(menuName = "JustGame/Runtime Set/Sound Set")]
    public class SoundGlobalSet : ScriptableObject
    {
        [SerializeField] private SoundFX[] m_soundFxList;

        private Dictionary<SoundEffectType, AudioClip> m_soundDictionary;

        private void OnEnable()
        {
            m_soundDictionary = new Dictionary<SoundEffectType, AudioClip>();
            for (int i = 0; i < m_soundFxList.Length; i++)
            {
                m_soundDictionary.Add(m_soundFxList[i].Type, m_soundFxList[i].Audio);
            }
        }
        
        public AudioClip GetSoundFX(SoundEffectType type)
        {
            m_soundDictionary.TryGetValue(type, out var audio);
            return audio;
        }
    }
}


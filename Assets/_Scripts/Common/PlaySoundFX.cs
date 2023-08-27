using JustGame.Scripts.RuntimeSet;
using UnityEngine;

namespace JustGame.Scripts.Common
{
    public class PlaySoundFX : MonoBehaviour
    {
        [SerializeField] private SoundEffectType Type;
        [SerializeField] private SoundGlobalSet m_soundGlobalSet;
        [SerializeField] private AudioSource m_audioSource;
        
        public void PlaySFX()
        {
            if (m_audioSource == null) return;
            m_audioSource.PlayOneShot(m_soundGlobalSet.GetSoundFX(Type));
        }
    }
}


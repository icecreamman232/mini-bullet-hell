using DG.Tweening;
using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public class SoundManager : MonoBehaviour
    {
        [Header("Music")] 
        [SerializeField] private float m_fadeInDuration;
        [SerializeField] private float m_fadeOutDuration;
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_fightBGM;

        private void Start()
        {
            m_audioSource.clip = m_fightBGM;
            m_audioSource.volume = 0;
            m_audioSource.DOFade(1, m_fadeInDuration);
            m_audioSource.Play();
        }
    }
}

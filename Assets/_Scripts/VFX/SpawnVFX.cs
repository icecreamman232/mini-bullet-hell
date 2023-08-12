using System;
using System.Collections;
using JustGame.Scripts.Common;
using UnityEngine;

namespace JustGame.Scripts.VFX
{
    public class SpawnVFX : MonoBehaviour
    {
        [SerializeField] private float m_delayTime;
        [SerializeField] private AnimationParameter m_disappearAnim;

        public Action OnFinishVFX;
        
        public void PlayVFX()
        {
            StartCoroutine(SpawnVFXRoutine());
        }

        private IEnumerator SpawnVFXRoutine()
        {
            yield return new WaitForSeconds(m_delayTime);
            m_disappearAnim.SetBool(true);
            yield return new WaitForSeconds(m_disappearAnim.Duration);
            OnFinishVFX?.Invoke();
        }
    }
}


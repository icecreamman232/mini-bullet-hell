using JustGame.Scripts.Data;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.VFX
{
    public class AnkhVFXController : MonoBehaviour
    {
        [SerializeField] private RevivePowerUp m_revivePowerUp;
        [SerializeField] private ShakeProfile m_shakeProfile;
        [SerializeField] private ScreenShakeEvent m_screenShakeEvent;

        public void PlayScreenShake()
        {
            m_screenShakeEvent.DoShake(m_shakeProfile);
        }
        public void OnFinishVFX()
        {
            m_revivePowerUp.IsVFXDone = true;
            Invoke(nameof(SelfDestroy),0.5f);
        }

        private void SelfDestroy()
        {
            Destroy(this.gameObject);
        }
    }
}


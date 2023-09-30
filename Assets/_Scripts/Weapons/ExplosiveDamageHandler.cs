using System;
using System.Collections;
using JustGame.Scripts.Common;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public class ExplosiveDamageHandler : DamageHandler
    {
        [SerializeField] private float m_lifeTime;
        [SerializeField] private PlayerExplosiveProjectile m_projectile;
        [SerializeField] private GameObject m_explodeDamageArea;
        [SerializeField] private float m_explodeDuration;
        [SerializeField] private AnimationParameter m_explodeAnim;
        private float m_lifeTimeTimer;

        public Action<GameObject> OnSelfExplode;
        private bool m_isExploding;
        private bool m_canCountDown;
        private void OnEnable()
        {
            m_lifeTimeTimer = m_lifeTime;
            m_explodeDamageArea.SetActive(false);
            m_isExploding = false;
            m_canCountDown = false;
        }

        protected override void Start()
        {
            SetDamageMultiplier(1);
        }

        public void StartTimer()
        {
            m_canCountDown = true;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (!LayerManager.IsInLayerMask(other.gameObject.layer, m_targetMask)) return;
            if (m_isExploding) return;
            StartCoroutine(OnExplode());
        }

        private IEnumerator OnExplode()
        {
            m_isExploding = true;
            m_projectile.StopBullet();
            m_lifeTimeTimer = 0;
            m_explodeDamageArea.SetActive(true);
            if (m_explodeAnim != null)
            {
                m_explodeAnim.SetTrigger();
            }
            yield return new WaitForSeconds(m_explodeDuration);
            m_explodeDamageArea.SetActive(false);
            OnSelfExplode?.Invoke(null);
        }
        
        private void Update()
        {
            if (!m_canCountDown) return;
            if (m_isExploding) return;
            m_lifeTimeTimer -= Time.deltaTime;
            if (m_lifeTimeTimer <= 0 && !m_isExploding)
            {
                StartCoroutine(OnExplode());
            }
        }
    }
}

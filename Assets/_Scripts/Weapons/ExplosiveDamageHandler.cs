using System;
using System.Collections;
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
        private float m_lifeTimeTimer;

        public Action<GameObject> OnSelfExplode;
        private bool m_isExploded;
        private bool m_hasProcess;
        
        private void OnEnable()
        {
            m_lifeTimeTimer = m_lifeTime;
            m_explodeDamageArea.SetActive(false);
            m_isExploded = false;
            m_hasProcess = false;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (!LayerManager.IsInLayerMask(other.gameObject.layer, m_targetMask)) return;
            StartCoroutine(OnExplode());
        }

        private IEnumerator OnExplode()
        {
            if (m_hasProcess)
            {
                yield break;
            }

            m_projectile.StopBullet();
            m_hasProcess = true;
            m_isExploded = true;
            m_lifeTimeTimer = 0;
            m_explodeDamageArea.SetActive(true);
            yield return new WaitForSeconds(m_explodeDuration);
            m_explodeDamageArea.SetActive(false);
            OnSelfExplode?.Invoke(null);
            m_hasProcess = false;

        }
        
        private void Update()
        {
            if (!this.gameObject.activeSelf) return;
            if (m_isExploded) return;
            m_lifeTimeTimer -= Time.deltaTime;
            if (m_lifeTimeTimer <= 0)
            {
                StartCoroutine(OnExplode());
            }
        }
    }
}

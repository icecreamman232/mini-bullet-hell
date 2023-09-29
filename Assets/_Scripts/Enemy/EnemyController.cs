using System;
using JustGame.Scripts.ScriptableEvent;
using JustGame.Scripts.VFX;
using JustGame.Scripts.Weapons;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyBrain m_brain;
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        [SerializeField] private EnemyHealth m_health;
        [SerializeField] private SpawnVFX m_spawnVFX;
        [SerializeField] private SpriteRenderer m_sprite;
        [SerializeField] private Collider2D m_collider2D;
        
        
        protected virtual void Start()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            if (m_brain == null)
            {
                m_brain = GetComponentInChildren<EnemyBrain>();
            }
            m_brain.Owner = this;
            m_gameCoreEvent.OnChangeStateCallback += OnGameStateChange;
            m_sprite.enabled = false;
            m_collider2D.enabled = false;
            m_spawnVFX.OnFinishVFX += Spawn;
            m_spawnVFX.PlayVFX();
        }
        
        public void SetActiveBrain(EnemyBrain activeBrain)
        {
            m_brain = activeBrain;
        }
        
        private void OnGameStateChange(GameState prevState, GameState newState)
        {
            if (newState == GameState.END_WAVE)
            {
                m_brain.BrainActive = false;
                m_brain.gameObject.SetActive(false);
                m_health.InstantDead();
                m_gameCoreEvent.OnChangeStateCallback -= OnGameStateChange;
            }
        }
        
        private void Spawn()
        {
            m_sprite.enabled = true;
            m_collider2D.enabled = true;
            
            if (m_brain != null)
            {
                m_brain.BrainActive = true;
            }
        }

        private void OnDestroy()
        {
            m_gameCoreEvent.OnChangeStateCallback -= OnGameStateChange;
        }
    }
}

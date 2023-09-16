using System;
using System.Collections;
using System.Collections.Generic;
using JustGame.Scripts.Data;
using JustGame.Scripts.RuntimeSet;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        [SerializeField] private RuntimeWorldSet m_worldSet;
        [SerializeField] private SpawnProfile m_spawnProfile;
        [SerializeField] private bool m_canSpawn;
        [SerializeField] private bool m_noEnemy;
        private float m_timer;

        private bool m_isSpawning;
        private void Awake()
        {
            m_gameCoreEvent.OnChangeStateCallback += OnChangeGameState;
        }
        
        private void Update()
        {
            if (m_noEnemy)
            {
                return;
            }
            if (!m_canSpawn) return;

            if (m_isSpawning) return;
            
            m_timer += Time.deltaTime;
            if (m_timer > m_spawnProfile.DelayTimeBetweenTwoSpawn)
            {
                StartCoroutine(SpawnRoutine());
            }
        }

        private IEnumerator SpawnRoutine()
        {
            if (m_isSpawning)
            {
                yield break;
            }

            m_isSpawning = true;
            
            var listToSpawn = m_spawnProfile.GetListSpawn();
            var centerToSpawn = m_worldSet.LevelBounds.GetRandomPoint();
                
            for (int i = 0; i < listToSpawn.Count; i++)
            {
                Instantiate(listToSpawn[i], GetRandomPointAroundCenter(centerToSpawn, 1.5f),
                    Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
            }
            
            m_timer = 0;
            m_isSpawning = false;
        }
        
        public Vector2 GetRandomPointAroundCenter(Vector2 centerPoint, float radius)
        {
            var random = Random.insideUnitCircle * radius;
            return centerPoint + random;
        }

        
        private void OnChangeGameState(GameState prevState, GameState curState)
        {
            switch (curState)
            {
                case GameState.FIGHTING:
                    m_canSpawn = true;
                    break;
                case GameState.PICK_UPGRADE:
                    break;
                case GameState.GAME_OVER:
                    m_canSpawn = false;
                    break;
            }
        }

        private void OnDestroy()
        {
            m_gameCoreEvent.OnChangeStateCallback -= OnChangeGameState;
        }
    }
}


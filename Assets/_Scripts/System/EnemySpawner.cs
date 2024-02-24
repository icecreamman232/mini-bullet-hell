using System.Collections;
using JustGame.Scripts.Attribute;
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

        [Header("Spawn Settings")] 
        [SerializeField] [ReadOnly] private int m_currentWave;
        [SerializeField] private SpawnProfile m_spawnProfile;
        [SerializeField] private bool m_canSpawn;
        [SerializeField] private bool m_noEnemy;
        private float m_timer;
        private bool m_isSpawning;

        public int CurrentWave => m_currentWave;
        
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
            if (m_timer > m_spawnProfile.GetDelayTime(m_currentWave))
            {
                StartCoroutine(SpawnRoutine(m_currentWave));
            }
        }
        
        private IEnumerator SpawnRoutine(int waveIndex)
        {
            if (m_isSpawning)
            {
                yield break;
            }

            m_isSpawning = true;

            var quantity = m_spawnProfile.GetQuantity(waveIndex);
            var prefab = m_spawnProfile.GetNextEnemyPrefab(waveIndex);
            var centerToSpawn = m_worldSet.LevelBounds.GetRandomPoint();
                
            for (int i = 0; i < quantity; i++)
            {
                Instantiate(prefab, GetRandomPointAroundCenter(centerToSpawn, 1.5f),
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
                case GameState.END_WAVE:
                    m_canSpawn = false;
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


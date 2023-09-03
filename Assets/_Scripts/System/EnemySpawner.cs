using JustGame.Scripts.Data;
using JustGame.Scripts.RuntimeSet;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

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
            m_timer += Time.deltaTime;
            if (m_timer > m_spawnProfile.DelayTimeBetweenTwoSpawn)
            {
                Instantiate(m_spawnProfile.GetNextSpawn(), m_worldSet.LevelBounds.GetRandomPoint(),
                    Quaternion.identity);
                m_timer = 0;
            }
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
    }
}


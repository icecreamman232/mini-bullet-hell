using System.Collections.Generic;
using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public class ObjectPooler : MonoBehaviour
    {
        public Transform Parent;
        public Vector2 Offset;
        public bool IsSharePool;
        public GameObject ObjectToPool;
        public int PoolSize;

        private List<GameObject> m_pool;

        public List<GameObject> CurrentPool => m_pool;
        
        private void Awake()
        {
            CreatePool();
        }

        private void CreatePool()
        {
            if (ObjectToPool == null) return;
            
            if (m_pool == null)
            {
                m_pool = new List<GameObject>();
            }

            //Find existing pool with same name
            if (IsSharePool)
            {
                var pools = FindObjectsOfType<ObjectPooler>();
                for (int i = 0; i < pools.Length; i++)
                {
                    if(pools[i] == this) continue;
                    if (pools[i].ObjectToPool.name == ObjectToPool.name && pools[i].ObjectToPool!=null)
                    {
                        m_pool = pools[i].CurrentPool;
                        return;
                    }
                }
            }
            
            for (int i = 0; i < PoolSize; i++)
            {
                var pooledObject = Instantiate(ObjectToPool, Parent);
                pooledObject.SetActive(false);
                m_pool.Add(pooledObject);
            }
        }

        public GameObject GetPooledGameObject()
        {
            for (int i = 0; i < PoolSize; i++)
            {
                if (!m_pool[i].activeInHierarchy)
                {
                    m_pool[i].SetActive(true);
                    return m_pool[i];
                }
            }
            
            return null;
        }
        
        
    }
}


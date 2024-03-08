using System;
using System.Collections.Generic;
using JustGame.Scripts.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.Managers
{
    public class UpgradeAttributeManager : Singleton<UpgradeAttributeManager>
    {
        [SerializeField] private AttributeUpgradeBase[] m_bronzeList;
        [SerializeField] private AttributeUpgradeBase[] m_silverList;
        [SerializeField] private AttributeUpgradeBase[] m_goldList;
        
        
        public void ApplyAttribute()
        {
            
        }
        
        public List<AttributeUpgradeBase> GetUpgradeList(int numberUpgrade)
        {
            var resultList = new List<AttributeUpgradeBase>();

            for (int i = 0; i < numberUpgrade; i++)
            {
                var randRank = Random.Range(1, 4);
                if (randRank == 1)
                {
                    resultList.Add(m_bronzeList[Random.Range(0,m_bronzeList.Length)]);
                }
                else if (randRank == 2)
                {
                    resultList.Add(m_silverList[Random.Range(0,m_silverList.Length)]);
                }
                else if (randRank == 3)
                {
                    resultList.Add(m_goldList[Random.Range(0,m_goldList.Length)]);
                }
            }

            return resultList;
        }
    }
}



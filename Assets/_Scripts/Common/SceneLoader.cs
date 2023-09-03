using System.Collections;
using System.Collections.Generic;
using JustGame.Scripts.Managers;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JustGame.Scripts.Common
{
    public class SceneLoader : PersistentSingleton<SceneLoader>
    {
        [SerializeField] private string m_loadingScene;
        [SerializeField] private bool m_isProcessing;

        public bool IsProcessing => m_isProcessing;
        
        public void LoadToScene(string fromScene, string toScene)
        {
            StartCoroutine(OnLoadToScene(fromScene, toScene));
        }

        private IEnumerator OnLoadToScene(string fromScene, string toScene)
        {
            if (m_isProcessing)
            {
                yield break;
            }
            m_isProcessing = true;
            
            var asyncLoading = SceneManager.LoadSceneAsync(m_loadingScene, LoadSceneMode.Additive);
            yield return new WaitUntil(() =>asyncLoading.isDone);

            var isUnloaded = SceneManager.UnloadSceneAsync(fromScene);
            yield return new WaitUntil(()=>isUnloaded.isDone);
            
            var asyncToScene = SceneManager.LoadSceneAsync(toScene, LoadSceneMode.Additive);
            yield return new WaitUntil(() => asyncToScene.isDone);

            var asyncUnloadLoading = SceneManager.UnloadSceneAsync(m_loadingScene);
            yield return new WaitUntil(() => asyncUnloadLoading.isDone);
            
            m_isProcessing = false;
        }
    }
}


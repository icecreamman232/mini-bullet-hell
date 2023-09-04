using System.Collections;
using JustGame.Scripts.Managers;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JustGame.Scripts.Common
{
    public class SceneLoader : PersistentSingleton<SceneLoader>
    {
        [SerializeField] private string m_loadingScene;
        [SerializeField] private float m_loadingTime;
        [SerializeField] private FloatEvent m_loadingBarEvent;
        [SerializeField] private bool m_isProcessing;

        private float m_progress;
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
            m_progress = 0;
            var asyncLoading = SceneManager.LoadSceneAsync(m_loadingScene, LoadSceneMode.Additive);
            yield return new WaitUntil(() =>asyncLoading.isDone);

            var isUnloaded = SceneManager.UnloadSceneAsync(fromScene);
            yield return new WaitUntil(()=>isUnloaded.isDone);
            
            var asyncToScene = SceneManager.LoadSceneAsync(toScene, LoadSceneMode.Additive);
            yield return new WaitUntil(() => asyncToScene.isDone);
            
            float delayTime = 0;
            while (delayTime < m_loadingTime)
            {
                delayTime += Time.deltaTime;
                m_progress = delayTime / m_loadingTime;
                m_loadingBarEvent.Raise(m_progress);
                yield return null;
            }

            var asyncUnloadLoading = SceneManager.UnloadSceneAsync(m_loadingScene);
            yield return new WaitUntil(() => asyncUnloadLoading.isDone);
            
            m_isProcessing = false;
        }
    }
}


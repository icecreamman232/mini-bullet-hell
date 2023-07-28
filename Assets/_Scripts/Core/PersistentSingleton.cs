using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public class PersistentSingleton<T> : MonoBehaviour where T: Component
    {
        private static T instance = null;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject newObj = new GameObject();
                        instance = newObj.AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            if (instance == null)
            {
                //if this is first instance, make it Singleton
                instance = this as T;
                instance.transform.parent = null;
                DontDestroyOnLoad(transform.gameObject);
            }
            else
            {
                //if an instance exists and we find another in scene, destroy it
                if (this != instance)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

}
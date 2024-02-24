using UnityEngine;

namespace JustGame.Scripts.Common
{
    public class SelfDestroy : MonoBehaviour
    {
        public void DestroySelf()
        {
            Destroy(this.gameObject);
        }
    }
}


using JustGame.Scripts.Items;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerLootCollect : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != LayerManager.ItemLayer) return;

            var item = other.gameObject.GetComponent<Item>();
            if (item == null) return;
            item.SetCollectByPlayer();
        }
    }
}

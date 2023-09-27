using JustGame.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class ShipSelectInfoPanel : MonoBehaviour
{
    [SerializeField] private Image m_shipPreviewImage;
    
    public void OnSelectShipAvatar(ShipAvatarButton avatar)
    {
        m_shipPreviewImage.sprite = avatar.ShipAvatar.sprite;
    }

    public void OnDeselectShipAvatar(ShipAvatarButton avatar)
    {
        m_shipPreviewImage.sprite = null;
    }
}

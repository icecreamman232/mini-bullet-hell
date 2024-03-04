using JustGame.Scripts.Data;
using JustGame.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class ShipSelectInfoPanel : MonoBehaviour
{
    [SerializeField] private Image m_shipPreviewImage;
    [SerializeField] private PlayerSettings m_settings;
    
    public void OnSelectShipAvatar(ShipAvatarButton avatar)
    {
        m_shipPreviewImage.sprite = avatar.ShipAvatar.sprite;
        m_settings.shipAttribute = avatar.ShipAttribute;
    }

    public void OnDeselectShipAvatar(ShipAvatarButton avatar)
    {
        m_shipPreviewImage.sprite = null;
    }
}

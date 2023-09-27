using JustGame.Scripts.Common;
using UnityEngine;
    
namespace JustGame.Scripts.UI
{
    public class MenuUIController : MonoBehaviour
    {
        [SerializeField] private PlayGameButton m_playGameButton;
        [SerializeField] private PlayGameButton m_shipSelectButton;
        
        private void Start()
        {
            m_playGameButton.OnClick += OnPressPlay;
            m_shipSelectButton.OnClick += OnPressShipSelect;
        }

        private void OnPressPlay()
        {
            SceneLoader.Instance.LoadToScene("MenuScene", "GameplayScene");
        }

        private void OnPressShipSelect()
        {
            SceneLoader.Instance.LoadToScene("MenuScene", "ShipSelectionScene");
        }

        private void OnDestroy()
        {
            m_playGameButton.OnClick -= OnPressPlay;
            m_shipSelectButton.OnClick -= OnPressShipSelect;
        }
    }
}


using JustGame.Scripts.Common;
using UnityEngine;
    
namespace JustGame.Scripts.UI
{
    public class MenuUIController : MonoBehaviour
    {
        [SerializeField] private PlayGameButton m_playGameButton;

        private void Start()
        {
            m_playGameButton.OnClick += OnPressPlay;
        }

        private void OnPressPlay()
        {
            SceneLoader.Instance.LoadToScene("MenuScene", "GameplayScene");
        }

        private void OnDestroy()
        {
            m_playGameButton.OnClick -= OnPressPlay;
        }
    }
}


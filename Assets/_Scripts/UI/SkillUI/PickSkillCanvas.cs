using System;
using JustGame.Scripts.Data;
using JustGame.Scripts.RuntimeSet;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.UI
{
    public class PickSkillCanvas : MonoBehaviour
    {
        [SerializeField] private RuntimeWorldSet m_runtimeWorldSet;
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private ChoosePowerUpButton m_choosePowerUpButton;
        [SerializeField] private PowerUpData m_selectedPowerUp;
        [SerializeField] private SkillCardUI[] m_skillCards;
        
        private void Start()
        {
            m_canvasGroup.alpha = 0;
            m_canvasGroup.interactable = false;

            for (int i = 0; i < m_skillCards.Length; i++)
            {
                m_skillCards[i].OnSelectAction += OnSelectCard;
            }

            m_choosePowerUpButton.OnClickUp += OnPressChooseButton;
            m_gameCoreEvent.OnChangeStateCallback += OnChangeGameState;
        }

        private void OnChangeGameState(GameState prevState, GameState newState)
        {
            switch (newState)
            {
                case GameState.FIGHTING:
                    Hide();
                    break;
                case GameState.PICK_SKILL:
                    Show();
                    break;
            }
        }
        
        private void Show()
        {
            m_canvasGroup.alpha = 1;
            m_canvasGroup.interactable = true;

            m_choosePowerUpButton.gameObject.SetActive(true);
            
            for (int i = 0; i < m_skillCards.Length; i++)
            {
                m_skillCards[i].gameObject.SetActive(true);
            }
            
            AssignPowerUpToCard();
        }

        private void Hide()
        {
            m_canvasGroup.alpha = 0;
            m_canvasGroup.interactable = false;

            m_choosePowerUpButton.gameObject.SetActive(false);
            
            for (int i = 0; i < m_skillCards.Length; i++)
            {
                m_skillCards[i].gameObject.SetActive(false);
            }
        }

        private void AssignPowerUpToCard()
        {
            var assignList = m_runtimeWorldSet.PowerUpManager.GetPowerUps();
            for (int i = 0; i < m_skillCards.Length; i++)
            {
                m_skillCards[i].AssignPowerUp(assignList[i]);
            }
        }
        
        private void OnSelectCard(SkillCardUI card)
        {
            m_selectedPowerUp = card.PowerUp;
        }

        private void OnPressChooseButton()
        {
            if (m_selectedPowerUp == null)
            {
                Debug.LogError("Not found powerup!");
            }
            m_selectedPowerUp.ApplyPowerUp();
            Hide();
            m_gameCoreEvent.SetGameState(GameState.FIGHTING);
        }

        private void OnDestroy()
        {
            m_gameCoreEvent.OnChangeStateCallback -= OnChangeGameState;
        }
    }
}

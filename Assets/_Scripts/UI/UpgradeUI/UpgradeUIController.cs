using DG.Tweening;
using JustGame.Scripts.Data;
using JustGame.Scripts.Managers;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.UI
{
    public class UpgradeUIController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        [SerializeField] private ChooseUpgradeButton m_chooseUpgradeButton;
        [SerializeField] private ActionEvent m_onPickedUpgradeEvent;
        [SerializeField] private UpgradeCardUI[] m_upgradeCards;

        private AttributeUpgradeBase m_selectedUpgrade;
        private UpgradeAttributeManager m_upgradeManager;
        
        private void Start()
        {
            m_gameCoreEvent.OnChangeStateCallback += OnChangeStateCallback;
            m_upgradeManager = UpgradeAttributeManager.Instance;

            for (int i = 0; i < m_upgradeCards.Length; i++)
            {
                m_upgradeCards[i].OnSelectAction += OnSelectCard;
            }

            m_chooseUpgradeButton.OnClickUp += OnPressChooseButton;
        }

        private void OnPressChooseButton()
        {
            if (m_selectedUpgrade == null)
            {
                Debug.LogError("Not found powerup!");
            }

            m_onPickedUpgradeEvent.Raise();
            m_selectedUpgrade.ApplyUpgrade();
            
            HideUI();
            m_gameCoreEvent.SetGameState(GameState.READY_TO_UPGRADE);
        }

        private void OnSelectCard(UpgradeCardUI card)
        {
            m_selectedUpgrade = card.UpgradeData;
        }
        
        private void OnChangeStateCallback(GameState prevState, GameState curState)
        {
            switch (curState)
            {
                case GameState.FIGHTING:
                    HideUI();
                    break;
                case GameState.PICK_UPGRADE:
                    ShowUI();
                    break;
            }
        }

        private void ShowUI()
        {
            m_canvasGroup.DOFade(1, 0.5f).OnComplete(OnFinishShow).SetUpdate(true);
        }

        private void OnFinishShow()
        {
            m_canvasGroup.interactable = true;
            
            m_chooseUpgradeButton.gameObject.SetActive(true);
            
            for (int i = 0; i < m_upgradeCards.Length; i++)
            {
                m_upgradeCards[i].gameObject.SetActive(true);
            }

            AssignUpgradeToCard();
        }

        private void AssignUpgradeToCard()
        {
            var assignList = UpgradeAttributeManager.Instance.GetUpgradeList(3);
            for (int i = 0; i < m_upgradeCards.Length; i++)
            {
                m_upgradeCards[i].AssignData(assignList[i]);
            }
        }

        private void HideUI()
        {
            m_canvasGroup.alpha = 0;
            m_canvasGroup.interactable = false;
            
            m_chooseUpgradeButton.gameObject.SetActive(false);
            
            for (int i = 0; i < m_upgradeCards.Length; i++)
            {
                m_upgradeCards[i].gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < m_upgradeCards.Length; i++)
            {
                m_upgradeCards[i].OnSelectAction -= OnSelectCard;
            }
        }
    }
}


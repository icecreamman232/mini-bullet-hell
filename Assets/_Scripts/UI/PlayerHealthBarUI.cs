using DG.Tweening;
using JustGame.Scripts.Data;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class PlayerHealthBarUI : MonoBehaviour
    {
        [SerializeField] [Min(0)] private float m_reduceSpeed;
        [SerializeField] [Min(0)] private float m_delayForDamageBar;
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        [SerializeField] private FloatEvent m_healthEvent;
        [Header("Bars")]
        [SerializeField] private Image m_damageImg;
        [SerializeField] private Image m_currentImg;
        [SerializeField] private RectTransform m_curImgRectTF;
        [SerializeField] private RectTransform m_damageImgRectTF;
        [SerializeField] private Image m_removeHPImg;
        [Header("Powerups")] 
        [SerializeField] private RevivePowerUp m_revivePowerUp;
        [SerializeField] private SacrificeHPPowerUp m_sacrificeHpPowerUp;
        
        
        private float m_target;
        private float m_delayCounter;
        
        private void Start()
        {
            m_canvasGroup.interactable = false;
            m_healthEvent.AddListener(UpdateHealthBar);
            m_gameCoreEvent.OnChangeStateCallback += OnChangeGameState;
            m_target = 1;
            m_damageImg.fillAmount = 1;
            m_currentImg.fillAmount = 1;
            m_removeHPImg.fillAmount = 0;
            m_sacrificeHpPowerUp.OnApplyPowerUp += OnSacrificeHP;
            m_revivePowerUp.OnPlayVFX += OnPlayReviveVFX;
        }

        private void OnShow()
        {
            m_canvasGroup.DOFade(1, 1f).SetUpdate(true);
        }
        
        private void OnHide()
        {
            m_canvasGroup.DOFade(0, 0.5f).SetUpdate(true);
        }
        
        private void Update()
        {
            m_delayCounter -= Time.deltaTime;
            if (m_delayCounter > 0)
            {
                return;
            }

            m_delayCounter = 0;
            
            if (m_damageImg.fillAmount <= m_target)
            {
                return;
            }

            m_damageImg.fillAmount -= Time.deltaTime * m_reduceSpeed;

            if (m_damageImg.fillAmount <= 0)
            {
                m_damageImg.fillAmount = 0;
            }
        }

        private void UpdateHealthBar(float percent)
        {
            m_delayCounter = m_delayForDamageBar;
            m_target = percent;
            m_currentImg.fillAmount = percent;
        }

        private void OnChangeGameState(GameState prevState, GameState curState)
        {
            if (curState == GameState.GAME_OVER)
            {
                OnHide();
            }
            else if (curState == GameState.FIGHTING)
            {
                OnShow();
            }
        }
        
        private void OnSacrificeHP()
        {
            m_removeHPImg.fillAmount += m_sacrificeHpPowerUp.HPPercentReduce;
            
            var anchorMaxCurrentImg = m_curImgRectTF.anchorMax;
            anchorMaxCurrentImg.x = 1 - m_removeHPImg.fillAmount;
            m_curImgRectTF.anchorMax = anchorMaxCurrentImg;
            
            var anchorMaxDamageImg = m_damageImgRectTF.anchorMax;
            anchorMaxDamageImg.x = 1 - m_removeHPImg.fillAmount;
            m_damageImgRectTF.anchorMax = anchorMaxDamageImg;
        }
        [ContextMenu("test vfx")]
        private void OnPlayReviveVFX()
        {
            var vfx = Instantiate(m_revivePowerUp.AnkhVFXPrefab, this.transform.parent);
            var rectTransform = vfx.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;
        }
        
        private void OnDestroy()
        {
            m_healthEvent.RemoveListener(UpdateHealthBar);
            m_sacrificeHpPowerUp.OnApplyPowerUp -= OnSacrificeHP;
            m_gameCoreEvent.OnChangeStateCallback -= OnChangeGameState;
            m_revivePowerUp.OnPlayVFX -= OnPlayReviveVFX;
        }
    }
}


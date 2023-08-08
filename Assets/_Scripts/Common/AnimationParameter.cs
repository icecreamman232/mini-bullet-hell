using UnityEngine;

namespace JustGame.Scripts.Common
{
    /// <summary>
    /// Play an animation depends on what we choose:trigger,bool,int etc...
    /// </summary>
    public class AnimationParameter : MonoBehaviour
    {
        [Tooltip("Leave it blank then the script will try to grab it on its game object")]
        public Animator TargetAnimator;
        public string ParameterName;
        public float Duration;
        private int m_animationParam; 
        
        // Start is called before the first frame update
        void Start()
        {
            if (TargetAnimator == null)
            {
                TargetAnimator = GetComponent<Animator>();
            }

            if (string.IsNullOrEmpty(ParameterName))
            {
                Debug.LogError($"Param name on {this.gameObject.name} not found");
                return;
            }
            m_animationParam = Animator.StringToHash(ParameterName);
        }

        private bool CheckCondition()
        {
            if (TargetAnimator == null)
            {
                Debug.LogError($"Animator on {this.gameObject.name} not found");
                return false;
            }

            if (m_animationParam == 0)
            {
                Debug.LogError($"Param name on {this.gameObject.name} not found");
                return false;
            }

            return true;
        }
        
        public void SetTrigger()
        {
            if (!CheckCondition())
            {
                return;
            }
            TargetAnimator.SetTrigger(m_animationParam);
        }
        
        public void SetBool(bool value)
        {
            if (!CheckCondition())
            {
                return;
            }
            TargetAnimator.SetBool(m_animationParam, value);
        }

        public void SetInt(int value)
        {
            if (!CheckCondition())
            {
                return;
            }
            TargetAnimator.SetInteger(m_animationParam, value);
        }
        
        public void SetFloat(float value)
        {
            if (!CheckCondition())
            {
                return;
            }
            TargetAnimator.SetFloat(m_animationParam, value);
        }
    }
}


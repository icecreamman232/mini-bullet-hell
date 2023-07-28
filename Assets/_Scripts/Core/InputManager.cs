using System;
using System.Collections.Generic;
using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public enum BindingAction
    {
        MOVE_LEFT,
        MOVE_RIGHT,
        MOVE_UP,
        MOVE_DOWN,
    }
    [Serializable]
    public struct ButtonAssign
    {
        public BindingAction keyAction;
        public KeyCode keyCode;
    }
    
    public enum ButtonStates
    {
        ButtonClicked,
        ButtonReleased,
        ButtonDown,
    }

    public class InputManager : PersistentSingleton<InputManager>
    {
        public ButtonAssign[] buttonsAssigns;
   
        private Dictionary<BindingAction, ButtonStates> m_buttonDictionary;
        private Dictionary<BindingAction, ButtonStates> m_mouseBtnDictionary;
        private bool m_isInputActive;

        public Action<Vector2> RightClickCallback;

        private bool m_hasFocus;
        public bool IsInputActive
        {
            get
            {
                return m_isInputActive;
            }
            set
            {
                m_isInputActive = value;
                for (int i = 0; i < buttonsAssigns.Length; i++)
                {
                    m_buttonDictionary[buttonsAssigns[i].keyAction] = ButtonStates.ButtonReleased;
                }
            }
        }
        protected override void Awake()
        {
            base.Awake();
            m_buttonDictionary = new Dictionary<BindingAction, ButtonStates>();
            
            for (int i = 0; i < buttonsAssigns.Length; i++)
            {
                m_buttonDictionary.Add(buttonsAssigns[i].keyAction,ButtonStates.ButtonReleased);
            }

            IsInputActive = true;
            m_hasFocus = true;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            IsInputActive = hasFocus;
        }

        private void Update()
        {
            if (!IsInputActive) return;
            for (int i = 0; i < buttonsAssigns.Length; i++)
            {
                if (Input.GetKeyDown(buttonsAssigns[i].keyCode))
                {
                    m_buttonDictionary[buttonsAssigns[i].keyAction] = ButtonStates.ButtonClicked;
                }
                else if (Input.GetKey(buttonsAssigns[i].keyCode))
                {
                    m_buttonDictionary[buttonsAssigns[i].keyAction] = ButtonStates.ButtonDown;
                }
                else if (Input.GetKeyUp(buttonsAssigns[i].keyCode))
                {
                    m_buttonDictionary[buttonsAssigns[i].keyAction] = ButtonStates.ButtonReleased;
                }
            }
            
        }

        public bool GetKeyClicked(BindingAction keyAction)
        {
            return (m_buttonDictionary[keyAction] == ButtonStates.ButtonClicked) && IsInputActive;
        }
        public bool GetKeyUp(BindingAction keyAction)
        {
            return (m_buttonDictionary[keyAction] == ButtonStates.ButtonReleased) && IsInputActive;
        }

        public bool GetKeyDown(BindingAction keyAction)
        {
            return (m_buttonDictionary[keyAction] == ButtonStates.ButtonDown) && IsInputActive;
        }

        public bool GetLeftClick()
        {
            return IsInputActive && Input.GetMouseButton(0);
        }
        
        public bool GetLeftClickDown()
        {
            return IsInputActive && Input.GetMouseButtonDown(0);
        }
        
        public bool GetLeftClickUp()
        {
            return IsInputActive && Input.GetMouseButtonUp(0);
        }
        
        public bool GetRightClickDown()
        {
            if (IsInputActive && Input.GetMouseButtonDown(1))
            {
                RightClickCallback?.Invoke(Input.mousePosition);
                return true;
            }
            return false;
        }
    }
}


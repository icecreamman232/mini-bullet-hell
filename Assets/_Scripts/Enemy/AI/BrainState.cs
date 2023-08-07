using System;
using System.Collections.Generic;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    [Serializable]
    public class BrainState
    {
        public string StateName;
        public List<BrainAction> Actions;
        public List<BrainTransition> Transitions;

        private EnemyBrain m_brain;
        
        public void Initialize(EnemyBrain brain)
        {
            m_brain = brain;
            
            foreach (var action in Actions)
            {
                if (action == null)
                {
                    continue;
                }
                action.Initialize(brain);
            }

            foreach (var transition in Transitions)
            {
                transition?.Initialize(brain);
            }
        }

        public void EnterState()
        {
            foreach (var action in Actions)
            {
                action.OnEnterState();
            }
            
            foreach (var transition in Transitions)
            {
                if (transition.BrainDecision != null)
                {
                    transition.BrainDecision.OnEnterState();
                }
            }
        }

        public void DoActions()
        {
            if (Actions.Count == 0) return;

            foreach (var action in Actions)
            {
                if (action != null)
                {
                    action.DoAction();
                }
                else
                {
                    Debug.LogError($"An action in {this.m_brain.gameObject.name} on state {StateName} is null");
                }
            }
        }

        public void CheckTransitions()
        {
            if (Transitions.Count == 0) return;

            foreach (var transition in Transitions)
            {
                if (transition.BrainDecision != null)
                {
                    if (transition.BrainDecision.CheckDecision())
                    {
                        if (!string.IsNullOrEmpty(transition.TrueStateName))
                        {
                            m_brain.TransitionToState(transition.TrueStateName);
                            break;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(transition.FalseStateName))
                        {
                            m_brain.TransitionToState(transition.FalseStateName);
                            break;
                        }
                    }
                }
            }
        }
        
        
        public void ExitState()
        {
            foreach (var action in Actions)
            {
                action.OnExitState();
            }
            
            foreach (var transition in Transitions)
            {
                if (transition.BrainDecision != null)
                {
                    transition.BrainDecision.OnExitState();
                }
            }
        }
    }
}


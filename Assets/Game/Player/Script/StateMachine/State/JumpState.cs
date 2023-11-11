using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomStateMachine
{
    public class JumpState : AbstractStateBase
    {
        public const string STATE_NAME = "Jump";
        
        public JumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
            _conditions.Add(() =>
            {
                if (!_playerStateMachine.MoveController.IsJumping)
                {
                    if (_playerStateMachine.MoveController.CurrentSqrtSpeed > 0.01F)
                    {
                        _nextStateName = MoveState.STATE_NAME;
                    }
                    else
                    {
                        _nextStateName = IdleState.STATE_NAME;
                    }

                    return true;
                }

                return false;
            });
        }

        public override string StateName => STATE_NAME;
        public override void OnEntry()
        {
            _playerStateMachine.MoveController.OnJumpStateEntry();
        }

        public override void OnLateUpdate()
        {
        }

        public override void OnFixedUpdate()
        {
        }

        public override void OnExit()
        {
            if (_nextStateName != MoveState.STATE_NAME)
            {
                _playerStateMachine.MoveController.Stop();
            }
            
            base.OnExit();
        }
    }    
}

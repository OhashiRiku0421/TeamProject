using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CustomStateMachine
{
    public class RunState : AbstractStateBase
    {
        /// <summary>ステート名</summary>
        public const string STATE_NAME = "Run";
        
        public RunState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
            // 走り終わった際
            _conditions.Add(() =>
            {
                if (!_playerStateMachine.MoveController.IsRunning || _playerStateMachine.MoveController.CurrentSqrtSpeed < 0.01F)
                {
                    _nextStateName = WalkState.STATE_NAME;
                    return true;
                }

                return false;
            });
            // 攻撃の遷移
            _conditions.Add(() =>
            {
                if (_playerStateMachine.AttackController.IsAttackAnimation)
                {
                    if (_playerStateMachine.AttackController.IsCloseRange)
                    {
                        _nextStateName = CloseRangeAttackState.STATE_NAME;
                    }
                    else
                    {
                        _nextStateName = LongRangeAttackState.STATE_NAME;
                    }

                    return true;
                }

                return false;
            });
        }

        public override void OnEntry()
        {
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override void OnLateUpdate()
        {
        }

        public override void OnFixedUpdate()
        {
            _playerStateMachine.MoveController.OnFixedUpdate();
        }

        public override void OnExit()
        {
            if (_nextStateName != WalkState.STATE_NAME)
            {
                _playerStateMachine.MoveController.Stop();
            }
            
            base.OnExit();
        }
    }   
}

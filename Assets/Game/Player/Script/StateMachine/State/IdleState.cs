using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomStateMachine
{
    public class IdleState : AbstractStateBase
    {
        /// <summary>ステート名</summary>
        public const string STATE_NAME = "Idle";
        
        public IdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
            // 歩き出した際の条件追加
            _conditions.Add(() =>
            {
                if (_playerStateMachine.MoveController.CurrentSqrtSpeed > 0.01F)
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
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }   
}

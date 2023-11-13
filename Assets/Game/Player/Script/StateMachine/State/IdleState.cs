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
                    _nextStateName = MoveState.STATE_NAME;
                    return true;
                }

                return false;
            });
            // 攻撃の遷移
            _conditions.Add(() =>
            {
                if (_playerStateMachine.AttackController.IsAttackAnimation)
                {
                    _nextStateName = AttackState.STATE_NAME;
                    return true;
                }

                return false;
            });
            // 採集の遷移
            _conditions.Add(() =>
            {
                if (_playerStateMachine.CollectController.IsCollecting)
                {
                    _nextStateName = CollectingState.STATE_NAME;
                    return true;
                }

                return false;
            });
            // 回避中の遷移
            _conditions.Add(() =>
            {
                if (_playerStateMachine.AvoidController.IsAvoiding)
                {
                    _nextStateName = AvoidanceState.STATE_NAME;
                    return true;
                }

                return false;
            });
            // ジャンプの遷移
            _conditions.Add(() =>
            {
                if (_playerStateMachine.MoveController.IsJumping)
                {
                    _nextStateName = JumpState.STATE_NAME;
                    return true;
                }

                return false;
            });
        }

        public override string StateName => STATE_NAME;

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
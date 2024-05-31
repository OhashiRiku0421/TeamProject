using System.Collections;
using System.Collections.Generic;
using CustomStateMachine;
using UnityEngine;

namespace CustomStateMachine
{
    public class MoveState : AbstractStateBase
    {
        /// <summary>ステート名</summary>
        public const string STATE_NAME = "Move";

        public MoveState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
            // 止まった際の処理
            _conditions.Add(() =>
            {
                if (_playerStateMachine.MoveController.CurrentSqrtSpeed <= 0.01F)
                {
                    _nextStateName = IdleState.STATE_NAME;
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
            // 回避の遷移
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
            _playerStateMachine.MoveController.OnFixedUpdateMoveState();
        }

        public override void OnExit()
        {
            // 他のステートに遷移する際は停止する
            if (_nextStateName != JumpState.STATE_NAME)
            {
                _playerStateMachine.MoveController.Stop();
            }
            
            base.OnExit();
        }
    }
}
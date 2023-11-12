using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomStateMachine
{
    public class AttackState : AbstractStateBase
    {
        /// <summary>ステート名</summary>
        public const string STATE_NAME = "CloseRangeAttack";

        public AttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
            // Idleへの遷移
            _conditions.Add(() =>
            {
                if (!_playerStateMachine.AttackController.IsAttackAnimation 
                    && _playerStateMachine.MoveController.CurrentSqrtSpeed < 0.01F)
                {
                    _nextStateName = IdleState.STATE_NAME;
                    return true;
                }

                return false;
            });
            // 移動への遷移
            _conditions.Add(() =>
            {
                if (!_playerStateMachine.AttackController.IsAttackAnimation
                    && _playerStateMachine.MoveController.CurrentSqrtSpeed >= 0.01F)
                {
                    _nextStateName = IdleState.STATE_NAME;
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
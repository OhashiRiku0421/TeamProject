using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomStateMachine
{
    public class AvoidanceState : AbstractStateBase
    {
        /// <summary>ステート名</summary>
        public const string STATE_NAME = "Avoidance";
        
        public AvoidanceState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
            // アイドルに遷移
            _conditions.Add(() =>
            {
                if (!_playerStateMachine.AvoidController.IsAvoiding)
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
            _playerStateMachine.AvoidController.OnSteteEntry();
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
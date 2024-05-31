using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomStateMachine
{
    public class CloseRangeAttackState : AbstractStateBase
    {
        /// <summary>ステート名</summary>
        public const string STATE_NAME = "CloseRangeAttack";
        
        public CloseRangeAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
            _conditions.Add(() =>
            {
                if (!_playerStateMachine.AttackController.IsAttackAnimation)
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

using System.Collections;
using System.Collections.Generic;
using CustomStateMachine;
using UnityEngine;

namespace CustomStateMachine
{
    public class LongRangeAttackState : AbstractStateBase
    {
        /// <summary>ステート名</summary>
        public const string STATE_NAME = "LongRangeAttack";
        
        public LongRangeAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
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

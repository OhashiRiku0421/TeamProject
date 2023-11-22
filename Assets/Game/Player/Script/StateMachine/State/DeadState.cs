using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomStateMachine
{
    public class DeadState : AbstractStateBase
    {
        public const string STATE_NAME = "Dead";
        
        public DeadState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override string StateName => STATE_NAME;
        public override void OnEntry()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override void OnLateUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
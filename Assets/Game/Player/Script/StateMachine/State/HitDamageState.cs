using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomStateMachine
{
    public class HitDamageState : AbstractStateBase
    {
        /// <summary>ステート名</summary>
        public const string STATE_NAME = "Damage";
        
        public HitDamageState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void OnEntry()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }
    }    
}
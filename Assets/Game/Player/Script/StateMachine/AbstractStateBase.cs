using System;
using System.Collections.Generic;


namespace CustomStateMachine
{
    /// <summary>ステートの基底クラス</summary>
    public abstract class AbstractStateBase
    {
        /// <summary>シーンを遷移する条件</summary>
        protected List<Func<bool>> _conditions = new List<Func<bool>>();

        protected string _nextStateName = "";

        /// <summary>ステートマシーンのインスタンス</summary>
        protected PlayerStateMachine _playerStateMachine = default;

        public AbstractStateBase(PlayerStateMachine playerStateMachine)
        {
            _playerStateMachine = playerStateMachine;
        }
        
        /// <summary>ステート名</summary>
        public abstract string StateName { get; }

        /// <summary>このステートに遷移した際に呼ばれる処理</summary>
        public abstract void OnEntry();

        /// <summary>このステートが現在のステートの時にUpdateのタイミングで呼び出される処理</summary>
        public virtual void OnUpdate()
        {
            foreach (var VARIABLE in _conditions)
            {
                if (VARIABLE.Invoke())
                {
                    _playerStateMachine.TransitionTo(_nextStateName);
                    break;
                }
            }
        }

        /// <summary>このステートが現在のステートの時にLateUpdateのタイミングで呼び出される処理</summary>
        public abstract void OnLateUpdate();

        /// <summary>このステートが現在のステートの時にFixedUpdateのタイミングで呼び出される処理</summary>
        public abstract void OnFixedUpdate();

        /// <summary>このステートから遷移する際に呼び出される処理</summary>
        public virtual void OnExit()
        {
            _nextStateName = "";
        }
    }
}
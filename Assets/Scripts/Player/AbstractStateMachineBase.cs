using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomStateMachine
{
    public abstract class AbstractStateMachineBase : MonoBehaviour
    {
        /// <summary>現在のステート</summary>
        public AbstractStateBase CurrentState { get; protected set; }

        /// <summary>ステートが変わった際に呼ばれるAction</summary>
        protected Action<AbstractStateBase> _onStateChanged = default;

        /// <summary>ステートが変わった際に呼ばれるAction</summary>
        public event Action<AbstractStateBase> OnStateChanged
        {
            add => _onStateChanged += value;
            remove => _onStateChanged -= value;
        }

        /// <summary>初期化 Start時に実行</summary>
        protected void Initialize()
        {
            // ステートを初期化する
            CurrentState = StateInit();
            CurrentState.OnEntry();

            // スタートした時点でActionを呼ぶ
            _onStateChanged?.Invoke(CurrentState);
        }

        /// <summary>ステートの情報を初期化する Start時に呼び出し</summary>
        /// <returns>初期ステート</returns>
        protected abstract AbstractStateBase StateInit();

        /// <summary>遷移を行う関数</summary>
        /// <param name="nextStateName">次のステートの名前</param>
        public abstract void TransitionTo(string nextStateName);

        private void Start() => Initialize();
        private void Update() => CurrentState?.OnUpdate();
        private void LateUpdate() => CurrentState?.OnLateUpdate();
        private void FixedUpdate() => CurrentState?.OnFixedUpdate();
    }
}
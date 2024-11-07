using System;
using System.Collections;
using System.Collections.Generic;
using Lim.Generic;
using UnityEngine;

namespace Lim.System.Defence
{
    public enum DefenceState
    {
        None = 0,
        Wait,
        Ready,
        Start,
        Defending,
        End,
        Success,
        Fail,
    }
    
    public class DefenceSystem : MonoSingleton<DefenceSystem>
    {
        private Dictionary<DefenceState, BaseDefenceState> _stateDic;
        private BaseDefenceState _curState;

        public DefenceState TestState;
        
        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ChangeState(TestState);
            }
        }

        private void Initialize()
        {
            _stateDic ??= new Dictionary<DefenceState, BaseDefenceState>();
            _stateDic.Clear();
            
            BaseDefenceState[] states = GetComponentsInChildren<BaseDefenceState>(true);

            for (int i = 0; i < states.Length; i++)
            {
                _stateDic.TryAdd(states[i].State, states[i]);
            }
            
            ChangeState(DefenceState.Wait);
        }

        public void ChangeState(DefenceState state)
        {
            if (state == _curState?.State) return;
            
            _curState?.gameObject.SetActive(false);
            
            _stateDic[state].gameObject.SetActive(true);
            _curState = _stateDic[state];
        }
    }
}
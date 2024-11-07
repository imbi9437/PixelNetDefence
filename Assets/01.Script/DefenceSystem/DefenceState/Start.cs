using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lim.System.Defence
{
    public class Start : BaseDefenceState
    {
        public override DefenceState State => DefenceState.Start;
        
        private void OnEnable()
        {
            Debug.Log("Start Enable");
        }
        
        private void Update()
        {
            Debug.Log("Starting");
        }
        
        private void OnDisable()
        {
            Debug.Log("Start Disable");
        }
    }
}
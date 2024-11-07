using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lim.System.Defence
{
    public class Wait : BaseDefenceState
    {
        public override DefenceState State => DefenceState.Wait;


        private void OnEnable()
        {
            Debug.Log("Wait Enable");
        }
        
        private void Update()
        {
            //Debug.Log("Waiting");
        }
        
        private void OnDisable()
        {
            Debug.Log("Wait Disable");
        }
    }
}
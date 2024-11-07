using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lim.System.Defence
{
    public class Fail : BaseDefenceState
    {
        public override DefenceState State => DefenceState.Fail;
        
        private void OnEnable()
        {
            Debug.Log("Fail Enable");
        }
        
        private void Update()
        {
            Debug.Log("Failing");
        }
        
        private void OnDisable()
        {
            Debug.Log("Fail Disable");
        }
    }
}
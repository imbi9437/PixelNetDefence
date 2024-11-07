using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lim.System.Defence
{
    public class Success : BaseDefenceState
    {
        public override DefenceState State => DefenceState.Success;
        
        private void OnEnable()
        {
            Debug.Log("Success Enable");
        }
        
        private void Update()
        {
            Debug.Log("Success Doing");
        }
        
        private void OnDisable()
        {
            Debug.Log("Success Disable");
        }
    }
}
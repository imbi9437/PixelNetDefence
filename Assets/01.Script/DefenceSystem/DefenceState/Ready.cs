using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lim.System.Defence
{
    public class Ready : BaseDefenceState
    {
        public override DefenceState State => DefenceState.Ready;
        
        private void OnEnable()
        {
            Debug.Log("Ready Enable");
        }
        
        private void Update()
        {
            //Debug.Log("Ready Doing");
        }
        
        private void OnDisable()
        {
            Debug.Log("Ready Disable");
        }
    }
}
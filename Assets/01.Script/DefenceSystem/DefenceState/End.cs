using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lim.System.Defence
{
    public class End : BaseDefenceState
    {
        public override DefenceState State => DefenceState.End;
        
        private void OnEnable()
        {
            Debug.Log("End Enable");
        }
        
        private void Update()
        {
            Debug.Log("Ending");
        }
        
        private void OnDisable()
        {
            Debug.Log("End Disable");
        }
    }
}
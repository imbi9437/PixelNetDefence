using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lim.System.Defence
{
    public abstract class BaseDefenceState : MonoBehaviour
    {
        public abstract DefenceState State { get; }
        protected DefenceSystem _system;

        public void Initialize(DefenceSystem system)
        {
            _system = system;
        }
    }
}
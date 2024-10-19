using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INode
{
    public enum NodeState
    {
        Running = 1 << 1,
        Success = 1 << 2,
        Fail = 1 << 3,
    }

    public NodeState Evaluate();
    public void Clear();
}

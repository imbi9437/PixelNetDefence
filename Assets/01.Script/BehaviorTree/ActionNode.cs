using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : INode
{
    private Func<INode.NodeState> _onUpdate = null;

    public ActionNode(Func<INode.NodeState> onUpdate)
    {
        _onUpdate = onUpdate;
    }

    public INode.NodeState Evaluate() => _onUpdate?.Invoke() ?? INode.NodeState.Fail;
    public void Clear() => _onUpdate = null;
}
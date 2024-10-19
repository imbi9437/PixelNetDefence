using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoratorNode : INode
{
    private INode _child;
    private Func<bool> _checker;

    public DecoratorNode(INode child, Func<bool> checker)
    {
        _child = child;
        _checker = checker;
    }

    public INode.NodeState Evaluate()
    {
        if (_checker.Invoke())
            return _child?.Evaluate() ?? INode.NodeState.Fail;
        
        return INode.NodeState.Fail;
    }

    public void Clear()
    {
        _child.Clear();
        _child = null;
    }
}

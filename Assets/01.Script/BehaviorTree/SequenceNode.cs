using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : INode
{
    private List<INode> _childNodes;

    public SequenceNode(List<INode> childNodes)
    {
        _childNodes = childNodes;
    }
    
    public INode.NodeState Evaluate()
    {
        if (_childNodes == null) return INode.NodeState.Fail;

        foreach (var childNode in _childNodes)
        {
            switch (childNode.Evaluate())
            {
                case INode.NodeState.Running:
                    return INode.NodeState.Running;
                case INode.NodeState.Success:
                    continue;
                case INode.NodeState.Fail:
                    return INode.NodeState.Fail;
            }
        }

        return INode.NodeState.Success;
    }

    public void Clear()
    {
        foreach (var childNode in _childNodes)
        {
            childNode.Clear();
        }
        
        _childNodes.Clear();
    }
}

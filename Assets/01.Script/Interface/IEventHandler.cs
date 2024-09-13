using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lim.Interface
{
    public interface IEventHandler
    {
        public void ExecuteEvent<T>(object sender, EventHandler<T> handler, T args) where T : EventArgs;
    }
}
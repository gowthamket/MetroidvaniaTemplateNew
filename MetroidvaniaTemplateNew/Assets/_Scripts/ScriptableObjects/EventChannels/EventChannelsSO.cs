using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Weapons;
using UnityEngine;

namespace Scripts.EventChannels
{
    public abstract class EventChannelsSO<T> : ScriptableObject
    {
        public event EventHandler<T> OnEvent;

        public void RaiseEvent(object sender, T context)
        {
            OnEvent?.Invoke(sender, context);
        }
    }
}

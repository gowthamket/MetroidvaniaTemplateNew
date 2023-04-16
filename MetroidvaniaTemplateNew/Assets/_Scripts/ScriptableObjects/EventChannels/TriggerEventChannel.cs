using System;
using UnityEngine;

namespace Scripts.EventChannels
{
    [CreateAssetMenu(fileName = "newTriggerChannel", menuName = "Event Channels/Trigger")]
    public class TriggerEventChannel : EventChannelsSO<EventArgs>
    {
    }
}
using System;
using Base;
using CorePlugin.Cross.Events.Interface;
using CorePlugin.Extensions;
using ObjectSystem.ObjectBase.Interfaces;
using UnityEngine;

namespace ReceiveColorZone.Scripts
{
    public class Receiver : MonoBehaviour, IEventHandler
    {
        private event GeneralEvents.GetRandomColor GetRandomColor;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IReceiver>(out var damageable))
            {
                damageable.ReceiveColor(GetRandomColor!.Invoke());
            }
        }
        public void InvokeEvents()
        {
            
        }
        public void Subscribe(params Delegate[] subscribers)
        {
            EventExtensions.Subscribe(ref GetRandomColor, subscribers);
        }
        public void Unsubscribe(params Delegate[] unsubscribers)
        {
            EventExtensions.Unsubscribe(ref GetRandomColor,unsubscribers);
        }
    }
}

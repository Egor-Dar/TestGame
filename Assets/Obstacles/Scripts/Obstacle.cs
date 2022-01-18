using System;
using Base;
using CorePlugin.Core;
using CorePlugin.Cross.Events.Interface;
using CorePlugin.Extensions;
using ObjectSystem.ObjectBase;
using ObjectSystem.ObjectBase.Interfaces;
using UnityEngine;

namespace Obstacles.Scripts
{
    public sealed class Obstacle : Moving, IEventHandler
    {
        private event GeneralEvents.GetRandomColor GetColor;

        public override void Initialize(Action<IPoolObject> onRelease)
        {
            base.Initialize(onRelease);
            EventInitializer.AddHandler(this);
                meshRenderer.material.color = GetColor!.Invoke();
        }
        public override void OnTriggerEnter(Collider other)
        {
            if (markedForDie) return;
            if (other.TryGetComponent<IReceiver>(out var damageable))
            {
                onReleaseNeeded.Invoke(this);
                damageable.ReceiveDamage(meshRenderer.material.color, damage);
            }
        }
        
        public void InvokeEvents()
        {

        }
        public void Subscribe(params Delegate[] subscribers)
        {
            EventExtensions.Subscribe(ref GetColor, subscribers);
        }
        public void Unsubscribe(params Delegate[] unsubscribers)
        {
            EventExtensions.Unsubscribe(ref GetColor, unsubscribers);
        }
    }
}

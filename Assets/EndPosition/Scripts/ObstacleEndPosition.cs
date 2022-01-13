using System;
using Base;
using CorePlugin.Cross.Events.Interface;
using CorePlugin.Extensions;
using ObjectSystem.ObjectBase.Interfaces;
using UnityEngine;

namespace EndPosition.Scripts
{
    [RequireComponent(typeof(BoxCollider))]
    public class ObstacleEndPosition : MonoBehaviour, IEventHandler
    {
        private event PoolDelegates.ReleasePoolObjectObstacles ObstaclePool;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IPoolObject>(out var poolObject))
            {
                ObstaclePool!.Invoke(poolObject);
            }
        }

        public void InvokeEvents()
        {
            
        }

        public void Subscribe(params Delegate[] subscribers)
        {
            EventExtensions.Subscribe(ref ObstaclePool, subscribers);
        }

        public void Unsubscribe(params Delegate[] unsubscribers)
        {
            EventExtensions.Unsubscribe(ref ObstaclePool, unsubscribers);
        }
    }
}

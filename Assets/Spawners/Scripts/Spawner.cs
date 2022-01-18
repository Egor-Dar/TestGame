using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Base;
using CorePlugin.Cross.Events.Interface;
using CorePlugin.Extensions;
using ObjectSystem.ObjectBase.Interfaces;
using Obstacles.Scripts;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Spawners.Scripts
{
    public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour, IPoolObject
    {
        [SerializeField] private protected T[] prefabs;
        [SerializeField] private protected int defaultCapacity = 10;
        [SerializeField] private protected int maxSize = 100;

        private protected Vector3 prefabPosition;
        private protected ObjectPool<IPoolObject> ObjectPool;
        private protected Dictionary<Type, IPoolObject[]> PoolObjects;
        
        private protected virtual void Start()
        {
            PoolObjects = new Dictionary<Type, IPoolObject[]>
            {
                {typeof(Obstacle), prefabs.Cast<IPoolObject>().ToArray()},
            };

            ObjectPool = new ObjectPool<IPoolObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy,
                                                     true, defaultCapacity, maxSize);
        }

        private protected virtual void ActionOnDestroy(IPoolObject obstacle)
        {
            obstacle.Destroy();
        }

        private protected virtual void ActionOnRelease(IPoolObject obstacle)
        {
            obstacle.OnRelease();
            obstacle.SetActive(false);
        }

        private protected virtual IPoolObject CreateFunc()
        {
            var keys = PoolObjects.Keys.ToArray();
            var randomKey = keys[Random.Range(0, keys.Length)];
            var value = PoolObjects[randomKey];
            var poolObject = value[Random.Range(0, value.Length)].Instantiate();
            return poolObject;
        }

        private protected abstract IEnumerator GetSpawnPosition();

        private protected virtual void ActionOnGet(IPoolObject obstacle)
        {
            obstacle.OnGet();
            obstacle.SetActive(true);
            obstacle.SetPosition(prefabPosition);
            obstacle.ResetState();
        }

        private protected virtual void OnDestroy()
        {
            ObjectPool.Clear();
        }

        private protected virtual void OnRelease(IPoolObject poolObject)
        { 
            poolObject.ResetState();
            if (poolObject.IsReleased) return;
            ObjectPool?.Release(poolObject);
        }
    }
}

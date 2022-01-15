using System;
using System.Collections;
using Base;
using CorePlugin.Attributes.EditorAddons;
using CorePlugin.Cross.Events.Interface;
using CorePlugin.Extensions;
using ObjectSystem.ObjectBase.Interfaces;
using Obstacles.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners.Scripts
{
    [CoreManagerElement]
    public class SpawnObstacle : Spawner<Obstacle>, IEventSubscriber, IEventHandler
    {
        private protected Vector3 pos1;
        private protected Vector3 pos2;
        private protected int ObstaclesCount;
        private protected int createdObstaclesCount;

        private event PoolDelegates.GetPositionData GetPositionData;
        private event PoolDelegates.GetLengthData GetLengthData;

        private protected override void Start()
        {
            base.Start();
            StartCoroutine(Spawn());
        }

        private protected override IEnumerator GetSpawnPosition(int index)
        {
            var rows=2;
            var vector3 = GetPositionData!.Invoke(index).pos1;
            var columns=10;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Vector3 position = new Vector3(vector3.x+i,1,vector3.z+j);
                    prefabPosition = position;
                }
            }
            yield return null;
        }

        private IEnumerator Spawn()
        {
            var dataLenght= GetLengthData!.Invoke();
            for (int data = 0; data < dataLenght; data++)
            {
                StartCoroutine(GetSpawnPosition(data));
                ObstaclesCount = GetPositionData!.Invoke(data).obstacleCount;
                while (createdObstaclesCount < ObstaclesCount)
                {
                    yield return new WaitForSeconds(0);
                    var newObstacle = ObjectPool.Get();
                    newObstacle.Initialize(OnRelease);
                    createdObstaclesCount++;
                }
            }
        }

        public Delegate[] GetSubscribers()
        {
            return new Delegate[]
            {
                (PoolDelegates.ReleasePoolObjectObstacles) OnRelease
            };
        }
        public void InvokeEvents()
        {

        }
        public void Subscribe(params Delegate[] subscribers)
        {
            EventExtensions.Subscribe(ref GetPositionData, subscribers);
            EventExtensions.Subscribe(ref GetLengthData, subscribers);
        }
        public void Unsubscribe(params Delegate[] unsubscribers)
        {
            EventExtensions.Unsubscribe(ref GetPositionData, unsubscribers);
            EventExtensions.Unsubscribe(ref GetLengthData, unsubscribers);
        }

    }
}

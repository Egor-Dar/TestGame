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
        [SerializeField] private float dist;
        [SerializeField] private float RandomValue;
        private int ObstaclesCount;
        private int createdObstaclesCount;

        private event PoolDelegates.GetPositionData GetPositionData;
        private event PoolDelegates.GetLengthData GetLengthData;

        private protected override void Start()
        {
            base.Start();

            StartCoroutine(GetSpawnPosition());
        }

        private protected override IEnumerator GetSpawnPosition()
        {
            var dataLenght = GetLengthData!.Invoke();
            for (int data = 0; data < dataLenght; data++)
            {
                var pos1 = GetPositionData!.Invoke(data).pos1;
                var pos2 = GetPositionData!.Invoke(data).pos2;
                var rows = 4;
                createdObstaclesCount = 0;
                ObstaclesCount = GetPositionData!.Invoke(data).obstacleCount;
                var columns = 4*(int)pos2.x;
                for (float i = 0; i < rows; i++)
                {
                    bool lastCooperationSpawn = false;
                    for (int j = 0; j < columns; j++)
                    {
                        if (createdObstaclesCount >= ObstaclesCount) continue;
                        if (!lastCooperationSpawn)
                        {
                            if (Random.value > RandomValue)
                            {
                                Vector3 position = new Vector3(pos1.x + (dist * i), 1, pos1.z + (j * 2));
                                if (position.x > pos2.x || position.x < pos1.x || position.z < pos1.z || position.z > pos2.z) continue;
                                lastCooperationSpawn = true;
                                prefabPosition = position;
                                var newObstacle = ObjectPool.Get();
                                newObstacle.Initialize(OnRelease);
                                createdObstaclesCount++;
                            }
                        }
                        else
                        {
                            lastCooperationSpawn = false;
                        }

                    }
                }


            }
            yield return null;
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

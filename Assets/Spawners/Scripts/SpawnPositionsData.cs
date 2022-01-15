using System;
using Base;
using CorePlugin.Cross.Events.Interface;
using UnityEngine;

namespace Spawners.Scripts
{
    public class SpawnPositionsData : MonoBehaviour, IEventSubscriber
    {
        [SerializeField] private PositionData[] data;
        private int GetLenghtData() => data.Length;
        private PositionData GetPosData(int index) => data[index];

        public Delegate[] GetSubscribers()
        {
            return new Delegate[]
            {
                (PoolDelegates.GetPositionData) GetPosData,
                (PoolDelegates.GetLengthData) GetLenghtData
            };
        }
    }
    [Serializable]
    public struct PositionData
    {
        public Vector3 pos1;
        public Vector3 pos2;
        public int obstacleCount;
    }
}

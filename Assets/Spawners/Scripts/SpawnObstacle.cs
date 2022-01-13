using System;
using System.Collections;
using Base;
using CorePlugin.Attributes.EditorAddons;
using CorePlugin.Cross.Events.Interface;
using ObjectSystem.ObjectBase;
using ObjectSystem.ObjectBase.Interfaces;
using Obstacles.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners.Scripts
{
    [CoreManagerElement]
    public class SpawnObstacle : Spawner<Obstacle>, IEventSubscriber
    {
        [SerializeField] private Vector3 SpawnPos;
        private Coroutine spawn;

        private protected override void Start()
        {
            base.Start();
            spawn = StartCoroutine(Spawn());
        }

        private protected override IPoolObject CreateFunc()
        {
            var obstacle = base.CreateFunc();
            onPauseChanged += ((Moving) obstacle).OnPause;
            return obstacle;
        }

        private protected override void ActionOnDestroy(IPoolObject obstacle)
        {
            onPauseChanged -= ((Moving) obstacle).OnPause;
            base.ActionOnDestroy(obstacle);
        }

        private protected override Vector3 GetSpawnPosition()
        {
            return new Vector3(Random.Range(-SpawnPos.x, SpawnPos.x), SpawnPos.y, SpawnPos.z);
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(0.5f, 1f));
                var newObstacle = ObjectPool.Get();
                newObstacle.Initialize(OnRelease);
            }
        }

        public Delegate[] GetSubscribers()
        {
            return new Delegate[]
            {
                (PoolDelegates.ReleasePoolObjectObstacles) OnRelease
            };
        }
    }
}

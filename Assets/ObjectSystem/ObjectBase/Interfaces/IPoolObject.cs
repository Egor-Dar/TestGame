using System;
using UnityEngine;

namespace ObjectSystem.ObjectBase.Interfaces
{
    public interface IPoolObject
    {
        public bool IsReleased { get; }
        public void SetActive(bool state);
        public void ResetState();
        public void Destroy();
        public void Initialize(Action<IPoolObject> onRelease);
        public IPoolObject Instantiate();
        public void SetPosition(Vector3 newPos);
        public void OnGet();
        public void OnRelease();
    }
}
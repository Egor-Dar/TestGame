using ObjectSystem.ObjectBase;
using ObjectSystem.ObjectBase.Interfaces;
using UnityEngine;

namespace Obstacles.Scripts
{
    public sealed class Obstacle : Moving
    {
        private void Reset()
        {
            rigidbody ??= GetComponent<Rigidbody>();
            meshRenderer ??= GetComponent<MeshRenderer>();
        }
        public override void OnTriggerEnter(Collider other)
        {
            if (paused) return;
            if (markedForDie) return;
            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                if (damageable is Obstacle) return;
                onReleaseNeeded.Invoke(this);
                damageable.ReceiveDamage(color, damage);
            }
        }
        private protected override Vector3 GetFlyDirection()
        {
            return Vector3.forward;
        }
    }
}

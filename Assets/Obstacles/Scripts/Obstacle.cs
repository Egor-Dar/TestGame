using ObjectSystem.ObjectBase;
using ObjectSystem.ObjectBase.Interfaces;
using UnityEngine;

namespace Obstacles.Scripts
{
    public sealed class Obstacle : Moving
    {
        public override void OnTriggerEnter(Collider other)
        {
            if (markedForDie) return;
            if (other.TryGetComponent<IReceiver>(out var damageable))
            {
                if (damageable is Obstacle) return;
                onReleaseNeeded.Invoke(this);
                damageable.ReceiveDamage(materialPropertyBlock.GetColor(0), damage);
            }
        }
    }
}

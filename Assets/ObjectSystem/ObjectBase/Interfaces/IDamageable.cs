using UnityEngine;

namespace ObjectSystem.ObjectBase.Interfaces
{
    public interface IDamageable
    {
        public void ReceiveDamage(Color colorCheck, int receivedDamage);
    }
}

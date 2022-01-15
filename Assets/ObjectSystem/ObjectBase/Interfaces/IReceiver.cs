using UnityEngine;

namespace ObjectSystem.ObjectBase.Interfaces
{
    public interface IReceiver
    {
        public void ReceiveDamage(Color colorCheck, int receivedDamage);
        public void ReceiveColor(Color color);
    }
}

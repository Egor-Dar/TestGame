using UnityEngine;

namespace Input.Scripts
{
    public class PCInput : CrossInput
    {
        public override void Execute()
        {
            if(paused) return;
            Move?.Invoke( new Vector2(0, UnityEngine.Input.GetAxis("Vertical")));
        }
    }
}

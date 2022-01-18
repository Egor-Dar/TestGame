using System;
using System.Linq;
using Base;
using UnityEngine;

namespace Input.Scripts
{
    public class AndroidInput : CrossInput
    {
        private Joystick.Scripts.Joystick Joystick;
        private Joystick.Scripts.Joystick GetJoystick(Joystick.Scripts.Joystick joystick)
        {
            return Joystick = joystick;
        }

        public override void Execute()
        {
            if (paused) return;
            Move?.Invoke(new Vector3(Joystick.inputVector.x,0,1));
        }

        public override Delegate[] GetSubscribers()
        {
            var based = base.GetSubscribers();
            return based.Append((GeneralEvents.GetJoystick) GetJoystick).ToArray();
        }
    }
}

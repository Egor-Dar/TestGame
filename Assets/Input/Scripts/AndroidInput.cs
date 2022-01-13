using System;
using System.Linq;
using Base;
using Joystick_Pack.Scripts.Base;
using UnityEngine;

namespace Input.Scripts
{
    public class AndroidInput : CrossInput
    {
        private Joystick Joystick;
        private Joystick GetJoystick(Joystick joystick)
        {
            return Joystick = joystick;
        }

        public override void Execute()
        {
            if (paused) return;
            Move?.Invoke(new Vector2(-Joystick.Horizontal,0));
        }

        public override Delegate[] GetSubscribers()
        {
            var based = base.GetSubscribers();
            return based.Append((GeneralEvents.GetJoystick) GetJoystick).ToArray();
        }
    }
}

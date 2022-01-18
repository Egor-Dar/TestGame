using System;
using Base;
using CorePlugin.Attributes.Validation;
using CorePlugin.Cross.Events.Interface;
using UnityEngine;

namespace PlayerCamera.Scripts
{
    public class PlayerCamera : MonoBehaviour, IEventSubscriber
    {
        [SerializeField] [NotNull] private Transform rig;
        [SerializeField] private float positionSmoothDamp = 0.0f;
        private Transform target;
        private Transform GetTarget(Transform target)
        {
            this.target = target;
            return null;
        }

        private Vector3 _cameraVelocity;

        private void FixedUpdate()
        {
           if(target!=null) SetPosition(target.position);
        }

        private void SetPosition(Vector3 position)
        {
            rig.position = Vector3.SmoothDamp(rig.position, new Vector3(0,position.y,position.z), ref _cameraVelocity, positionSmoothDamp);
        }
        public Delegate[] GetSubscribers()
        {
            return new Delegate[]
            {
                (GeneralEvents.GetTarget) GetTarget
            };
        }
    }
}

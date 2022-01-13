using System;
using Base;
using CorePlugin.Extensions;

namespace Input.Scripts
{
    public abstract class CrossInput :ICrossInput
    {
        private protected PlayerEvents.Move Move;
        private protected bool paused;


        public virtual void InvokeEvents()
        {
        }
        
        public virtual void Subscribe(params Delegate[] subscribers)
        {
            EventExtensions.Subscribe(ref Move, subscribers);
        }
        public virtual void Unsubscribe(params Delegate[] unsubscribers)
        {
            EventExtensions.Unsubscribe(ref Move, unsubscribers);
        }
        public abstract void Execute();
        
        private void UpdateMSG(bool pause)
        {
            paused = pause;
        }

        public virtual Delegate[] GetSubscribers()
        {
            return new Delegate[]
            {
                (GeneralEvents.OnPauseStateChanged) UpdateMSG
            };
        }
    }
}

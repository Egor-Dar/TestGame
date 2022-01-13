using System;
using Base;
using CorePlugin.Cross.Events.Interface;
using CorePlugin.Extensions;
using UnityEngine;

namespace ScreenSystem.Scripts
{
    public class PauseController : MonoBehaviour, IEventHandler, IEventSubscriber
    {
        private event ScreenStateDelegates.Play Play;
        private event ScreenStateDelegates.Pause Pause;

        private event GeneralEvents.OnPauseStateChanged OnPauseStateChanged;
        
        private bool paused = false;
        private bool markedForDie = false;
        public bool IsPaused() { return paused; }
        
        private void Paused()
        {
            if(markedForDie) return;
            if (paused)
            {
                paused = false;
                Play?.Invoke();
            }
            else
            {
                paused = true;
                Pause?.Invoke();
            }
            OnPauseStateChanged?.Invoke(paused);
        }
        private void EventMarkedDie(bool state)
        {
            markedForDie = state;
        }

        public void InvokeEvents()
        {

        }
        
        public void Subscribe(params Delegate[] subscribers)
        {
            EventExtensions.Subscribe(ref Play, subscribers);
            EventExtensions.Subscribe(ref Pause, subscribers);
            EventExtensions.Subscribe(ref OnPauseStateChanged, subscribers);
        }
        public void Unsubscribe(params Delegate[] unsubscribers)
        {
            EventExtensions.Unsubscribe(ref Play, unsubscribers);
            EventExtensions.Unsubscribe(ref Pause, unsubscribers);
            EventExtensions.Unsubscribe(ref OnPauseStateChanged, unsubscribers);
        }
        
        public Delegate[] GetSubscribers()
        {
            return new Delegate[]
            {
                (ScreenStateDelegates.Paused) Paused,
                (GeneralEvents.MarkedForDie) EventMarkedDie
            };
        }
    }
}

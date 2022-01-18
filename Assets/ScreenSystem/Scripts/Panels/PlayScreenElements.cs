using System;
using Base;
using CorePlugin.Cross.Events.Interface;
using CorePlugin.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenSystem.Scripts.Panels
{
    public sealed class PlayScreenElements : MonoBehaviour, IEventHandler, IEventSubscriber
    {
        [SerializeField] private Button pause;
        [SerializeField] private Joystick.Scripts.Joystick joystick;
        [SerializeField] private Text scoreText;
        private event GeneralEvents.GetJoystick getJoystick;
        private event ScreenStateDelegates.Paused PauseEvent;
        
        private void Start()
        {
            pause.onClick.AddListener(OnPauseEvent);
            InvokeEvents();
        }
        
        public void InvokeEvents()
        {
            getJoystick?.Invoke(joystick);
        }
        private void SetScore(int score)
        {
            scoreText.text = score.ToString();
        }
        public void Subscribe(params Delegate[] subscribers)
        {
            EventExtensions.Subscribe(ref PauseEvent, subscribers);
            EventExtensions.Subscribe(ref getJoystick, subscribers);
            
        }
        public void Unsubscribe(params Delegate[] unsubscribers)
        {
            EventExtensions.Unsubscribe(ref PauseEvent, unsubscribers);
            EventExtensions.Unsubscribe(ref getJoystick, unsubscribers);
        }
        private void OnPauseEvent()
        {
            PauseEvent?.Invoke();
        }


        public Delegate[] GetSubscribers()
        {
            return new Delegate[]
            {
                (PlayerEvents.OnPlayerHealthReceived) SetScore
            };
        }
    }
}

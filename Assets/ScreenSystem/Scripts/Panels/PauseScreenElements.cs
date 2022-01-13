using System;
using Base;
using CorePlugin.Cross.Events.Interface;
using CorePlugin.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenSystem.Scripts.Panels
{
    public sealed class PauseScreenElements : MonoBehaviour, IEventHandler
    {
        [SerializeField] private Button play;
        [SerializeField] private Button goToHome;
        [SerializeField] private Button restart;
        private event ScreenStateDelegates.Paused PlayEvent;
        private event LoadScenes.GoToMainMenu GoToHomeEvent;
        private event LoadScenes.Restart RestartEvent;
        private void OnPlayEvent()
        {
            PlayEvent?.Invoke();
        }
        private void OnGoToHomeEvent()
        {
            GoToHomeEvent?.Invoke();
        }
        private void OnRestartEvent()
        {
            RestartEvent?.Invoke();
        }
        private void Start()
        {
            play.onClick.AddListener(OnPlayEvent);
            goToHome.onClick.AddListener(OnGoToHomeEvent);
            restart.onClick.AddListener(OnRestartEvent);
        }

        public void InvokeEvents() { }
        public void Subscribe(params Delegate[] subscribers)
        {
            EventExtensions.Subscribe(ref PlayEvent, subscribers);
            EventExtensions.Subscribe(ref GoToHomeEvent, subscribers);
            EventExtensions.Subscribe(ref RestartEvent, subscribers);
        }
        public void Unsubscribe(params Delegate[] unsubscribers)
        {
            EventExtensions.Unsubscribe(ref PlayEvent, unsubscribers);
            EventExtensions.Unsubscribe(ref GoToHomeEvent, unsubscribers);
            EventExtensions.Unsubscribe(ref RestartEvent, unsubscribers);
        }
    }
}

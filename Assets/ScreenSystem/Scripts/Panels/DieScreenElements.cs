using System;
using Base;
using CorePlugin.Cross.Events.Interface;
using CorePlugin.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenSystem.Scripts.Panels
{
    public sealed class DieScreenElements : MonoBehaviour, IEventHandler, IEventSubscriber
    {
        [SerializeField] private Button goToHome;
        [SerializeField] private Button restart;
        [SerializeField] private Text scoreText;
        private event LoadScenes.Restart RestartEvent;
        private event LoadScenes.GoToMainMenu GoToHomeEvent;
        private void OnRestartEvent()
        {
            RestartEvent?.Invoke();
        }
        private void OnGoToHomeEvent()
        {
            GoToHomeEvent?.Invoke();
        }
        private void Start()
        {
            restart.onClick.AddListener(OnRestartEvent);
            goToHome.onClick.AddListener(OnGoToHomeEvent);
        }
        private int SetScore(int score)
        {
            scoreText.text = score.ToString();
            return 0;
        }
        public void InvokeEvents() { }
        public void Subscribe(params Delegate[] subscribers)
        {
            EventExtensions.Subscribe(ref RestartEvent, subscribers);
            EventExtensions.Subscribe(ref GoToHomeEvent, subscribers);
        }
        public void Unsubscribe(params Delegate[] unsubscribers)
        {
            EventExtensions.Unsubscribe(ref RestartEvent, unsubscribers);
            EventExtensions.Unsubscribe(ref GoToHomeEvent, unsubscribers);
        }
        public Delegate[] GetSubscribers()
        {
            return new Delegate[]
            {
                (PlayerEvents.GetScore) SetScore
            };
        }
    }
}

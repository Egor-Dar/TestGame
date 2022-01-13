using System;
using Base;
using CorePlugin.Cross.Events.Interface;
using CorePlugin.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenSystem.Scripts.Panels
{
    public sealed class MainScreenElements : MonoBehaviour, IEventHandler
    {
        [SerializeField] private Button play;
        private event LoadScenes.Restart PlayEvent;

        private void OnPlayEvent()
        {
            PlayEvent?.Invoke();
        }
        private void Start()
        {
            play.onClick.AddListener(OnPlayEvent);
        }

        public void InvokeEvents()
        {

        }
        public void Subscribe(params Delegate[] subscribers)
        {
            EventExtensions.Subscribe(ref PlayEvent, subscribers);
        }
        public void Unsubscribe(params Delegate[] unsubscribers)
        {
            EventExtensions.Subscribe(ref PlayEvent, unsubscribers);
        }
    }
}

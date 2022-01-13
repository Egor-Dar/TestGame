using System;
using Base;
using CorePlugin.Core;
using CorePlugin.Cross.Events.Interface;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class SceneController : BaseCore, IEventSubscriber
    {
        private void GoToHome()
        {
            SceneManager.LoadScene("Scenes/Menu");
        }
        private void Play()
        {
            SceneManager.LoadScene("Scenes/Play");
        }
        public Delegate[] GetSubscribers()
        {
            return new Delegate[]
            {
                (LoadScenes.Restart) Play,
                (LoadScenes.GoToMainMenu) GoToHome
            };
        }
    }
}

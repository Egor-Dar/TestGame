using System;
using Base;
using CorePlugin.Attributes.EditorAddons;
using CorePlugin.Cross.Events.Interface;
using CorePlugin.Logger;
using UnityEngine;

namespace ScreenSystem.Scripts
{
    [CoreManagerElement]
    public class ScreenController : MonoBehaviour, IEventSubscriber
    {
        [CoreManagerElementsField(FieldType.PlayMode)] private ScreenStateObject _pauseScreen;
        [CoreManagerElementsField(FieldType.PlayMode)] private ScreenStateObject _dieScreen;
        [CoreManagerElementsField(FieldType.PlayMode)] private ScreenStateObject _playScreen;

        private readonly ScreenState _screenState = new ScreenState();
        private bool markedForDie;

        public void AddDieScreen(ScreenStateObject stateObject)
        {
            SetStateScreenObject(ref _dieScreen, stateObject);
        }

        public void AddPauseScreen(ScreenStateObject stateObject)
        {
            SetStateScreenObject(ref _pauseScreen, stateObject);
        }

        public void AddPlayScreen(ScreenStateObject stateObject)
        {
            SetStateScreenObject(ref _playScreen, stateObject);
        }

        private void SetStateScreenObject(ref ScreenStateObject stateObject, ScreenStateObject newStateObject)
        {
            if (stateObject != null)
            {
                Destroy(stateObject);
            }

            stateObject = newStateObject;
            DebugLogger.Log($"State screen {stateObject.name}", stateObject);
        }

        public void Initialize()
        {
            Play();
        }

        private void Play()
        {
            _screenState.SetScreen(_playScreen);
        }

        private void Die()
        {
            _screenState.SetScreen(_dieScreen);
        }

        private void Pause()
        {
            _screenState.SetScreen(_pauseScreen);
        }


        private void Settings()
        {
            Debug.Log("Settings Panel");
        }

        public Delegate[] GetSubscribers()
        {
            return new Delegate[]
            {
                (ScreenStateDelegates.Play) Play,
                (ScreenStateDelegates.Die) Die,
                (ScreenStateDelegates.Pause) Pause
            };
        }

    }
}

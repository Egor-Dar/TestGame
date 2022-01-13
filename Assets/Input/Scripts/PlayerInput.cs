using CorePlugin.Core;
using UnityEngine;

namespace Input.Scripts
{
    public class PlayerInput : MonoBehaviour
    {
        private ICrossInput _currentInput;

        private void Awake()
        {
            _currentInput = new AndroidInput();
          /*  switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    _currentInput = new AndroidInput();
                    break;
                default:
                    _currentInput = new PCInput();
                    break;
            }*/
        }

        private void Start()
        {
            EventInitializer.Subscribe(_currentInput);
            EventInitializer.AddHandler(_currentInput, true, true);
            _currentInput.InvokeEvents();
        }

        private void OnDestroy()
        {
            EventInitializer.RemoveHandler(_currentInput);
            EventInitializer.Unsubscribe(_currentInput);
        }

        private void Update()
        {
            _currentInput?.Execute();
        }
    }
}

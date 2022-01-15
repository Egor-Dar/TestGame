using CorePlugin.Attributes.EditorAddons;
using CorePlugin.Attributes.Validation;
using CorePlugin.Core.Interface;
using ScreenSystem.Scripts;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(Canvas))]
    public class UIContainsManager : MonoBehaviour, ICore
    {
        [SerializeField] private Canvas uiCanvas;
        [CoreManagerElementsField(FieldType.EditorMode)] [PrefabRequired] [SerializeField]
        private ScreenController screenController;
        [CoreManagerElementsField(FieldType.EditorMode)] [PrefabRequired] [SerializeField]
        private ScreenStateObject playScreenInAndroid;
        [CoreManagerElementsField(FieldType.EditorMode)] [PrefabRequired] [SerializeField]
        private ScreenStateObject playScreenInPC;
        [CoreManagerElementsField(FieldType.EditorMode)] [PrefabRequired] [SerializeField]
        private ScreenStateObject pauseScreen;
        [CoreManagerElementsField(FieldType.EditorMode)] [PrefabRequired] [SerializeField]
        private ScreenStateObject dieScreen;

        [CoreManagerElementsField(FieldType.PlayMode)] private ScreenController _screenController;

        private void Awake()
        {
            uiCanvas ??= GetComponent<Canvas>();
        }

        public void InitializeElements()
        {
            _screenController = Instantiate(screenController, uiCanvas.transform);
          _screenController.AddPlayScreen(Instantiate(playScreenInAndroid, uiCanvas.transform));

            _screenController.AddPauseScreen(Instantiate(pauseScreen, uiCanvas.transform));
            _screenController.AddDieScreen(Instantiate(dieScreen, uiCanvas.transform));

            _screenController.Initialize();
        }
    }
}

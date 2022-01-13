using CorePlugin.Attributes.EditorAddons;
using CorePlugin.Extensions;
using UnityEngine;

namespace ScreenSystem.Scripts
{
    [CoreManagerElement]
    [RequireComponent(typeof(CanvasGroup))]
    public class ScreenStateObject : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup ??= GetComponent<CanvasGroup>();
        }
        
        private void Reset()
        {
            canvasGroup ??= GetComponent<CanvasGroup>();
        }

        public ScreenStateObject SetActive(bool state)
        {
            canvasGroup.ChangeGroupState(state);
            return this;
        }
    }
}

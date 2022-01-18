using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Joystick.Scripts
{
    public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler

    {
        [SerializeField] private Image joystick;
        [SerializeField] private RectTransform panel;

        public Vector2 inputVector;

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 pos;
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(joystick.rectTransform, eventData.position, eventData.pressEventCamera, out pos)) return;
            pos.x = (pos.x / joystick.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joystick.rectTransform.sizeDelta.x);

            inputVector = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);
            inputVector = (inputVector.magnitude > 1f) ? inputVector.normalized : inputVector;

        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            inputVector = Vector2.zero;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            joystick.rectTransform.anchoredPosition = (eventData.position - (Vector2)panel.position);

            OnDrag(eventData);
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

namespace ActionCode.UI
{
    public class MenuEventTrigger : MonoBehaviour,
        ISubmitHandler, ISelectHandler, IPointerEnterHandler, IPointerClickHandler
    {
        [Tooltip("The menu where this Event belongs.")]
        public BaseMenu menu;

        public void OnSubmit(BaseEventData eventData) => Submit();

        public void OnSelect(BaseEventData eventData) => Select();

        public void OnPointerEnter(PointerEventData eventData) => Select();

        public void OnPointerClick(PointerEventData eventData)
        {
            var isLeftClick = eventData.button == PointerEventData.InputButton.Left;
            if (isLeftClick) Submit();
        }

        protected virtual void Submit() => menu?.SubmitEvent(this);

        protected virtual void Select() => menu?.SelectEvent(this);
    }
}
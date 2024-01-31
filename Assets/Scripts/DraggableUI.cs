using UnityEngine;
using UnityEngine.EventSystems;


public class DraggableUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    protected RectTransform rectTransform;
    protected Canvas canvas;
    protected CanvasGroup canvasGroup;

    protected virtual void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponentInParent<CanvasGroup>();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out Vector3 worldPoint))
        {
            rectTransform.position = worldPoint;
        }
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }
}

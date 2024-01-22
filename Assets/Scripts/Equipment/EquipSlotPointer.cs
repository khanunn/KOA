using UnityEngine;
using UnityEngine.EventSystems;

public class EquipSlotPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        EventManager.instance.equipmentEvents.SlotIPointerEnter();

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventManager.instance.equipmentEvents.SlotIPointerExit();
    }
}
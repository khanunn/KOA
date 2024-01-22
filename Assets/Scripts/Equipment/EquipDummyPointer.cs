using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipDummyPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log("Enter Model");
        EventManager.instance.equipmentEvents.DummyIPointerEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Exit Model");
        EventManager.instance.equipmentEvents.DummyIPointerExit();
    }
}

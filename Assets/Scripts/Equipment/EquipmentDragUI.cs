using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentDragUI : DraggableUI
{
    [SerializeField] private EquipmentController equipmentController;
    protected override void Start()
    {
        base.Start();
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }
    public override void OnDrag(PointerEventData eventData)
    {
        if (equipmentController.IsClickDummy) { return; }
        base.OnDrag(eventData);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }
}
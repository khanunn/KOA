using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class InventoryUI : DraggableUI
{
    public CurrencyManager currencyManager;
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
        base.OnDrag(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }

    private void Update()
    {
        currencyManager.UpdateGoldText();
    }
}

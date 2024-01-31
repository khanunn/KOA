using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class EquipmentController : MonoBehaviour
{

    private Vector2 lastMousePosition;
    public float rotationSpeed = 0.5f;
    public GameObject rotationDummy;

    private bool isDummyPointer;
    private bool isSlotPointer;
    public bool IsClickDummy { get; private set; }
    private bool isClicking = false;
    private float clickTime = 0f;
    private float doubleClickTimeThreshold = 0.3f; // ปรับตามความต้องการ

    private CustomAction input;

    public GameObject equipment;
    private bool equipmentSwitch;

    void OnEnable()
    {
        input.Enable();
        EventManager.instance.equipmentEvents.onDummyIPointerEnter += DummyIPointerEnter;
        EventManager.instance.equipmentEvents.onDummyIPointerExit += DummyIPointerExit;
        EventManager.instance.equipmentEvents.onSlotIPointerEnter += SlotIPointerEnter;
        EventManager.instance.equipmentEvents.onSlotIPointerExit += SlotIPointerExit;
    }

    void OnDisable()
    {
        input.Disable();
        EventManager.instance.equipmentEvents.onDummyIPointerEnter -= DummyIPointerEnter;
        EventManager.instance.equipmentEvents.onDummyIPointerExit -= DummyIPointerExit;
        EventManager.instance.equipmentEvents.onSlotIPointerEnter -= SlotIPointerEnter;
        EventManager.instance.equipmentEvents.onSlotIPointerExit -= SlotIPointerExit;
    }
    void AssignInput()
    {
        input.Equipment.Preview.started += ctx => StartToRotate();
        input.Equipment.Preview.canceled += ctx => CancleToRotate();
        input.Equipment.Preview.performed += ctx => DoubleClickUnEquip();
        input.Equipment.Window.performed += ctx => SwitchEquipment();
    }

    private void Awake()
    {
        input = new CustomAction();
        AssignInput();

    }
    private void Start()
    {
        //equipBtn = GameObject.Find("Equipment").GetComponent<DoubleClickHandler>();
    }
    private void Update()
    {
        DummyPreview();
        CooldownDoubleClick();
    }

    private void DummyPreview()
    {
        if (IsClickDummy)
        {
            // Check for left mouse button click
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                // Store the initial mouse position
                Vector3 lastMousePosition = Mouse.current.position.ReadValue();
            }

            // Check for left mouse button drag
            if (Mouse.current.leftButton.isPressed)
            {
                // Calculate the delta movement of the mouse
                Vector3 currentMousePosition = Mouse.current.position.ReadValue();
                Vector3 mouseDelta = currentMousePosition - (Vector3)lastMousePosition;

                // Rotate the GameObject based on the mouse movement
                float rotationX = mouseDelta.y * rotationSpeed;
                float rotationY = -mouseDelta.x * rotationSpeed;

                rotationDummy.transform.Rotate(Vector3.up, rotationY, Space.World);
                lastMousePosition = currentMousePosition;
            }
        }
    }


    private void StartToRotate()
    {
        //Debug.Log("started rotate");
        if (isDummyPointer)
        {
            IsClickDummy = true;
        }
    }
    private void CancleToRotate()
    {
        //Debug.Log("cancled rotate");
        if (!isDummyPointer)
        {
            if (IsClickDummy)
            {
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(rotationDummy.transform.rotation.x, rotationDummy.transform.rotation.x, -180));
                rotationDummy.transform.rotation = lookRotation;
            }

            IsClickDummy = false;
        }
    }

    private void DoubleClickUnEquip()
    {
        if (isClicking)
        {
            DoubleClickAction();
            isClicking = false;
            clickTime = 0f;
        }
        else
        {
            isClicking = true;
        }
    }
    private void CooldownDoubleClick()
    {
        if (isClicking)
        {
            clickTime += Time.deltaTime;

            if (clickTime > doubleClickTimeThreshold)
            {
                isClicking = false;
                clickTime = 0f;
            }
        }
    }

    private void DoubleClickAction()
    {
        if (isSlotPointer)
        {
            Debug.Log("Double Click");
        }
    }

    private void DummyIPointerEnter()
    {
        // Debug.Log("Enter Dummy on Controller");
        isDummyPointer = true;
    }
    private void DummyIPointerExit()
    {
        //Debug.Log("Exit Dummy on Controller");
        isDummyPointer = false;
    }
    private void SlotIPointerEnter()
    {
        //Debug.Log("Enter Slot on Controller");
        isSlotPointer = true;
    }
    private void SlotIPointerExit()
    {
        //Debug.Log("Enter Slot on Controller");
        isSlotPointer = false;
    }
    public void SwitchEquipment()
    {
        if (!equipmentSwitch)
        {
            equipment.SetActive(true);
            equipmentSwitch = true;
        }
        else
        {
            equipment.SetActive(false);
            equipmentSwitch = false;
        }
    }

    public void ExitButton()
    {
        equipment.SetActive(false);
        equipmentSwitch = false;
    }
}



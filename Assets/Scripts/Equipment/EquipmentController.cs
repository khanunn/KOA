using UnityEngine;
using UnityEngine.InputSystem;

public class EquipmentController : MonoBehaviour 
{

    private Vector2 lastMousePosition;
    public float rotationSpeed = 0.5f;
    public GameObject rotationDummy;

    CustomAction input;
    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    private void Awake() {
        input = new CustomAction();
        AssignInput();
        
    }
    
    void AssignInput()
    {
        input.UI.Equipment.started += ctx => ClickToRotate();
    }

    private void ClickToRotate()
    {
        Debug.Log("started rotate");
    }

    /* private void OnEnable()
    {
        // Subscribe to mouse events
        InputSystem.onMouse.AddListeners(OnMouseClick, OnMouseDrag);
    }

    private void OnDisable()
    {
        // Unsubscribe from mouse events
        InputSystem.onMouse.RemoveListeners(OnMouseClick, OnMouseDrag);
    }

    private void OnMouseClick(MouseControl control, MousePhase phase)
    {
        if (phase == MousePhase.Down && control.button == MouseButton.Left)
        {
            // Store the initial mouse position
            lastMousePosition = control.position.ReadValue();
        }
    }

    private void OnMouseDrag(MouseControl control, MousePhase phase)
    {
        if (phase == MousePhase.Move && control.button == MouseButton.Left)
        {
            // Calculate the delta movement of the mouse
            Vector2 mouseDelta = control.position.ReadValue() - lastMousePosition;

            // Rotate the GameObject based on the mouse movement
            float rotationX = mouseDelta.y * rotationSpeed;
            float rotationY = -mouseDelta.x * rotationSpeed;

            rotationDummy.transform.Rotate(Vector3.up, rotationY, Space.World);
            //transform.Rotate(Vector3.right, rotationX, Space.World);

            // Store the current mouse position for the next frame
            lastMousePosition = control.position.ReadValue();
        }
    } */
}


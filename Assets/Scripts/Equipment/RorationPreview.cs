using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Windows;


public class RorationPreview : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Slider rotationSlider;
    [SerializeField] GameObject rotationDummy;

    [SerializeField] private float rotationSpeed = 2f;
    private Vector3 lastMousePosition;

    public TMP_Text feedbackText;

    private Rect rotationArea = new Rect(0.225f, 0.225f, 0.325f, 0.325f);

    void Update()
    {
        // Check for left mouse button click
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            // Store the initial mouse position
            lastMousePosition = UnityEngine.Input.mousePosition;
        }

        // Check for left mouse button drag
        if (UnityEngine.Input.GetMouseButton(0))
        {
            // Calculate the delta movement of the mouse
            Vector3 mouseDelta = UnityEngine.Input.mousePosition - lastMousePosition;

            // Rotate the GameObject based on the mouse movement
            float rotationX = mouseDelta.y * rotationSpeed;
            float rotationY = -mouseDelta.x * rotationSpeed;

            rotationDummy.transform.Rotate(Vector3.up, rotationY, Space.World);
            //transform.Rotate(Vector3.right, rotationX, Space.World);

            // Store the current mouse position for the next frame
            lastMousePosition = UnityEngine.Input.mousePosition;
        }
    }

    public void RightAngle()
    {
        //rotationDummy.transform.Rotate(0, transform.rotation.y + 10, 0);
        print("check");
    }
    public void LeftAngle()
    {
        rotationDummy.transform.Rotate(0, transform.rotation.y - 10, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // เมื่อมีการสัมผัส Pointer กับ Button
        feedbackText.text = "Mouse over the button";
        // เพิ่มโค้ดที่ต้องการทำเมื่อ PointerEnter
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // เมื่อ Pointer ออกจาก Button
        feedbackText.text = "Mouse exit the button";
        // เพิ่มโค้ดที่ต้องการทำเมื่อ PointerExit
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        feedbackText.text = "Mouse Down the button";
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        feedbackText.text = "Mouse Up the button";
    }
}


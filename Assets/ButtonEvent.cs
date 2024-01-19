using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public TMP_Text feedback;
    public void OnPointerDown(PointerEventData eventData)
    {
        feedback.text = "Mouse Point Down";
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        feedback.text = "Mouse Point Up";
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

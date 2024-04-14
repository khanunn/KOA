using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TestA : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {       
        if (other.tag == "Interactable")
        {
            Debug.LogWarning("fuck you");

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Interactable")
        {
            Debug.LogWarning("fuck you2");

        }
    }

}

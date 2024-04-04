using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

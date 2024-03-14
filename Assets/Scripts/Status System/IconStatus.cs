using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconStatus : MonoBehaviour
{
    [SerializeField] Text DurationText;
    public float DurationTime;
    public bool StartCD = false;
    // Start is called before the first frame update
  
    // Update is called once per frame
    void Update()
    {
        if(StartCD)
        {
            DurationTime -= Time.deltaTime;
            DurationText.text = DurationTime.ToString();
        }

        if (DurationTime <= 0) Destroy(this.gameObject);
    }
}

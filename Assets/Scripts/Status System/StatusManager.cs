using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    Dictionary<StatusInfoSO,StatusBehavious> CurrentStatus = new Dictionary<StatusInfoSO, StatusBehavious>();

    List<StatusInfoSO> CheckDuration = new List<StatusInfoSO>();

    [SerializeField] GameObject IconStatusPanal;
    [SerializeField] GameObject DisplayIcon;
    public async void AddStatus(StatusInfoSO statusInfo)
        
    {
        if (CurrentStatus.ContainsKey(statusInfo))
        {
            StackStatus(statusInfo);
            return;
        }

        StatusBehavious behavious = new StatusBehavious();

        behavious.target = this.gameObject;

        behavious.duration = statusInfo.Duration;        
        behavious.statusIntensity = statusInfo.Intensity;
        behavious.statusID = statusInfo.StatusID;       

        CurrentStatus.Add(statusInfo, behavious);
     
        if(this.GetComponent<Interactable>().interactionType == InteractableType.PLAYER)
        {
            DisplayIcon.transform.GetChild(0).GetComponent<Image>().sprite = statusInfo.Icon;
            DisplayIcon.transform.GetChild(1).GetComponent<Text>().text = statusInfo.Duration.ToString();
            DisplayIcon.GetComponent<IconStatus>().DurationTime = statusInfo.Duration;
            DisplayIcon.GetComponent<IconStatus>().StartCD = true;
            // Instantiate the image object
            Instantiate(DisplayIcon, IconStatusPanal.transform);
        }     

        await behavious.ActiveSkill();             

        //remove skill form dict  

        RemoveStatus(statusInfo);

        //Debug.Log("Check CurrentStatus Count " + CurrentStatus.Count);
    }
    public void RemoveStatus(StatusInfoSO statusInfo)
    {
        CurrentStatus.Remove(statusInfo);
    }

    public void StackStatus(StatusInfoSO statusInfo)
    {
        //Debug.Log("Test On Stack");
        CurrentStatus[statusInfo].duration += statusInfo.Duration;
    }
    // Start is called before the first frame update
    void Start()
    {
        //using for test boss skill 
        //EventManager.instance.healthEvents.HealthChange(5000);
    }

    // Update is called once per frame
    void Update()
    {
        //using to handle stack
        foreach (var status in CurrentStatus)
        {
            status.Value.duration -= Time.deltaTime;

            if (status.Value.duration <= 0) CheckDuration.Add(status.Key);
        }

        foreach (var status in CheckDuration)
        {
            RemoveStatus(status);
        }

        CheckDuration.Clear();
    }


}

using UnityEngine;

public class StatController : MonoBehaviour
{
    [SerializeField] private GameObject statCanvas;
    private bool statSwitch;

    private void OnEnable()
    {
        EventManager.instance.inputEvents.onStatPressed += SwitchCharacterInfo;
    }
    private void OnDisable()
    {
        EventManager.instance.inputEvents.onStatPressed -= SwitchCharacterInfo;
    }

    private void SwitchCharacterInfo()
    {
        if (!statSwitch)
        {
            statCanvas.SetActive(true);
            statSwitch = true;
            EventManager.instance.statEvents.ShowStat();
        }
        else
        {
            statCanvas.SetActive(false);
            statSwitch = false;
        }
    }
}
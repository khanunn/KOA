using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    private CustomAction input;
    private bool settingSwitch;

    [Header("Audio Settings")]
    [SerializeField] AudioMixer AudioMixer;

    [Header("Resolution Settings")]
    [SerializeField] Resolution[] Resolution;
    [SerializeField] TMPro.TMP_Dropdown ResolutionDropdown;
    //[SerializeField] GameObject fpsText;
    [SerializeField] bool ShowfpsText = false;
    public TMP_Text fpsText;
    float deltaTime;

    [Header("Settings")]
    [SerializeField] GameObject SettingPanal;

    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }
    private void AssignInput()
    {
        input.Setting.Window.performed += ctx => SwitchSetting();
    }

    public void SwitchSetting()
    {
        if (!settingSwitch)
        {
            SettingPanal.SetActive(true);
            settingSwitch = true;
        }
        else
        {
            SettingPanal.SetActive(false);
            settingSwitch = false;
        }
    }

    private void Awake()
    {
        input = new CustomAction();
        AssignInput();
        fpsText.enabled = false;
    }

    private void Start()
    {
        Resolution = Screen.resolutions; //Put all aviable resolutuion in this variable
        ResolutionDropdown.ClearOptions(); //Clear Previous one

        List<string> Alloption = new List<string>();

        int currentResoultionIndex = 0;

        for (int i = 0; i < Resolution.Length; i++)
        {
            string options = Resolution[i].width + " x " + Resolution[i].height;
            Alloption.Add(options);

            if (Resolution[i].width == Screen.currentResolution.width && Resolution[i].height == Screen.currentResolution.height)
            {
                currentResoultionIndex = i;
            }
        }

        ResolutionDropdown.AddOptions(Alloption);
        ResolutionDropdown.value = currentResoultionIndex;
        ResolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = Resolution[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetMasterVolume(float volume)
    {
        AudioMixer.SetFloat("Master", volume);
    }

    public void SetBGVolume(float volume)
    {
        AudioMixer.SetFloat("Background", volume);
    }

    public void SetSFXVolume(float volume)
    {
        AudioMixer.SetFloat("SFX", volume);
    }

    public void SetQuality(int level)
    {
        QualitySettings.SetQualityLevel(level);
    }

    public void ToggleFPS()
    {
        if (!ShowfpsText)
        {
            ShowfpsText = true;
            //fpsText.SetActive(true);
            fpsText.enabled = true;

        }
        else if (ShowfpsText)
        {
            ShowfpsText = false;
            //fpsText.SetActive(false);
            fpsText.enabled = false;
        }
    }
    void Update()
    {
        if (ShowfpsText)
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fpsText.GetComponent<TextMeshProUGUI>().text = "FPS: " + Mathf.Ceil(fps).ToString();
        }
    }

}

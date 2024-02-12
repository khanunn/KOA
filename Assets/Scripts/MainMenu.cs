using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Preview Model Settings")]
    [SerializeField] GameObject PreviewModel;
    [SerializeField] Material [] SkinMaterial;

    [Header("Scene Settings")]
    [SerializeField] Animator CameraMovement;
    public bool NextScene = false;
    public void GoToNextScene()
    {
        CameraMovement.Play("Transition");               
    }

    public void GoToSelection()
    {
        CameraMovement.Play("A to B");
    }
    public void GoToCharacterSelection()
    {
        CameraMovement.Play("Character Selection");
    }

    public void ChangeColorSKin(int Index)
    {
        PreviewModel.GetComponent<SkinnedMeshRenderer>().material = SkinMaterial[Index];
    }
    
    private void Update()
    {
        if(NextScene) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

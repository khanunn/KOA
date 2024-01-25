using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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

    private void Update()
    {
        if(NextScene) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportExit : MonoBehaviour
{
    [SerializeField] private TeleportName teleportLoad;
    [SerializeField] private TeleportName teleportBack;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActiveScene();
            Debug.Log("Active Scene");
        }
    }

    private void ActiveScene()
    {
        PlayerPrefs.SetString("LastSceneBack", teleportBack.ToString());
        Debug.Log("SetSceneBack : " + teleportBack);
        SceneManager.LoadScene(teleportLoad.ToString(), LoadSceneMode.Single);

    }
}
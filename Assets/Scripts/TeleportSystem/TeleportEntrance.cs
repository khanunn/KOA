using UnityEngine;

public class TeleportEntrance : MonoBehaviour
{
    //public string lastSceneBack;
    [SerializeField] private TeleportName teleportName;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("LastSceneBack") == teleportName.ToString())
        {
            Debug.Log("GetSceneBack : " + teleportName);
            TeleportManager.instance.player.transform.position = transform.position;
            TeleportManager.instance.player.transform.eulerAngles = transform.eulerAngles; ;
        }
    }
}
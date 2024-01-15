using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager instance { get; private set; }
    public GameObject player;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            Debug.LogError("Found TeleportManager > 1 in scene");
        }
        instance = this;
    }
}
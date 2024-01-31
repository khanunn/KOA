using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class ShopPoint : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    private Interactable target;
    public bool playerIsNear { get; private set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
            //Debug.Log("PlayerIsNear: " + playerIsNear + " from QuestPoint");

            target = FindFirstObjectByType<PlayerController>().GetComponent<Interactable>();
            //Debug.Log("Target : " + target + "Type " + target.interactionType);
            target.myPlayer.InteractableChange(this.gameObject);
            shopPanel.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
            //Debug.Log("PlayerIsNear: " + playerIsNear + " from QuestPoint");
            shopPanel.SetActive(false);
        }
    }
}
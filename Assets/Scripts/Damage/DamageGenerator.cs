using TMPro;
using UnityEngine;

public class DamageGenerator : MonoBehaviour
{
    public GameObject prefab;


    private void OnEnable()
    {
        EventManager.instance.playerEvents.onAttackPopUp += CreatePopUp;
    }
    private void OnDisable()
    {
        EventManager.instance.playerEvents.onAttackPopUp -= CreatePopUp;
    }

    private void CreatePopUp(Vector3 position, string text, Color color)
    {
        GameObject popup = Instantiate(prefab, position, Quaternion.identity);
        TMP_Text temp = popup.transform.GetChild(0).GetComponent<TMP_Text>();
        temp.text = text;
        temp.faceColor = color;

        //Destroy Timer
        Destroy(popup, 1f);
    }
}

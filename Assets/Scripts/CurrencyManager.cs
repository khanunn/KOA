using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [Header("Gold")]
    public int gold;
    public TMP_Text textGold;

    private void OnEnable()
    {
        EventManager.instance.currencyEvents.onGoldGained += GoldGained;
    }
    private void OnDisable()
    {
        EventManager.instance.currencyEvents.onGoldGained -= GoldGained;
    }
    private void Start()
    {
        textGold.text = "" + gold;
    }
    private void GoldGained(int gold)
    {
        this.gold += gold;
        textGold.text = "" + this.gold.ToString();
    }
    private void Update()
    {
        textGold.text = "" + this.gold.ToString();
    }
}
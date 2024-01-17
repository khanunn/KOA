using System.Collections;
using TMPro;
using UnityEngine;

public class StatReader : MonoBehaviour
{
    public StatManager statManager;
    public StatKey statKey;
    private Stat stat;

    public TMP_Text statText;
    public TMP_Text differenceText;

    private int currentValue;
    private int currentLevel;

    private void OnEnable()
    {
        EventManager.instance.statEvents.onShowStat += ResetUpdateText;
        EventManager.instance.playerEvents.onPlayerLevelChange += UpdateStatText;
    }
    private void OnDisable()
    {
        EventManager.instance.statEvents.onShowStat -= ResetUpdateText;
        EventManager.instance.playerEvents.onPlayerLevelChange -= UpdateStatText;
    }

    private void Start()
    {
        //Debug.Log("Stat Key: " + statKey);
        //statText = gameObject.GetComponent<TMP_Text>();
        stat = statManager.GetStat(statKey);
        //Debug.Log("Stat Key : " + stat.statKey);
        currentValue = stat.statValue;
        currentLevel = statManager.levelManager.level;
        statText.text = statKey.ToString() + " : " + stat.statValue;
    }

    private void UpdateStatText(int level)
    {
        if (currentLevel != level)
        {
            currentLevel = level;
            stat = statManager.GetStat(statKey);
            int difference = stat.statValue - currentValue;

            while (difference > 0)
            {
                difference--;
                currentValue++;
            }

            while (difference < 0)
            {
                difference++;
                currentValue--;
            }

            statText.text = $"{statKey}: {currentValue}";
        }
    }

    /* private void Update()
    {
        UpdateText();
    }
    public void UpdateText()
    {
        if (currentLevel != statManager.levelManager.level)
        {
            currentLevel = statManager.levelManager.level;
            Stat stat = statManager.GetStat(statKey);
            int difference = stat.statValue - currentValue;

            if (difference > 0)
            {
                differenceText.text = " + " + difference;
                differenceText.color = new Color(0, 255, 0);
                //Invoke(nameof(TickTextUp), 1.5f);
                StartCoroutine(TickTextUp(difference));
            }
            else if (difference < 0)
            {
                differenceText.text = " - " + difference;
                differenceText.color = new Color(255, 0, 0);
                StartCoroutine(TickTextDown(difference));
                //Invoke(nameof(TickTextDown), 1.5f);
            }
            else
            {
                differenceText.text = difference.ToString();
                differenceText.color = new Color(255, 0, 0);
                StartCoroutine(TickTextUp(difference));
            }
        }
    }
    public IEnumerator TickTextUp(int difference)
    {
        yield return new WaitForSeconds(1f);
        while (difference > 0)
        {
            difference--;
            currentValue++;
            differenceText.text = "+" + difference.ToString();
            statText.text = statKey.ToString() + ": " + currentValue;

            yield return new WaitForSeconds(0.1f);
        }

        differenceText.text = "";
    }

    public IEnumerator TickTextDown(int difference)
    {
        yield return new WaitForSeconds(1f);
        while (difference < 0)
        {
            difference++;
            currentValue--;
            differenceText.text = "-" + difference.ToString();
            statText.text = statKey.ToString() + ": " + currentValue;

            yield return new WaitForSeconds(0.1f);
        }

        differenceText.text = "";
    } */

    private void ResetUpdateText()
    {
        stat = statManager.GetStat(statKey);
        statText.text = statKey.ToString() + ": " + stat.statValue;
        differenceText.text = "";
    }
}
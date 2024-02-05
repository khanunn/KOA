using System;

public class QuestEvents
{
    public event Action<string> onStartQuest;
    public void StartQuest(string id)
    {
        if (onStartQuest != null)
        {
            onStartQuest(id);
        }
    }

    public event Action<string> onAdvanceQuest;
    public void AdvanceQuest(string id)
    {
        if (onAdvanceQuest != null)
        {
            onAdvanceQuest(id);
        }
    }

    public event Action<string> onFinishQuest;
    public void FinishQuest(string id)
    {
        if (onFinishQuest != null)
        {
            onFinishQuest(id);
        }
    }

    public event Action<Quest> onQuestStateChange;
    public void QuestStateChange(Quest quest)
    {
        if (onQuestStateChange != null)
        {
            //onQuestStateChange(quest);
            onQuestStateChange?.Invoke(quest);
        }
    }

    public event Action<string> onStartDialogue;
    public void StartDialogue(string id)
    {
        if (onStartDialogue != null)
        {
            onStartDialogue(id);
        }
    }
    public event Action<string> onFinishDialogue;
    public void FinishDialogue(string id)
    {
        if (onFinishDialogue != null)
        {
            onFinishDialogue(id);
        }
    }
}
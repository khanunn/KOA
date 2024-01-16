using System;

public class DialogueEvents
{
    public event Action<DialogueInfoSO> onDialogueStart;
    public void DialogueStart(DialogueInfoSO dialogue)
    {
        if (onDialogueStart != null)
        {
            onDialogueStart(dialogue);
        }
    }

    public event Action<DialogueInfoSO> onDialogueFinish;
    public void DialogueFinish(DialogueInfoSO dialogue)
    {
        if (onDialogueFinish != null)
        {
            onDialogueFinish(dialogue);
        }
    }
}
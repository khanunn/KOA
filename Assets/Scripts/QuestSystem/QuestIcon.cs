using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    [SerializeField] private GameObject notStartIcon;
    [SerializeField] private GameObject canStartIcon;
    [SerializeField] private GameObject inProgressIcon;
    [SerializeField] private GameObject canFinishIcon;

    public void SetState(QuestState newState, bool startPoint, bool finishPoint)
    {
        notStartIcon.SetActive(false);
        canStartIcon.SetActive(false);
        inProgressIcon.SetActive(false);
        canFinishIcon.SetActive(false);

        switch (newState)
        {
            case QuestState.NOT_REQUIREMENT:
                if (startPoint)
                {
                    notStartIcon.SetActive(true);
                }
                break;
            case QuestState.CAN_START:
                if (startPoint)
                {
                    canStartIcon.SetActive(true);
                }
                break;
            case QuestState.IN_PROGRESS:
                if (finishPoint)
                {
                    inProgressIcon.SetActive(true);
                }
                break;
            case QuestState.CAN_FINISH:
                if (finishPoint)
                {
                    canFinishIcon.SetActive(true);
                }
                break;
            case QuestState.FINISHED:

                break;
            default:
                Debug.LogWarning("ไม่มี Quest State ที่ Switch รู้จักสำหรับ Icon: " + newState);
                break;
        }
    }
}
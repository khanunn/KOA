using UnityEngine;
using static UnityEngine.CullingGroup;

public class StatController : MonoBehaviour
{
    [SerializeField] private GameObject statCanvas;
    private bool statSwitch;
    private StatManager statManager;

    /// <Stat>
    public Stat v_hp_max, v_mp_max, v_hp_recovery, v_mp_recovery, v_patk, v_matk, v_pdef, v_mdef, v_acc, v_evade, v_crit_change, v_crit_dam, v_pdam, v_mdam;
    /// </summary>
    private void Awake()
    {
        statManager = GetComponentInParent<StatManager>();
    }
    private void OnEnable()
    {
        EventManager.instance.inputEvents.onStatPressed += SwitchCharacterInfo;
        EventManager.instance.statEvents.onSendStatManager += StartStat;
    }
    private void OnDisable()
    {
        EventManager.instance.inputEvents.onStatPressed -= SwitchCharacterInfo;
        EventManager.instance.statEvents.onSendStatManager -= StartStat;
    }
    private void Start()
    {
        Debug.Log("statmanager" + statManager);
        v_hp_max = statManager.GetStat(StatKey.v_hp_max);
        v_mp_max = statManager.GetStat(StatKey.v_mp_max);

        v_hp_recovery = statManager.GetStat(StatKey.v_hp_recovery);
        v_mp_recovery = statManager.GetStat(StatKey.v_mp_recovery);

        v_patk = statManager.GetStat(StatKey.v_patk);
        v_matk = statManager.GetStat(StatKey.v_matk);

        //Added
        v_acc = statManager.GetStat(StatKey.v_acc);
        v_evade = statManager.GetStat(StatKey.v_evade);

        v_pdef = statManager.GetStat(StatKey.v_pdef);
        v_mdef = statManager.GetStat(StatKey.v_mdef);

        v_crit_change = statManager.GetStat(StatKey.v_crit_change);
        v_crit_dam = statManager.GetStat(StatKey.v_crit_dam);

        v_pdam = statManager.GetStat(StatKey.v_pdam);
        v_mdam = statManager.GetStat(StatKey.v_mdam);

        EventManager.instance.statEvents.SendStatController(this);
    }
    private void StartStat(StatManager myStat)
    {
        //statManager = myStat;
    }

    public void EditPhysicalDefend(int value)
    {
        statManager.ChangeStatBySkill(v_pdef, value);
    }

    public void EditPhysicalDamage(int value)
    {
        statManager.ChangeStatBySkill(v_patk, value);
    }

    private void SwitchCharacterInfo()
    {
        if (!statSwitch)
        {
            statCanvas.SetActive(true);
            statSwitch = true;
        }
        else
        {
            statCanvas.SetActive(false);
            statSwitch = false;
        }
    }
}
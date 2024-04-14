using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerEnemy : MonoBehaviour
{
    [Header("Behavious Setting")]
    [SerializeField] float ChargeDuration = 2f;
    [SerializeField] float ChargeSpeed = 2f;
    [SerializeField] StatusInfoSO SpeedBuff;
    StatusManager statusManager;
    Animator animator;
    public void ChargeAttack()
    {
        //for changing Charge Duration & ChargeSpeed
        SpeedBuff.ChangeIntensity(ChargeSpeed);
        SpeedBuff.SetDuration(ChargeDuration);

        statusManager.AddStatus(SpeedBuff);
        animator.SetBool("ChargeAttack", true);
    }

    public void FinishChargeAttack()
    {
        animator.SetBool("ChargeAttack", false);
    }
    // Start is called before the first frame update
    void Start()
    {
        statusManager = this.GetComponent<StatusManager>();
        animator = this.GetComponent<Animator>();
        if (!SpeedBuff) SpeedBuff = Resources.Load("/Buff/Speed Tier 1.asset") as StatusInfoSO;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

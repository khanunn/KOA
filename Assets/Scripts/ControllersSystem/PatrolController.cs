using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolController : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    CapsuleCollider capsuleCollider;
    bool patrolBusy = false;
    bool patrolDie = false;
    bool patrolMoveing = false;
    Interactable target;
    const string WALK = "Walk";
    const string IDLE = "Idle";
    const string ATTACK = "Attack";
    const string DEATH = "Death";
    //=====================================================//
    [Header("Moving")]
    [SerializeField] private float moveRadius;
    [SerializeField] private float ramdomDelayMin;
    [SerializeField] private float ramdomDelayMax;
    //=====================================================//
    [Header("Attacking")]
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDelay;
    [SerializeField] private float attackDistance;
    //====================================================//
    [Header("Damage")]
    [SerializeField] private int punchDamage;
    [SerializeField] private int meleeDamage;
    //====================================================//
    [Header("Infomation")]
    public MonsterInfoSO monsterInfoSO;
    //====================================================//

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        //MoveWithDelay();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RandomToMove();
        PlayAnimations();
        FollowTarget();
    }

    void RandomToMove()
    {
        if (target != null || patrolDie) return;
        if (!patrolMoveing)
        {
            float delayTime = Random.Range(ramdomDelayMin, ramdomDelayMax);
            Invoke(nameof(MoveWithDelay), delayTime);
            patrolMoveing = true;
            //Debug.Log("(RandomToMove)Moving : "+patrolMoveing);
            return;
        }
    }

    void FollowTarget()
    {
        if (target == null) return;
        //=============ระยะห่างเป้าหมายน้อยกว่าระยะโจมตี========================//
        if (Vector3.Distance(target.transform.position, transform.position) <= attackDistance)
        {
            if (target.interactionType == InteractableType.PLAYER)
            {
                ReachDistance();
            }
        }
        else
        {
            if (patrolDie) return;
            agent.SetDestination(target.transform.position);
        }
    }
    //=============="หันหน้าไปยังทิศทางของเป้าหมาย"=====================//
    void FaceToTarget()
    {
        Vector3 direction = (agent.destination - target.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(-direction.x, 0, -direction.z));
        transform.rotation = lookRotation;
    }
    //==============================เข้าระยะโจมตี============================//
    void ReachDistance()
    {
        agent.SetDestination(transform.position);
        FaceToTarget();
        if (patrolBusy)
        {
            return;
        }

        patrolBusy = true;

        switch (target.interactionType)
        {
            case InteractableType.PLAYER:
                //Debug.Log("Interacted Player");
                PlayAnimations();
                Invoke(nameof(SendAttack), attackDelay);
                Invoke(nameof(ResetBusy), attackSpeed);
                break;
        }
    }
    //================โจมตี==========================//
    private void SendAttack()
    {
        //Debug.Log("Attacked Enemy");
        if (target == null) return;
        target.myActor.TakeDamage(punchDamage);
        target.myActor.DamageOnHealthBar();
        SendPlayer();
    }
    private void SendPlayer()
    {
        //target.myPlayer.SetPlayerHealth(punchDamage);
        //GiveDamageToPlayer(punchDamage);

        if (target.myActor.currentHealth <= 0)
        {
            //Debug.Log("Enemy DEATH");
            target.myPlayer.SetPlayerDie(true);
            //target.myActor.OnDeath();
            ResetTarget();
            ResetMoveing();
            return;
        }
        else
        {
            //target.myPatrol.SetTargetToPlayer(this);
        }
    }
    //==============================================//
    private void ResetBusy()
    {
        patrolBusy = false;
        PlayAnimations();
    }
    //========================ท่าทางแอนิเมชั่น================================//
    private void PlayAnimations()
    {
        if (animator != null)
        {
            if (patrolBusy)
            {
                animator.SetBool(ATTACK, true);
                return;
            }
            else if (patrolDie)
            {
                animator.SetBool(ATTACK, false);
                animator.SetBool(WALK, false);
                animator.SetBool(IDLE, false);
                return;
            }
            else
            {
                animator.SetBool(ATTACK, false);

                if (agent.velocity != Vector3.zero)
                {
                    animator.SetBool(IDLE, false);
                    animator.SetBool(WALK, true);
                }
                else
                {
                    animator.SetBool(WALK, false);
                    animator.SetBool(IDLE, true);
                }
            }
        }
    }

    public void SetTargetToPlayer(PlayerController player)
    {
        //Debug.Log("SetTarged Success");
        if (target != null) return;
        target = player.GetComponent<Interactable>();
        //Debug.Log("PatrolController Target : "+target);
    }

    public void SetPatrolDie(bool die)
    {
        target = null;
        patrolDie = die;
        agent.enabled = !agent.enabled;
        capsuleCollider.enabled = !capsuleCollider.enabled;
        animator.SetTrigger(DEATH);

        //int gold = Random.Range(monsterInfoSO.GoldMin, monsterInfoSO.GoldMax);
        GiveRewardToPlayer(monsterInfoSO);
    }

    private void MoveWithDelay()
    {
        if (target != null || patrolDie) return;

        patrolMoveing = true;
        Vector3 randomPosition = Random.insideUnitSphere * moveRadius;
        randomPosition.y = 0f;
        Vector3 finalPosition = transform.position + randomPosition;
        agent.SetDestination(finalPosition);
        //Debug.Log("Move With Delay");
        Invoke(nameof(ResetMoveing), 0.5f);
        return;
    }

    private void ResetMoveing()
    {
        patrolMoveing = false;
        //Debug.Log("(ResetMoving)Moving : "+patrolMoveing);
    }
    private void ResetTarget()
    {
        target = null;
        //Debug.Log("Reseted Target");
    }

    private void GiveRewardToPlayer(MonsterInfoSO infoSO)
    {
        int gold = Random.Range(infoSO.GoldMin, infoSO.GoldMax);
        int exp = infoSO.Experience;

        EventManager.instance.playerEvents.ExperienceGained(exp);
        EventManager.instance.currencyEvents.GoldGained(gold);
        GiveDropRateToPlayer(infoSO);
    }

    private void GiveDropRateToPlayer(MonsterInfoSO infoSO)
    {
        HashSet<ItemInfoSO> items = new HashSet<ItemInfoSO>();
        HashSet<float> drops = new HashSet<float>();

        for (int i = 0; i < Mathf.Min(infoSO.Items.Count, infoSO.DropRate.Count); i++)
        {
            ItemInfoSO item = infoSO.Items[i];
            float drop = infoSO.DropRate[i];

            if (items.Contains(item))
            {
                Debug.LogWarning("Found Dupplicate UniqueItem");
                continue;
            }

            items.Add(item);
            drops.Add(drop);
            //Debug.Log("Add Item to HashSet " + item + "rate" + drop);

            float randomValue = Random.value;
            //Debug.Log("Random Value: " + randomValue);
            if (randomValue <= drop)
            {
                const int amountGain = 1;
                EventManager.instance.itemEvents.AddItem(item);
                EventManager.instance.pickupEvents.UpdateItem(item, amountGain);
                Debug.Log("Drop Success: " + item);
            }
        }
    }

    private void GiveDamageToPlayer(int damaged)
    {
        EventManager.instance.healthEvents.HealthChange(damaged);
    }


}

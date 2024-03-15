using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public Interactable target;
    const string WALK = "Walk";
    const string IDLE = "Idle";
    const string ATTACK = "Attack";
    const string DEATH = "Death";
    //=====================================================//
    [Header("Moving")]
    [SerializeField] private float moveRadius;
    [SerializeField] private float ramdomDelayMin;
    [SerializeField] private float ramdomDelayMax;

    //====================================================//
    [Header("Aggro System")]
    [SerializeField] float AggroTime;
    [SerializeField] bool StartCheckAggro = false;
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
        AggroTime = monsterInfoSO.AggroTime;
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }

    // Update is called once per frame
    void Update()
    {
        RandomToMove();
        PlayAnimations();

        FollowTarget();
        AggroCheck();

        if (AggroTime <= 0)
        {
            target = null;
            AggroTime = monsterInfoSO.AggroTime;
            StartCheckAggro = false;
            
        }
    }

    void AggroCheck()
    {
        if (!StartCheckAggro) return;
     
        if (Vector3.Distance(target.transform.position, transform.position) > attackDistance)
        {
            AggroTime -= Time.deltaTime;            
        }
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

        if (agent.obstacleAvoidanceType != ObstacleAvoidanceType.LowQualityObstacleAvoidance)
        {
            agent.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
        }
        //=============ระยะห่างเป้าหมายน้อยกว่าระยะโจมตี========================//
        if (Vector3.Distance(target.transform.position, transform.position) <= attackDistance)
        {
            if (target.interactionType == InteractableType.PLAYER)
            {
                ReachDistance();
                StartCheckAggro = true;
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

        /* switch (target.interactionType)
        {
            case InteractableType.PLAYER:
                //Debug.Log("Interacted Player");
                PlayAnimations();
                Invoke(nameof(SendAttack), attackDelay);
                Invoke(nameof(ResetBusy), attackSpeed);
                break;
        } */
    }
    //================โจมตี==========================//
    private void SendAttack()
    {        
        //Dragon's Addition Code        
        if (this.GetComponent<BossBehaviourScript>() != null) //Using for hitCount in BossBehaviourScript
        {
            BossBehaviourScript bossBehaviour = this.GetComponent<BossBehaviourScript>();
            bossBehaviour.HitCount += 1;
        }

        //Debug.Log("Attacked Enemy");
        if (target == null) return;
        target.myActor.TakeDamage(monsterInfoSO.Damage);
        Vector3 position = target.transform.position;
        EventManager.instance.playerEvents.AttackPopUp(position, monsterInfoSO.Damage.ToString(), Color.red);
        target.myActor.DamageOnHealthBar();
        this.GetComponent<AudioSource>().Play();
        SendPlayer();       
    }
    private void SendPlayer()
    {
        //target.myPlayer.SetPlayerHealth(punchDamage);
        //GiveDamageToPlayer(punchDamage);

        if (target.myActor.CurrentHealth <= 0)
        {
            //Debug.Log("Enemy DEATH");
            //target.myPlayer.SetPlayerDie(true);
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
        Debug.Log("Boss Die");
        target = null;
        patrolDie = die;
        agent.enabled = !agent.enabled;
        capsuleCollider.enabled = !capsuleCollider.enabled;
        animator.SetTrigger(DEATH);
        animator.Play(DEATH);

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

    private void OnTriggerEnter(Collider other)
    {
        MeshCollider meshCollider = other.GetComponent<MeshCollider>();
        if (meshCollider != null)
        {
            Debug.Log("MeshCollider" + meshCollider.tag);

            target = FindFirstObjectByType<PlayerController>().GetComponent<Interactable>();
            target.myPlayer.InteractableChange(this.gameObject);
            target.myPlayerSkill.SendAttackSkill();
            //Debug.Log("Target myPlayerSkill: " + target.myPlayerSkill);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.HID;

public class PlayerController : MonoBehaviour
{
    CustomAction input;
    NavMeshAgent agent;
    Animator animator;
    //PatrolController patrolController;
    const string IDLE = "Idle";
    const string WALK = "Walk";
    const string ATTACK = "Attack";
    const string PICKUP = "Pickup";
    const string DEATH = "Death";
    private float pickupDistance = 1.5f;
    private float talkDistance = 1.5f;
    private float targetDistance;
    private CapsuleCollider capsuleCollider;
    private bool playerDie;
    private Actor playerActor;
    private PlayerSkill playerSkill;
    private StatController statController;
    //=====================================================//
    [Header("Attack")]
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDelay;
    [SerializeField] private float attackDistance;
    [SerializeField] private ParticleSystem attackEffect;
    private bool playerBusy = false;
    public Interactable target { get; private set; }
    //=====================================================//
    [Header("Movement")]
    [SerializeField] private ParticleSystem clickEffect;
    [SerializeField] private LayerMask clickLayer;
    [SerializeField] private float lookRotationSpeed;

    //Disable Movement Player for using Skill
    public bool CanWalk = true;


    //====================================================//
    [Header("Damage")]
    [SerializeField] private int physicDamage;
    [SerializeField] private int meleeDamage;
    //====================================================//
    [Header("Health")]
    [SerializeField] private int playerCurrentHealth;
    [SerializeField] private int playerMaxHealth;
    //====================================================//

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        //patrolController = GetComponent<PatrolController>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        input = new CustomAction();
        AssignInput();
        playerActor = GetComponent<Actor>();
        playerSkill = GetComponent<PlayerSkill>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //playerMaxHealth = playerActor.currentHealth;
        //EventManager.instance.healthEvents.HealthChange(playerMaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
        //PlayAnimations();
    }

    void AssignInput()
    {
        input.Main.Move.performed += ctx => ClickToMove();
        input.Main.AttackOff.performed += ctx => ClickToCancle();
        input.Main.Talk.performed += ctx => SpaceToTalk();
        input.Main.Inventory.performed += ctx => PushToInventory();
        input.Main.CharInfo.performed += ctx => PushToCharacterInfo();
    }

    void OnEnable()
    {
        input.Enable();
        EventManager.instance.statEvents.onSendStatController += StartStatus;
    }

    void OnDisable()
    {
        input.Disable();
        EventManager.instance.statEvents.onSendStatController -= StartStatus;
    }
    private void StartStatus(StatController myStat)
    {
        statController = myStat;
    }

    public void InteractableChange(QuestPoint npc)
    {
        target = npc.GetComponent<Interactable>();
        //Debug.Log("target from player: " + npc + "Type: " + target.interactionType);
    }

    //==================ยกเลิกการโจมตีเป้าหมาย=============================//
    void ClickToCancle()
    {
        /* if (EventSystem.current.IsPointerOverGameObject())
        {
            EventManager.instance.inputEvents.InventoryItemOptionalClose();
            return;
        } */
        SendInventory();

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, clickLayer))
        {
            if (!hit.transform.CompareTag("Interactable"))
            {
                //Debug.Log("Is not interactablel tag");
                ResetBusy();
                ResetTarget();
            }
            else
            {
                Debug.Log("this is a whatever LV.99 Heal 987/1,000");
            }
        }
    }

    //============================เคลื่อนที่ตัวละคร==========================//
    private void ClickToMove()
    {
        if (CanWalk)
        {
            //Debug.Log("Click Success");
            if (EventSystem.current.IsPointerOverGameObject() || playerDie) { return; }//ถ้าคลิกโดนอินเตอร์เฟส จะถูกรีเทิน

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, clickLayer))
            {
                if (hit.transform.CompareTag("Interactable"))
                {
                    //เช็คเฉพาะถ้าเป้าหมายเป็น NPC ก่อนหน้านี้ ให้ ResetBusy เพื่อป้องกันไม่ให้ Attack รัวๆถ้าเป้าหมายเป็น Enemy
                    if (target != null && target.interactionType == InteractableType.NPC)
                    {
                        ResetBusy();
                    }
                    target = hit.transform.GetComponent<Interactable>();
                    Debug.Log("PlayerController Target : " + target);

                    switch (target.interactionType)
                    {
                        case InteractableType.ENEMY:
                            targetDistance = attackDistance;
                            break;
                        case InteractableType.ITEM_QUEST:
                            targetDistance = pickupDistance;
                            break;
                        case InteractableType.NPC:
                            targetDistance = talkDistance;
                            break;
                        case InteractableType.ITEM_INVENTORY:
                            targetDistance = pickupDistance;
                            break;

                    }

                    if (clickEffect != null)
                    {
                        Instantiate(clickEffect, hit.point += new Vector3(0, 1f, 0), clickEffect.transform.rotation);
                    }
                }
                else
                {
                    //AnimMove(false,true); //เดิน
                    agent.SetDestination(hit.point);

                    if (target != null)
                    {
                        Debug.Log("target: " + target);
                        switch (target.interactionType)
                        {
                            case InteractableType.NPC:
                                ResetBusy();
                                SendNpc(false);
                                ResetTarget();
                                break;
                            case InteractableType.ENEMY:
                                ResetBusy();
                                ResetTarget();
                                break;
                            case InteractableType.ITEM_QUEST:
                                ResetBusy();
                                ResetTarget();
                                break;
                            case InteractableType.ITEM_INVENTORY:
                                ResetBusy();
                                ResetTarget();
                                break;
                        }
                    }

                    if (clickEffect != null)
                    {
                        Instantiate(clickEffect, hit.point += new Vector3(0, 1f, 0), clickEffect.transform.rotation);
                    }
                }
            }
        }
    }
    #region หันหน้าไปยังทิศทางของเป้าหมาย
    void FaceToTarget()
    {
        Vector3 direction = (agent.destination - target.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(-direction.x, 0, -direction.z));
        transform.rotation = lookRotation;
        //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
    }

    #endregion
    //========================ท่าทางแอนิเมชั่น================================//
    void AnimMove(bool animIdle, bool animWalk)
    {
        animator.SetBool(IDLE, animIdle);
        animator.SetBool(WALK, animWalk);
    }

    void AnimAttack(bool animAttack)
    {
        animator.SetBool(ATTACK, animAttack);
    }

    //==============================เคลื่อนที่ไปยังเป้าหมาย===============================//
    void FollowTarget()
    {
        if (agent.velocity == Vector3.zero)
        {
            AnimMove(true, false); //ยืน
        }
        else
        {
            AnimMove(false, true); //เดิน
        }

        if (target == null)
        {
            return;
        }
        //PlayAnimations();
        //=============ระยะห่างเป้าหมายน้อยกว่าระยะโจมตี========================//
        if (Vector3.Distance(target.transform.position, transform.position) <= targetDistance)
        {
            //Debug.Log("TargetDistance: "+targetDistance);
            ReachDistance();
            if (target != null && target.interactionType == InteractableType.NPC)
            {
                if (target.myQuestPoint.playerIsNear) return;

                SendNpc(true);
                //EventManager.instance.inputEvents.SubmitPressed();
                SpaceToTalk();
                return;
            }
        }
        else
        {
            if (CanWalk) { agent.SetDestination(target.transform.position); }
        }
    }

    //==============================เข้าถึงระยะ============================//
    void ReachDistance()
    {
        if (CanWalk)
        {
            agent.SetDestination(transform.position);
            FaceToTarget();
            if (playerBusy)
            {
                return;
            }

            playerBusy = true;

            switch (target.interactionType)
            {
                case InteractableType.ENEMY:
                    AnimAttack(true);
                    Invoke(nameof(SendAttack), attackDelay);
                    Invoke(nameof(ResetBusy), attackSpeed);
                    break;
                case InteractableType.ITEM_QUEST:
                    //Debug.Log("Interacted Item");
                    animator.SetBool(PICKUP, true);
                    PickupItem();
                    target.InteracItem();
                    ResetTarget();
                    Invoke(nameof(ResetBusy), 0.5f);
                    break;
                case InteractableType.NPC:
                    Debug.Log("Interacted NPC");
                    break;
                case InteractableType.ITEM_INVENTORY:
                    animator.SetBool(PICKUP, true);
                    SendItem();
                    target.InteracItem();
                    ResetTarget();
                    Invoke(nameof(ResetBusy), 0.5f);
                    break;
            }
        }
    }
    void SendNpc(bool near)
    {
        target.myQuestPoint.PlayerIsNear(near);
    }
    //================โจมตี==========================//
    void SendAttack()
    {
        //Debug.Log("Attacked Enemy");
        if (target == null) return;
        physicDamage = statController.v_patk.statValue;
        target.myActor.TakeDamage(physicDamage);
        SendEnemy();
    }
    //==========================================//
    void SendEnemy()
    {
        if (target.myActor.currentHealth <= 0)
        {
            //Debug.Log("Enemy DEATH");
            target.myPatrol.SetPatrolDie(true);
            target.myActor.OnDeath();
            KillMonster();
            ResetTarget();
            return;
        }
        else
        {
            target.myPatrol.SetTargetToPlayer(this);
        }
    }

    void ResetBusy()
    {
        playerBusy = false;
        //PlayAnimations();
        AnimAttack(false);
        animator.SetBool(PICKUP, false);
        //Debug.Log("Reseted Busy");
    }

    private void ResetTarget()
    {
        target = null;
        //Debug.Log("Reseted Target");
    }

    private void KillMonster()
    {
        EventManager.instance.killEvents.MonsterKilled();
    }

    private void PickupItem()
    {
        EventManager.instance.pickupEvents.ItemPickup();
    }

    private void SpaceToTalk()
    {
        if (target == null || target.interactionType != InteractableType.NPC) return;

        if (target.myQuestPoint.playerIsNear)
        {
            EventManager.instance.inputEvents.SubmitPressed();
        }
    }

    private void PushToInventory()
    {
        EventManager.instance.inputEvents.InventoryPressed();
        EventManager.instance.inputEvents.InventoryItemOptionalClose();
        //EventManager.instance.itemEvents.ListNameItem();
    }

    private void PushToCharacterInfo()
    {
        EventManager.instance.inputEvents.StatPressed();
    }

    private void SendItem()
    {
        target.myItem.OnTakeItem();
        //EventManager.instance.itemEvents.ListNameItem();
        //EventManager.instance.itemEvents.AddItem(itemInfoSO);
    }

    private void SendInventory()
    {
        //EventManager.instance.inputEvents.InventoryItemClose();
    }
    public void SetPlayerDie(bool die)
    {
        target = null;
        playerDie = die;
        agent.enabled = !agent.enabled;
        capsuleCollider.enabled = !capsuleCollider.enabled;
        animator.SetTrigger(DEATH);
    }
    /* public void SetPlayerHealth(int amount)
    {
        playerCurrentHealth = playerActor.currentHealth;
        //playerCurrentHealth = amount;
        EventManager.instance.healthEvents.HealthChange(playerCurrentHealth);
        Debug.Log(playerCurrentHealth);
    } */


    // Dragon's Part

    public void StopSequence()
    {
        agent.SetDestination(transform.position);
    }
}

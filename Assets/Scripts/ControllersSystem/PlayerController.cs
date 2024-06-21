using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;
using System.Linq;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public CustomAction input;
    public NavMeshAgent agent;
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
    public bool PlayerDie { get; private set; }
    private Actor playerActor;
    private PlayerSkill playerSkill;
    private AutoFightSystem auto;
    public bool isAuto = false;
    public StatController statController;
    [Header("HitChangeSystem")]
    public int Accuracy = 50; //base Acc is 50
    public int Evade = 10;
    //=====================================================//
    [Header("Attack")]
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDelay;
    [SerializeField] private float attackDistance;
    [SerializeField] private ParticleSystem attackEffect;    
    private bool playerBusy = false;
    public bool isReachDistance;
    public Interactable target { get; private set; }
    //=====================================================//
    [Header("Movement")]
    [SerializeField] private ParticleSystem clickEffect;
    [SerializeField] private LayerMask clickLayer;
    [SerializeField] private float lookRotationSpeed;
    [SerializeField]private float groundOffset = 0.1f;

    //Disable Movement Player for using Skill
    public bool CanWalk = true;


    //====================================================//
    [Header("Damage")]
    [SerializeField] private int physicDamage;
    [SerializeField] private int meleeDamage;
    public int MagicDefend = 0;
    public int PhysicalDefend = 0;
    [SerializeField] private int CritRate = 0;
    [SerializeField] private int CritDMG = 0;
    //====================================================//
    public List<Interactable> _targetsList  = new List<Interactable>();
    public bool isTargetSerected;
    //====================================================//
    [Header("Mouse Position")]
    public Vector3 mousePositionInScene;

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
        auto = GetComponentInChildren<AutoFightSystem>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //physicDamage = statController.v_patk.statValue;
        //playerMaxHealth = playerActor.currentHealth;
        //EventManager.instance.healthEvents.HealthChange(playerMaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
        FollowTarget();
        //PlayAnimations();
        if (playerActor.CurrentHealth <= 0 && !PlayerDie) SetPlayerDie(true);
        if (isAuto && target == null){
            if(auto.nearsestTarget != null){
                target = auto.nearsestTarget.gameObject.GetComponent<Interactable>();
            }else{isAuto = false;}
        }
        PhysicalDefend = statController.v_pdef.statValue;
    }
    

    void AssignInput()
    {
        input.Main.Move.performed += ctx => ClickToMove();
        input.Main.AttackOff.performed += ctx => ClickToCancle();
        input.Main.Talk.performed += ctx => SpaceToTalk();
        input.Main.Inventory.performed += ctx => PushToInventory();
        input.Main.CharInfo.performed += ctx => PushToCharacterInfo();
        input.Main.Macro.performed += ctx => Macro();
        input.Main.AttackAction.performed += ctx => Action_NormalAttack();
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

    public void InteractableChange(GameObject obj)
    {
        target = obj.GetComponent<Interactable>();
        Debug.Log("target from player: " + obj + "Type: " + target.interactionType);
    }

    //==================ยกเลิกการโจมตีเป้าหมาย=============================//
    void ClickToCancle()
    {
        /* if (EventSystem.current.IsPointerOverGameObject())
        {
            EventManager.instance.inputEvents.InventoryItemOptionalClose();
            return;
        } */
        isTargetSerected = false;
        if (EventSystem.current.IsPointerOverGameObject() || PlayerDie) { return; }//ถ้าคลิกโดนอินเตอร์เฟส จะถูกรีเทิน
        isAuto = false;
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
        isAuto = false;
        isTargetSerected = false;
        //Debug.Log("Click Success");
        if (EventSystem.current.IsPointerOverGameObject() || PlayerDie) { return; }//ถ้าคลิกโดนอินเตอร์เฟส จะถูกรีเทิน
        if (playerSkill.isSkillPlaying) { playerSkill.isSkillPlaying = false; }
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, clickLayer))
        {
            mousePositionInScene = hit.point; // Mouse position in scene coordinates
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
                    Instantiate(clickEffect, hit.point += new Vector3(0, groundOffset, 0), clickEffect.transform.rotation);
                }
            }
            else
            {
                //AnimMove(false,true); //เดิน
                agent.SetDestination(hit.point + hit.normal * groundOffset);

                if (target != null)
                {
                    Debug.Log("target: " + target);
                    switch (target.interactionType)
                    {
                        case InteractableType.NPC:
                            ResetBusy();
                            SendNpc(false);
                            ResetTarget();
                            EventManager.instance.dialogueEvents.DialogueCancle();
                            break;
                        default:
                            ResetBusy();
                            ResetTarget();
                            break;
                    }
                }

                if (clickEffect != null)
                {
                    Instantiate(clickEffect, hit.point += new Vector3(0, groundOffset, 0), clickEffect.transform.rotation);
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
    public void FollowTarget()
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
        if (playerSkill.isSkillPlaying) return;
        //PlayAnimations();
        //=============ระยะห่างเป้าหมายน้อยกว่าระยะโจมตี========================//
        if (Vector3.Distance(target.transform.position, transform.position) <= targetDistance && isTargetSerected )
        {
            isReachDistance = true;
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
            if (CanWalk && isTargetSerected) { agent.SetDestination(target.transform.position); isReachDistance = false;}
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
                    if(!isTargetSerected){
                        ResetBusy();
                    } 
                    /* Invoke(nameof(SendAttack), attackDelay);
                    Invoke(nameof(ResetBusy), attackSpeed); */
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
        if (target == null || target.myActor == null ) return;

        Accuracy = statController.v_acc.statValue;
        Evade = statController.v_evade.statValue;
        CritDMG = statController.v_crit_dam.statValue;
        CritRate = statController.v_crit_change.statValue;

        // Calculate hit rate 
        int hitRate = Accuracy - target.myPatrol.Evade;
        //Debug.LogWarning("HitRate: " + hitRate);
        // Generate random number between 0 and 100
        int randomNum = Random.Range(0, 100);
        //Debug.LogWarning("Random chance: " + randomNum);

        if (randomNum <= hitRate)
        {
            //Calculated TotalDamage
            physicDamage = statController.v_patk.statValue - target.myPatrol.PhysicalDefend;             
            //Debug.Log(physicDamage);

            Vector3 position = target.transform.position;

            int CritResult = Random.Range(0, 100); //Calculated Critical
            //Debug.Log("Crit Result: " + CritResult);

            //if Critical
            if (CritResult <= CritRate) {                 
                physicDamage = Mathf.RoundToInt(physicDamage * (CritDMG - 0.5f)); //Reduce 2 into 1.5                
                if (physicDamage <= 0)
                {
                    EventManager.instance.playerEvents.AttackPopUp(position, "Block", Color.red);                    
                    return;
                }

                target.myActor.TakeDamage(physicDamage);
                EventManager.instance.playerEvents.AttackPopUp(position, "Crit " + physicDamage.ToString(), Color.green);
                SendEnemy();
                return;
            }

            //if Not Critical
            if (physicDamage <= 0)
            {
                EventManager.instance.playerEvents.AttackPopUp(position, "Block", Color.red);
                return;
            }

            target.myActor.TakeDamage(physicDamage);            
            EventManager.instance.playerEvents.AttackPopUp(position, physicDamage.ToString(), Color.green);            
            SendEnemy();
        }
        else
        {
            Vector3 position = target.transform.position;
            EventManager.instance.playerEvents.AttackPopUp(position, "Miss", Color.green);
        }        
    }
    //==========================================//
    public void SendEnemy()
    {
        if (target.myActor.CurrentHealth <= 0)
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
        EventManager.instance.killEvents.MonsterKilled(target.myPatrol.monsterInfoSO.Id);
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
    private void Macro()
    {
        isTargetSerected = true;
        if(auto.nearsestTarget != null && !isAuto && !PlayerDie){
            isAuto = true;
            target = auto.nearsestTarget.gameObject.GetComponent<Interactable>();
            if (target != null && target.interactionType == InteractableType.NPC)
                {
                    ResetBusy();
                }
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
        }else{
            ResetTarget();
            isAuto = false;
            isTargetSerected = false;
        }
    }
    private void SendItem()
    {
        target.myItem.OnTakeItem();
        //EventManager.instance.itemEvents.ListNameItem();
        //EventManager.instance.itemEvents.AddItem(itemInfoSO);
    }
    public async void SetPlayerDie(bool die)
    {
        isAuto = false;
        target = null;
        PlayerDie = die;
        agent.enabled = !agent.enabled;
        capsuleCollider.enabled = !capsuleCollider.enabled;
        animator.SetTrigger(DEATH);
        await Task.Delay(5000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        ResetBusy();
    }

    public void MoveSpeed(float speed)
    {
        agent.speed = speed;
    }    

    private void Action_NormalAttack(){
       
        isTargetSerected = !isTargetSerected;
    }
}

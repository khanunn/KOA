using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem.HID;
using UnityEngine.Rendering;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    public class SkillAction : IDisposable //Special Interface for destroy after doing something
    {
        private int lifeTime = 3000;
        private GameObject prefab;
        private GameObject instanceVFX;

        public async void Init(int skillId, SkillInfoSO SkillData, Transform parent, quaternion rotation)
        {
            //Load VFX to memory as Prefab
            var op = Addressables.LoadAssetAsync<GameObject>($"Assets/VFX/InScene/{skillId}.prefab");
            prefab = await op.Task;

            //Bring Prefab to the scene as gameobject
            instanceVFX = Instantiate(prefab);

            //Bring it into child and set pos and set 0 rotation at player (Specific detail are setting in child of VFX)
            instanceVFX.transform.SetParent(parent);
            instanceVFX.transform.localPosition = new Vector3(prefab.transform.position.x, prefab.transform.position.y, prefab.transform.position.z);
            instanceVFX.transform.rotation = rotation;

            //Set Lifetime base on each skill            
            lifeTime = SkillData.LifetimeVFX;


            Debug.Log("lifeTime: " + lifeTime);
            await Task.Delay(lifeTime); //Like yield return waitforsecond(second) in IEnumerator

            Dispose(); //Special function destroy after doing something
        }
        public void Dispose()
        {
            Destroy(instanceVFX);
            Addressables.Release(prefab);
        }
    }


    [Header("Skill Settings")]
    Animator animator;
    [SerializeField] int Skill_ID;
    public bool isSkillPlaying = false;
    [SerializeField] SkillSlotManager slotManager;

    [Header("Equipment Settings")]
    public bool IsWeapon = false;
    [SerializeField] SkinnedMeshRenderer Sword;
    [SerializeField] SkinnedMeshRenderer Shield;

    [Header("Player Setting")]
    [SerializeField] PlayerController Player;
    [SerializeField] GameObject VFX;

    public float[] MaxCooldown; //Set Max CD
    public float[] skillCooldowns; //using for Count CD
    public int[] CurrentSkill; // List all aviable skill

    public SkillInfoSO[] SkillData; // List all aviable skill

    //use for hard code set skill
    int[] UnWeaponSkillSet = { 0, 3, 4 };
    int[] WeaponSkillSet = { 1, 2, 3, 4 };


    float[] SkillMaxSetCD = { 3, 3, 5, 7 };

    private StatController statController;

    private Actor myActor;
    private PlayerController player;
    AnimatorStateInfo stateInfo;

    void OnEnable()
    {
        EventManager.instance.statEvents.onSendStatController += StartStatus;
    }

    void OnDisable()
    {
        EventManager.instance.statEvents.onSendStatController -= StartStatus;
    }
    private void StartStatus(StatController myStat)
    {
        statController = myStat;
    }

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
        myActor = GetComponent<Actor>();
        player = GetComponent<PlayerController>();

        LoadScriptObject();

        SkillData = new SkillInfoSO[6];

        if (IsWeapon)
        {
            skillCooldowns = new float[4];
            SkillMaxSetCD = new float[4];
            CurrentSkill = new int[4];
        }
        else
        {
            skillCooldowns = new float[3];
            SkillMaxSetCD = new float[3];
            CurrentSkill = new int[3];
        }



        StartCoroutine(CooldownTimer());
    }


    IEnumerator CooldownTimer()
    {
        //int temp = skillCooldowns.Length;
        while (true)
        {
            //if (temp != skillCooldowns.Length) break;

            for (int i = 0; i < skillCooldowns.Length; i++)
            {
                if (skillCooldowns[i] > 0)
                {
                    skillCooldowns[i] -= Time.deltaTime;
                    slotManager.UnActiveSlot(i);
                    if (skillCooldowns[i] < 0)
                        skillCooldowns[i] = 0;
                }
                else slotManager.ActiveSlot(i);
            }
            yield return null;
        }

        //if(temp != skillCooldowns.Length) StartCoroutine(CooldownTimer());
    }

    void SkillSystem()
    {
        if (Player.PlayerDie) return;

        if (!isSkillPlaying)
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                TryStartSkill(CurrentSkill[0], 0);
                //Debug.Log(CurrentSkill[0]);
            }

            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                TryStartSkill(CurrentSkill[1], 1);
                // Debug.Log(CurrentSkill[1]);
            }

            if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                TryStartSkill(CurrentSkill[2], 2);
                //Debug.Log(CurrentSkill[2]);
            }

            if (IsWeapon)
            {
                if (Input.GetKeyUp(KeyCode.Alpha4))
                {
                    TryStartSkill(CurrentSkill[3], 3);
                    //Debug.Log(CurrentSkill[3]);
                }
            }
        }
    }

    private void TryStartSkill(int skillId, int ButtonID)
    {
        // Check if the skill is not on cooldown
        if (skillCooldowns[ButtonID] == 0 && !isSkillPlaying)
        {
            // isSkillPlaying = true;
            StartSkill(skillId, ButtonID);
            Invoke("ResetSkill", 0.01f);
            // Set cooldown for the skill
            skillCooldowns[ButtonID] = MaxCooldown[ButtonID];
        }
    }

    public void ResetSkill()
    {
        animator.SetInteger("Skill_ID", -1);
    }

    void StartSkill(int skillId, int ButtonID)
    {
        if (isSkillPlaying)
            return;


        Player.StopSequence(); //using to player stop moving

        animator.Play(skillId.ToString()); //Player SKill Movement

        Debug.Log(skillId);
        var action = new SkillAction();
        action.Init(skillId, SkillData[ButtonID], VFX.transform, Player.transform.rotation);
    }

    private float[] GetSkillCooldowns()
    {
        return skillCooldowns;
    }

    //This code suppose to call when it have skill changing panal in the future
    private async void LoadScriptObject()  //Call this everytime when player need to resetskill on skillslot
    {
        int i = 0;

        //will be using later on when equipment Item are call
        //SkillMaxSetCD = new float[CurrentSkill.Length];
        //skillCooldowns = new float[CurrentSkill.Length];
        SkillData = new SkillInfoSO[CurrentSkill.Length];
        foreach (int ID in CurrentSkill)
        {
            //Debug.Log(i);
            if (i < CurrentSkill.Length)
            {
                var op = Addressables.LoadAssetAsync<SkillInfoSO>($"Assets/Skill/{ID}.asset"); //Load VFX to memory as Prefab
                var prefab = await op.Task;
                SkillData[i] = prefab;
                SkillMaxSetCD[i] = prefab.CooldownSkill;
            }
            i++;
        }

        slotManager.SettingIconAsync();

    }

    private void Update()
    {
        SkillSystem();

        MaxCooldown = SkillMaxSetCD;


        if (IsWeapon)
        {
            CurrentSkill = WeaponSkillSet;

            //This code suppose to call when it have skill changing panal in the future
            LoadScriptObject();
        }
        else
        {
            CurrentSkill = UnWeaponSkillSet;
            //This code suppose to call when it have skill changing panal in the future
            LoadScriptObject();
        }

        Skill_ID = animator.GetInteger("Skill_ID");
        animator.SetBool("IsWeapon", IsWeapon);

        /* if (IsWeapon)
        {
            Sword.enabled = true;
            Shield.enabled = true;
        }
        else
        {
            Sword.enabled = false;
            Shield.enabled = false;
        } */

        // Check if the animation has finished
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Skill")) //Don't forget to add tag in animator
        {
            //isSkillPlaying = false;
            //ResetSkill();

        }

        /* if (isSkillPlaying)
        {
            Player.StopSequence();
        } */
    }
    public void SkillPLaying()
    {
        isSkillPlaying = true;
        Player.StopSequence();
    }

    public void SkillNotPLaying()
    {
        isSkillPlaying = false;
    }
    public void SendAttackSkill()
    {
        Debug.Log("SendAttackSkill Connected");
        PlayerController controller = GetComponent<PlayerController>();
        int skillPhysicDamage = statController.v_patk.statValue * 2;
        Debug.Log("skill damage: " + skillPhysicDamage);
        Debug.Log("skill Target: " + controller.target);

        if (controller.target == null) return;
        controller.target.myActor.TakeDamage(skillPhysicDamage);
        Vector3 position = controller.target.transform.position;
        EventManager.instance.playerEvents.AttackPopUp(position, skillPhysicDamage.ToString(), Color.green);
        controller.SendEnemy();
    }

    private void HealingSkill()
    {
        int percent = 10;
        int healing = myActor.MaxHealth * percent / 100;
        EventManager.instance.healthEvents.HealthGained(healing);
    }

    private async void BoostSkill()
    {
        float defaultAnimatorSpeed = animator.speed;
        animator.speed = 2;
        player.MoveSpeed(7f);
        /* Animation animation = animator.GetComponent<Animation>();
        Debug.Log("Animation: " + animation);
        animation["Walk"].speed = 1.5f; */
        await Task.Delay(10000);

        animator.speed = defaultAnimatorSpeed;
        player.MoveSpeed(3.5f);
    }
}


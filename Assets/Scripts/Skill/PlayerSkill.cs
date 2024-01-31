using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    [Header("Skill Settings")]
    Animator animator;
    [SerializeField] int Skill_ID;
    [SerializeField] bool isSkillPlaying = false;
    [SerializeField] SkillSlotManager slotManager;

    [Header("Equipment Settings")]
    public bool IsWeapon = false;
    [SerializeField] SkinnedMeshRenderer Sword;
    [SerializeField] SkinnedMeshRenderer Shield;

    [Header("Player Setting")]
    [SerializeField] PlayerController Player;
    [SerializeField] GameObject VFX;
    private MeshCollider meshCollider;

    public float[] MaxCooldown; //Set Max CD
    public float[] skillCooldowns; //using for Count CD
    public int[] CurrentSkill; // List all aviable skill

    public SkillInfoSO[] SkillData; // List all aviable skill

    //use for hard code set skill
    int[] UnWeaponSkillSet = { 0, 3, 4 };
    int[] WeaponSkillSet = { 1, 2, 3, 4 };


    float[] SkillMaxSetCD = { 3, 3, 5, 7 };

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
        meshCollider = this.GetComponentInChildren<MeshCollider>();

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
    private void Start()
    {
        meshCollider.enabled = false;
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
        if (!isSkillPlaying)
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                TryStartSkill(CurrentSkill[0], 0);
                Debug.Log(CurrentSkill[0]);
            }

            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                TryStartSkill(CurrentSkill[1], 1);
                Debug.Log(CurrentSkill[1]);
            }

            if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                TryStartSkill(CurrentSkill[2], 2);
                Debug.Log(CurrentSkill[2]);
            }

            if (IsWeapon)
            {
                if (Input.GetKeyUp(KeyCode.Alpha4))
                {
                    TryStartSkill(CurrentSkill[3], 3);
                    Debug.Log(CurrentSkill[3]);
                }
            }


        }
    }

    private void TryStartSkill(int skillId, int ButtonID)
    {
        // Check if the skill is not on cooldown
        if (skillCooldowns[ButtonID] == 0)
        {
            isSkillPlaying = true;
            StartSkill(skillId);
            Invoke("ResetSkill", 0.01f);
            // Set cooldown for the skill
            skillCooldowns[ButtonID] = MaxCooldown[ButtonID];
        }
    }

    private void ResetSkill()
    {
        animator.SetInteger("Skill_ID", -1);
    }

    async void StartSkill(int skillId)
    {
        meshCollider.enabled = true;
        isSkillPlaying = true;
        Player.StopSequence(); //using to player stop moving

        animator.Play(skillId.ToString()); //Player SKill Movement
        var op = Addressables.LoadAssetAsync<GameObject>($"Assets/VFX/InScene/{skillId}.prefab"); //Load VFX to memory as Prefab

        if (VFX.transform.childCount == 0)
        {
            var prefab = await op.Task;

            //Bring Prefab to the scene as gameobject
            GameObject CreateVFX = Instantiate(prefab);

            //Bring it into child and set pos at player
            CreateVFX.transform.SetParent(VFX.transform);

            CreateVFX.transform.localPosition = new Vector3(0, 0, 0);
            CreateVFX.transform.rotation = Player.transform.rotation;
        }

        // Get the AnimationClip from the Animator's runtimeAnimatorController    
        AnimationClip[] animationClips = animator.runtimeAnimatorController.animationClips;

        // Find the AnimationClip with the specified name
        AnimationClip targetClip = System.Array.Find(animationClips, clip => clip.name == skillId.ToString());
        if (targetClip != null)
        {
            // Retrieve the length of the animation
            float animationLength = targetClip.length;
            Invoke("DestroyVFX", animationLength);
            Addressables.Release(op);
        }
    }

    private void DestroyVFX()
    {
        isSkillPlaying = false;

        foreach (Transform child in VFX.transform)
        {
            // Destroy all components attached to the child GameObject
            Destroy(child.gameObject);
        }
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
            isSkillPlaying = false;
            meshCollider.enabled = false;
            //ResetSkill();

        }

        if (isSkillPlaying)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                Player.StopSequence();
            }
        }
    }

    private void SendAttackSkill()
    {

    }
}


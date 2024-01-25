/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    [Header("Skill Settings")]
    Animator animator;
    [SerializeField] private int Skill_ID;
    [field: SerializeField] public bool isSkillPlaying { get; private set; }

    [Header("Equipment Settings")]
    public bool IsWeapon = false;
    [SerializeField] SkinnedMeshRenderer Sword;
    [SerializeField] SkinnedMeshRenderer Shield;

    [Header("Player Setting")]
    [SerializeField] PlayerController Player;
    //==========================================================//
    MeshCollider meshCollider;
    [SerializeField] GameObject VFX;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        Player = this.GetComponent<PlayerController>();
        meshCollider = GetComponentInChildren<MeshCollider>();
        //Debug.Log(meshCollider);
    }

    void SkillSystem()
    {
        if (!isSkillPlaying && Player.target != null)
        {
            if (IsWeapon) // Have Sword
            {
                Debug.Log(Player.target.interactionType);
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    isSkillPlaying = true;
                    StartSkill(1);
                    Invoke("ResetSkill", 0.1f);
                }

                if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    isSkillPlaying = true;
                    StartSkill(2);
                    Invoke("ResetSkill", 0.1f);
                }
            }

            if (!IsWeapon) // Bare hand
            {
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    StartSkill(0);
                    Invoke("ResetSkill", 0.1f);
                }
            }

            if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                StartSkill(3);
                Invoke("ResetSkill", 0.1f);
            }

            if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                StartSkill(4);
                Invoke("ResetSkill", 0.1f);
            }
        }
    }

    private void ResetSkill()
    {
        animator.SetInteger("Skill_ID", -1);
    }

    void StartSkill(int skillId)
    {
        isSkillPlaying = true;
        Player.StopSequence(); //using to player stop moving
        //animator.SetInteger("Skill_ID", skillId);     
        animator.Play(skillId.ToString());                     
    }

    private void Update()
    {
        SkillSystem();

        Skill_ID = animator.GetInteger("Skill_ID");
        animator.SetBool("IsWeapon", IsWeapon);

        if (IsWeapon)
        {
            Sword.enabled = true;
            Shield.enabled = true;
        }
        else
        {
            Sword.enabled = false;
            Shield.enabled = false;
        }

        // Check if the animation has finished
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Skill")) //Don't forget to add tag in animator
        {
            isSkillPlaying = false;
            //ResetSkill();
        }

        /* if (isSkillPlaying)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                Player.StopSequence();
            }
        } */

    }
}
*/
using System.Collections;
using System.Collections.Generic;
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

    [Header("Equipment Settings")]
    public bool IsWeapon = false;
    [SerializeField] SkinnedMeshRenderer Sword;
    [SerializeField] SkinnedMeshRenderer Shield;

    [Header("Player Setting")]
    [SerializeField] PlayerController Player;
    [SerializeField] GameObject VFX;
    [SerializeField] float [] Cooldown;

    private Dictionary<int, float> skillCooldowns = new Dictionary<int, float>();

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    void SkillSystem()
    {
        if (!isSkillPlaying)
        {
            if (IsWeapon) // Have Sword
            {
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    TryStartSkill(1);
                }

                if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    TryStartSkill(2);
                }
            }

            if (!IsWeapon) // Bare hand
            {
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    TryStartSkill(0);
                }
            }

            if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                TryStartSkill(3);
            }

            if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                TryStartSkill(4);
            }
        }
    }

    private void TryStartSkill(int skillId)
    {
        // Check if the skill is not on cooldown
        if (!skillCooldowns.ContainsKey(skillId) || Time.time >= skillCooldowns[skillId])
        {
            isSkillPlaying = true;
            StartSkill(skillId);
            Invoke("ResetSkill", 0.01f);

            // Set cooldown for the skill
            skillCooldowns[skillId] = Time.time + Cooldown[skillId];
        }
    }

    private void ResetSkill()
    {
        animator.SetInteger("Skill_ID", -1);
    }

    async void StartSkill(int skillId)
    {
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
           // CreateVFX.transform.rotation = Player.transform.rotation;
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

    private void Update()
    {
        SkillSystem();

        Skill_ID = animator.GetInteger("Skill_ID");
        animator.SetBool("IsWeapon", IsWeapon);

        if (IsWeapon)
        {
            Sword.enabled = true;
            Shield.enabled = true;
        }
        else
        {
            Sword.enabled = false;
            Shield.enabled = false;
        }

        // Check if the animation has finished
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Skill")) //Don't forget to add tag in animator
        {
            isSkillPlaying = false;
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
}
